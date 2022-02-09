namespace WorkItems.API.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class WorkItem
    {
        public int Id { get; set; }

        public string AssetName { get; set; }

        public int AssetId { get; set; }

        public string WorkType { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
        
        public bool Passed { get; set; }

        public DateTime DateTime { get; set; }
    }
}
