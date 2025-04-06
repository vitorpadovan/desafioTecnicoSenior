namespace Challenge.Common.Interfaces
{
    public interface ICacheService
    {
        public Task SetValueAsync<T>(string key, T value, int ttl = 28000);
        public Task<T> GetValueAsync<T>(string key);
        public Task<int> AddCounter(string key, int quantity = 1);
        public Task DeleteCounter(string key);
    }
}
