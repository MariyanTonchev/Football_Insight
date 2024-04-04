using Football_Insight.Core.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace Football_Insight.Core.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;

        public CacheService(IMemoryCache _memoryCache)
        {
            memoryCache = _memoryCache;
        }

        public bool TryGetCachedItem(string cacheKey)
        {
            if (memoryCache.TryGetValue(cacheKey, out _))
            {
                return true;
            }

            return false;
        }
    }
}
