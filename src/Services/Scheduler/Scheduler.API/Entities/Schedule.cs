namespace Scheduler.API.Entities
{
    using System.Collections.Generic;

    public class Schedule
    {
        public string AssetName { get; set; }

        public List<ScheduleItem> Items { get; set; } = new List<ScheduleItem>();

        public Schedule()
        {

        }

        public Schedule(string assetName)
        {
            AssetName = assetName;
        }

        public decimal TotalCost
        {
            get
            {
                decimal total = 0;
                foreach (var item in Items)
                {
                    total += item.Cost;
                }
                return total;
            }
        }
    }
}
