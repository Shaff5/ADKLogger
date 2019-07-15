using Microsoft.EntityFrameworkCore;

namespace ADKLogger.Data
{
    public class ADKLoggerDbContext :DbContext
    {
        public ADKLoggerDbContext(DbContextOptions<ADKLoggerDbContext> options)
            : base(options)
        {
        }

        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
