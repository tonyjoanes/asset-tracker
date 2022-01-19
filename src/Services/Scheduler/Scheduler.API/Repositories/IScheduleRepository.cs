namespace Scheduler.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Scheduler.API.Entities;

    public interface IScheduleRepository
    {
        Task<Schedule> GetSchedule(string userName);

        Task<Schedule> UpdateSchedule(Schedule schedule);

        Task DeleteSchedule(string assetName);
    }
}
