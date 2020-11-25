using System;
using FewBox.SDK.Mail;
using Microsoft.Extensions.Logging;

public class ColorConsoleLogger : ILogger<MQMailService>
{
    public IDisposable BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        Console.WriteLine("Custom Logger.");
    }
}