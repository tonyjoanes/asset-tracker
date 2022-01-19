namespace Scheduler.API.Entities
{
    using System;

    public class ScheduleItem
    {
        public string AssetId { get; set; }

        public string AssetName { get; set; }

        public DateTime StartDate { get; set; }

        public decimal Cost { get; set; }
    }
}
