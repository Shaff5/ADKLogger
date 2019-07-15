using System;

namespace ADKLogger.WebApi.Models
{
    public class LogEntity
    {
        public string Title { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public DateTime DateTimeCreation { get; set; }
    }
}
