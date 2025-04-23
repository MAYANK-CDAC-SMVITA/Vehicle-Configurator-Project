using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Vehicle_Config_DotNet_.Logging
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbLoggerProvider(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_scopeFactory);
        }

        public void Dispose() { }
    }
}
