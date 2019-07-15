using System.Collections.Generic;

namespace ADKLogger.WebApi.Models
{
    public class Statistics
    {
        public int TotalLogCount { get; set; }
        public Dictionary<string, int> LevelCounts { get; set; }
    }
}
