using Microsoft.Extensions.Logging;

namespace QuickDotNetExtensions.UnitTests;

public class LoggerExtensionsTests
{
    private class TestLogger : ILogger
    {
        public List<(LogLevel Level, string Message, object[] Args)> Logs { get; } = new();

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (state is IReadOnlyList<KeyValuePair<string, object?>> kvps)
            {
                var msg = formatter(state, exception);
                var args = new List<object?>();
                foreach (var kvp in kvps)
                {
                    args.Add(kvp.Value);
                }
                Logs.Add((logLevel, msg, args.ToArray()!));
            }
            else
            {
                Logs.Add((logLevel, formatter(state, exception), Array.Empty<object>()));
            }
        }

        private class NullScope : IDisposable
        {
            public static NullScope Instance { get; } = new();
            public void Dispose() { }
        }
    }

    [Fact]
    public void LoggerExtensions_LogMethodScope_Logs_Enter_And_Exit()
    {
        var logger = new TestLogger();

        using (logger.LogMethodScope("TestMethod", LogLevel.Warning))
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }

        Assert.Equal(2, logger.Logs.Count);

        Assert.Equal(LogLevel.Warning, logger.Logs[0].Level);
        Assert.Contains("Entering", logger.Logs[0].Message);
        Assert.Contains("TestMethod", logger.Logs[0].Message);

        Assert.Equal(LogLevel.Warning, logger.Logs[1].Level);
        Assert.Contains("Exiting", logger.Logs[1].Message);
        Assert.Contains("TestMethod", logger.Logs[1].Message);
        Assert.Contains("Elapsed", logger.Logs[1].Message);
    }

    [Fact]
    public void LoggerExtensions_LogMethodScope_Uses_Default_LogLevel_Information()
    {
        var logger = new TestLogger();

        using (logger.LogMethodScope("DefaultLevelMethod"))
        {
            // No-op
        }

        Assert.Equal(LogLevel.Information, logger.Logs[0].Level);
        Assert.Equal(LogLevel.Information, logger.Logs[1].Level);
    }
}