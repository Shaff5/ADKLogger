using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ADKLogger.Services;
using ADKLogger.WebApi.Models;

namespace ADKLogger.WebApi.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogEntryService _logEntryService;

        public LogController(ILogEntryService logEntryService)
        {
            _logEntryService = logEntryService;
        }

        [Route("postlog")]
        [HttpPost]
        public ActionResult PostLog([FromBody] LogEntity logEntity)
        {
            var logEntry = new ADKLogger.Data.LogEntry
            {
                Title = logEntity.Title,
                Level = logEntity.Level,
                Message = logEntity.Message,
                UserId = logEntity.UserId,
                DateCreated = logEntity.DateTimeCreation
            };

            _logEntryService.InsertLog(logEntry);

            return Ok();
        }

        [Route("getreport/user/{userid}")]
        [HttpGet]
        public ActionResult<Statistics> GetReport(string userId, DateTime? from = null, DateTime? to = null)
        {
            var entries = _logEntryService.GetLogEntries()
                .Where(e => e.UserId == userId &&
                    (from == null || e.DateCreated >= from.Value) &&
                    (to == null || e.DateCreated <= to.Value));

            var levelCounts = entries.GroupBy(e => e.Level).ToDictionary(e => e.Key, e => e.Count());

            var total = 0;
            foreach (var kv in levelCounts)
            {
                total += kv.Value;
            }

            return new Statistics
            {
                TotalLogCount = total,
                LevelCounts = levelCounts
            };
        }

        [Route("getlogs")]
        [HttpGet]
        public ActionResult<IEnumerable<LogEntity>> GetLogs(DateTime? from = null, DateTime? to = null, string filterColumn = "", string filterValue = "",
            string sortColumn = "", int pageNumber = 1, int pageSize = 50)
        {
            var entries = _logEntryService.GetLogEntries(from, to, filterColumn, filterValue, sortColumn, pageNumber, pageSize);

            return entries.Select(e => new LogEntity
            {
                Title = e.Title,
                Level = e.Level,
                Message = e.Message,
                UserId = e.UserId,
                DateTimeCreation = e.DateCreated
            }).ToList();
        }
    }
}
