using System;
using System.Linq;
using ADKLogger.Data;

namespace ADKLogger.Services
{
    public interface ILogEntryService
    {
        IQueryable<LogEntry> GetLogEntries();
        IQueryable<LogEntry> GetLogEntries(DateTime? from, DateTime? to, string filterColumn, string filterValue, string sortColumn, int pageNumber, int pageSize);
        LogEntry InsertLog(LogEntry logEntry);
    }
}
