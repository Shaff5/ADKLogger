using System.Collections.Generic;
using System.Linq;
using ADKLogger.WebApi.Controllers;
using ADKLogger.WebApi.Models;
using ADKLogger.Data;
using ADKLogger.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ADKLogger.Tests
{
    public class LogControllerTests
    {
        LogController _controller;
        ILogEntryService _service;

        public LogControllerTests()
        {
            _service = new LogEntryServiceFake();
            _controller = new LogController(_service);
        }

        [Fact]
        public void GetReport_WhenCalled_ReturnsOkResult()
        {
            var actionResult = _controller.GetReport("123");

            Assert.IsType<ActionResult<Statistics>>(actionResult);
        }

        [Fact]
        public void GetReport_WhenCalled_ReturnsCorrectStatistics()
        {
            var value = _controller.GetReport("123").Value;

            var statistics = Assert.IsType<Statistics>(value);
            Assert.Equal(3, statistics.TotalLogCount);
            Assert.Equal(1, statistics.LevelCounts["Warning"]);
            Assert.Equal(1, statistics.LevelCounts["Informational"]);
            Assert.Equal(1, statistics.LevelCounts["Error"]);
        }

        [Fact]
        public void GetLogs_WhenCalled_ReturnsCorrectLogEntries()
        {
            var entries = _controller.GetLogs(null, null, "level", "warning").Value;

            Assert.Equal(4, entries.Count());
        }
    }
}
