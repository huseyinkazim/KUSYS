﻿namespace KUSYS.Api.Middleware
{
	public class MyCustomLoggerProvider : ILoggerProvider
	{
		public ILogger CreateLogger(string categoryName)
		{
			return new MyCustomLogger();
		}

		public void Dispose()
		{
		}
	}

	public class MyCustomLogger : ILogger
	{
		public IDisposable? BeginScope<TState>(TState state) where TState : notnull
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			var msg = formatter(state, exception);
			var logMesg = $"[{DateTime.Now}] - {msg}";
			Console.WriteLine(logMesg);
		}
	}
}
