using System;
using System.Linq;
using ADKLogger.Data;

namespace ADKLogger.Services
{
    public class LogEntryService : ILogEntryService
    {
        private readonly ADKLoggerDbContext _dbContext;
        private int maxPageSize = 50;

        public LogEntryService(ADKLoggerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<LogEntry> GetLogEntries()
        {
            return _dbContext.LogEntries.AsQueryable();
        }

        public IQueryable<LogEntry> GetLogEntries(DateTime? from, DateTime? to, string filterColumn, string filterValue, string sortColumn, int pageNumber, int pageSize)
        {
            var entries = _dbContext.LogEntries
                .Where(e =>
                    (from == null || e.DateCreated >= from.Value) &&
                    (to == null || e.DateCreated <= to.Value));

            //Hard-coding column names for now. If there much more than 4 columns I would use reflection instead. 
            //(Wouldn't want a 30 case switch statement for a 30 column table). My PropertyManagement MVC app on GitHub
            //has an example of using reflection to build up a where clause from user input via a search page.
            //Also shows how to protect against sql injection.


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

            return entries;
        }

        public LogEntry InsertLog(LogEntry logEntry)
        {
            var newEntry = _dbContext.Add(logEntry);
            _dbContext.SaveChanges();

            return logEntry;
        }
    }
}
