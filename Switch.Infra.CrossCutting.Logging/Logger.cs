using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Switch.Infra.CrossCutting.Logging
{
    public class Logger : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new InternalLogger();
        }

        public void Dispose()
        {
            
        }

        private class InternalLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
               
                System.IO.File.AppendAllText(@"c:\temp\log.txt", formatter(state, exception));
                Console.WriteLine(formatter(state, exception));
            }
        }
    }
}
