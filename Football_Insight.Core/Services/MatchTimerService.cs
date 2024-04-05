using Football_Insight.Core.Contracts;
using Football_Insight.Infrastructure.Data.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace Football_Insight.Core.Services
{
    public class MatchTimerService : IMatchTimerService
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICacheService cacheService;

        public MatchTimerService(IMemoryCache _memoryCache, ICacheService _cacheService)
        {
            memoryCache = _memoryCache;
            cacheService = _cacheService;
        }

        public void UpdateMatchMinute(int matchId)
        {
            var minuteCacheKey = $"Match_{matchId}_Minutes";
            var statusCacheKey = $"Match_{matchId}_Status";

            if (cacheService.TryGetCachedItem(minuteCacheKey))
            {
                if(cacheService.TryGetCachedItem(statusCacheKey) && memoryCache.Get<MatchStatus>(statusCacheKey) == MatchStatus.HalfTime)
                {
                    memoryCache.Set(minuteCacheKey, Constants.MessageConstants.HalfTimeMinute, TimeSpan.FromHours(2));
                    memoryCache.Set(statusCacheKey, MatchStatus.SecondHalf, TimeSpan.FromHours(2));
                }
                memoryCache.Set(minuteCacheKey, memoryCache.Get<int>(minuteCacheKey) + 1, TimeSpan.FromHours(2));
            }
            else
            {
                memoryCache.Set(minuteCacheKey, 1, TimeSpan.FromHours(2));
            }
        }

        public int GetMatchMinute(int matchId)
        {
            var cacheKey = $"Match_{matchId}_Minutes";
            return memoryCache.Get<int>(cacheKey);
        }
    }
}
