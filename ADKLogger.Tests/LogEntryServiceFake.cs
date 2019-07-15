using System;
using System.Collections.Generic;
using System.Linq;
using ADKLogger.Data;
using ADKLogger.Services;

namespace ADKLogger.Tests
{
    public class LogEntryServiceFake : ILogEntryService
    {
        private readonly List<LogEntry> _logEntries;
        private int maxPageSize = 50;

        public LogEntryServiceFake()
        {
            _logEntries = new List<LogEntry>()
            {
                new LogEntry() { LogEntryId = 1, Title = "Title 1", Level = "Warning", Message = "Title 1 Message", UserId = "123", DateCreated = new DateTime(2016, 9, 21) },
                new LogEntry() { LogEntryId = 2, Title = "Title 2", Level = "Error", Message = "Title 2 Message", UserId = "456", DateCreated = new DateTime(2017, 12, 31) },
                new LogEntry() { LogEntryId = 3, Title = "Title 3", Level = "Informational", Message = "Title 4 Message", UserId = "789", DateCreated = new DateTime(2015, 1, 1) },
                new LogEntry() { LogEntryId = 4, Title = "Title 4", Level = "Error", Message = "Title 4 Message", UserId = "789", DateCreated = new DateTime(2019, 7, 14) },
                new LogEntry() { LogEntryId = 5, Title = "Title 5", Level = "Warning", Message = "Title 5 Message", UserId = "456", DateCreated = new DateTime(2019, 1, 1) },
                new LogEntry() { LogEntryId = 6, Title = "Title 6", Level = "Informational", Message = "Title 6 Message", UserId = "123", DateCreated = new DateTime(2018, 1, 1) },
                new LogEntry() { LogEntryId = 7, Title = "Title 7", Level = "Error", Message = "Title 7 Message", UserId = "123", DateCreated = new DateTime(2016, 2, 28) },
                new LogEntry() { LogEntryId = 8, Title = "Title 8", Level = "Warning", Message = "Title 8 Message", UserId = "789", DateCreated = new DateTime(2016, 5, 12) },
                new LogEntry() { LogEntryId = 9, Title = "Title 9", Level = "Informational", Message = "Title 9 Message", UserId = "456", DateCreated = new DateTime(2016, 10, 31) },
                new LogEntry() { LogEntryId = 10, Title = "Title 10", Level = "Warning", Message = "Title 10 Message", UserId = "987", DateCreated = new DateTime(2010, 3, 21) },
            };
        }

        public IQueryable<LogEntry> GetLogEntries()
        {
            return _logEntries.AsQueryable();
        }

        public IQueryable<ADKLogger.Data.LogEntry> GetLogEntries(DateTime? from, DateTime? to, string filterColumn, string filterValue, string sortColumn, int pageNumber, int pageSize)
        {
            var entries = _logEntries
                .Where(e =>
                    (from == null || e.DateCreated >= from.Value) &&
                    (to == null || e.DateCreated <= to.Value));

            if (!string.IsNullOrEmpty(filterColumn) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterColumn.ToLower().Trim())
                {
                    case "title":
                        entries = entries.Where(e => e.Title.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case "level":
                        entries = entries.Where(e => e.Level.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case "message":
                        entries = entries.Where(e => e.Message.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case "userid":
                        entries = entries.Where(e => e.UserId.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                switch (sortColumn.ToLower().Trim())
                {
                    case "title":
                        entries = entries.OrderBy(e => e.Title);
                        break;
                    case "level":
                        entries = entries.OrderBy(e => e.Level);
                        break;
                    case "message":
                        entries = entries.OrderBy(e => e.Message);
                        break;
                    case "userid":
                        entries = entries.OrderBy(e => e.UserId);
                        break;
                }
            }

            var maxNumberOfEntries = Math.Min(pageSize, maxPageSize);
            entries = entries.Skip((pageNumber - 1) * maxNumberOfEntries).Take(maxNumberOfEntries);

            return entries.AsQueryable();
        }

        public LogEntry InsertLog(LogEntry logEntry)
        {
            _logEntries.Add(logEntry);

            return logEntry;
        }
    }
}
