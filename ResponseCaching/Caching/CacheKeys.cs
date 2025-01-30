using Microsoft.Extensions.Caching.Memory;

namespace ResponseCaching.Caching
{
    public static class CacheKeys
    {
        public static dynamic cacheEntryOption = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(10),
            SlidingExpiration = TimeSpan.FromSeconds(30),
            Size = 1024
        };

        public static string UserList = "UserList";
    }
}
