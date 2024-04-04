using Football_Insight.Core.Contracts;
using Football_Insight.Infrastructure.Data.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Football_Insight.Core.Services
{
    public class MatchTimerService : IMatchTimerService
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICacheService cacheService;
        private readonly IRepository repository;

        public MatchTimerService(IMemoryCache _memoryCache, ICacheService _cacheService, IRepository _repository)
        {
            memoryCache = _memoryCache;
            cacheService = _cacheService;
            repository = _repository;
        }
        
        public void UpdateMatchMinute(int matchId)
        {
            var cacheKey = $"Match_{matchId}_Minutes";

            if (cacheService.TryGetCachedItem(cacheKey))
            {
                memoryCache.Set(cacheKey, memoryCache.Get<int>(cacheKey) + 1, TimeSpan.FromHours(3));
            }
            else
            {
                memoryCache.Set(cacheKey, 1, TimeSpan.FromHours(1));
            }
        }

        public int GetMatchMinute(int matchId)
        {
            var cacheKey = $"Match_{matchId}_Minutes";
            return memoryCache.Get<int>(cacheKey);
        }
    }
}
