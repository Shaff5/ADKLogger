using System;

namespace ADKLogger.Data
{
    public class LogEntry
    {
        public int LogEntryId { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
