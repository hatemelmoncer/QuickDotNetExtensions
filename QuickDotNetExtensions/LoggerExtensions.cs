using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace QuickDotNetExtensions;

public static class LoggerExtensions
{
    /// <summary>
    /// Logs the start and end of a method, including elapsed time.
    /// Usage: using var scope = logger.LogMethodScope(nameof(MyMethod), LogLevel.Information);
    /// </summary>
    /// <param name="logger">The ILogger instance.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="level">The log level (default: Information).</param>
    /// <returns>IDisposable scope that logs exit and elapsed time.</returns>
    public static IDisposable LogMethodScope(this ILogger logger, string methodName, LogLevel level = LogLevel.Information)
    {
        return new MethodLoggerScope(logger, methodName, level);
    }

    private class MethodLoggerScope : IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _methodName;
        private readonly LogLevel _level;
        private readonly Stopwatch _stopwatch;

        public MethodLoggerScope(ILogger logger, string methodName, LogLevel level)
        {
            _logger = logger;
            _methodName = methodName;
            _level = level;
            _stopwatch = Stopwatch.StartNew();

            _logger.Log(_level, "Entering {MethodName}", _methodName);
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.Log(_level, "Exiting {MethodName} (Elapsed: {ElapsedMilliseconds}ms)", _methodName, _stopwatch.ElapsedMilliseconds);
        }
    }
}