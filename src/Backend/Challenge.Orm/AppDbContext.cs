using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Challenge.Orm
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly ILogger<AppDbContext> _logger;
        public AppDbContext(DbContextOptions options, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
