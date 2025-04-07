using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Bff.Middleware
{
    public class LogScopeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogScopeMiddleware> _logger;

        public LogScopeMiddleware(RequestDelegate next, ILogger<LogScopeMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string path;
            try
            {
                path = httpContext.Request.Path;
            }
            catch
            {
                path = "Without Path";
            }

            List<KeyValuePair<string, object>> logScope = new List<KeyValuePair<string, object>>();

            AddVersion(logScope);
            AddHosts(httpContext, logScope);

            logScope.Add(new KeyValuePair<string, object>("ServerName", Environment.MachineName));
            try
            {
                // Detect device type and browser
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                var deviceType = DetectDeviceType(userAgent);
                var browser = DetectBrowser(userAgent);

                logScope.Add(new KeyValuePair<string, object>("DeviceType", deviceType));
                logScope.Add(new KeyValuePair<string, object>("Browser", browser));
            }
            catch
            {
                // Handle exceptions related to user agent parsing
            }

            if (httpContext.User.Identity.IsAuthenticated)
            {
                logScope.Add(new KeyValuePair<string, object>("AuthenticatedUser", httpContext.User.Identity.Name));
            }
            else
            {
                logScope.Add(new KeyValuePair<string, object>("AuthenticatedUser", "anonymous"));
            }

            List<string> endpointsToIgnore = [@"/hc", @"/hub/globalbp", "/api/translation"];
            if (httpContext.Request != null && httpContext.Request.Path != null && (endpointsToIgnore.Contains(httpContext.Request.Path)))
            {
                using (this._logger.BeginScope(logScope))
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    // this.logger.LogDebug("Starting Request on {path}", path);
                    await _next(httpContext);
                    stopwatch.Stop();
                    // this.logger.LogDebug("Request Ending in {seconds} on {path}", stopwatch.Elapsed.TotalSeconds, path);
                }
            }
            else
            {
                using (this._logger.BeginScope(logScope))
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    this._logger.LogInformation("Starting Request on {path}", path);
                    await _next(httpContext);
                    stopwatch.Stop();
                    this._logger.LogInformation("Request Ending in {seconds} on {path}", stopwatch.Elapsed.TotalSeconds, path);
                }
            }
        }

        private static void AddHosts(HttpContext httpContext, List<KeyValuePair<string, object>> logScope)
        {
            try
            {
                logScope.Add(new KeyValuePair<string, object>("Request.Host", httpContext.Request.Host));
                logScope.Add(new KeyValuePair<string, object>("Request.Path", httpContext.Request.Path));
            }
            catch
            {

            }

            try
            {
                logScope.Add(new KeyValuePair<string, object>("Request.QueryString", httpContext.Request.QueryString.Value));
            }
            catch
            {

            }
        }

        private static DateTime RetrieveLinkerTimestamp(string filePath)
        {
            return TimeZoneInfo.ConvertTimeToUtc(System.IO.File.GetLastWriteTime(filePath), TimeZoneInfo.Local);
        }

        private void AddVersion(List<KeyValuePair<string, object>> logScope)
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = asm.GetName();
                Version version = assemblyName.Version;
                logScope.Add(new KeyValuePair<string, object>("BackendApiVersion", version.ToString()));
            }
            catch (Exception ex)
            {
                logScope.Add(new KeyValuePair<string, object>("BackendApiVersion", ex.Message));
            }

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var dateTime = RetrieveLinkerTimestamp(Assembly.GetExecutingAssembly().Location);
                logScope.Add(new KeyValuePair<string, object>("BackendPublishDate", dateTime));
            }
            catch
            {
                logScope.Add(new KeyValuePair<string, object>("BackendPublishDate", "Error"));
            }
        }

        private string DetectDeviceType(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            if (Regex.IsMatch(userAgent, "Mobile|Android|iP(hone|od|ad)|IEMobile|BlackBerry|Opera Mini", RegexOptions.IgnoreCase))
                return "Mobile";

            return "Browser";
        }

        private string DetectBrowser(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            if (userAgent.Contains("Chrome", StringComparison.OrdinalIgnoreCase))
                return "Chrome";
            if (userAgent.Contains("Firefox", StringComparison.OrdinalIgnoreCase))
                return "Firefox";
            if (userAgent.Contains("Safari", StringComparison.OrdinalIgnoreCase) && !userAgent.Contains("Chrome", StringComparison.OrdinalIgnoreCase))
                return "Safari";
            if (userAgent.Contains("Edge", StringComparison.OrdinalIgnoreCase))
                return "Edge";
            if (userAgent.Contains("MSIE", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Trident", StringComparison.OrdinalIgnoreCase))
                return "Internet Explorer";

            return "Other";
        }
    }
}
