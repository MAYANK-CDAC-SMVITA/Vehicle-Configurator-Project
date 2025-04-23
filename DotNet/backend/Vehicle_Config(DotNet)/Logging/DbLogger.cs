using Microsoft.Extensions.DependencyInjection;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;
namespace Vehicle_Config_DotNet_.Logging
{
    public class DbLogger : ILogger
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbLogger(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null) return;

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ProjectContext>();

                var logEntry = new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    LogLevel = logLevel,
                    Message = formatter(state, exception),
                    Exception = exception?.ToString() ?? "" // Store empty string if null
                };

                try
                {
                    context.LogEntries.Add(logEntry);
                    await context.SaveChangesAsync(); // Async to prevent blocking
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to log to database: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
            }
        }
    }
}
