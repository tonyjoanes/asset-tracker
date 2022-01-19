namespace Scheduler.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using Scheduler.API.Entities;

    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IDistributedCache _redisCache;

        public ScheduleRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<Schedule> GetSchedule(string assetName)
        {
            var schedule = await _redisCache.GetStringAsync(assetName);

            if (string.IsNullOrEmpty(schedule))
                return null;

            return JsonConvert.DeserializeObject<Schedule>(schedule);
        }

        public async Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            await _redisCache.SetStringAsync(schedule.AssetName, JsonConvert.SerializeObject(schedule));

            return await GetSchedule(schedule.AssetName);
        }

        public async Task DeleteSchedule(string assetName)
        {
            await _redisCache.RemoveAsync(assetName);
        }
    }
}
