namespace Installer.Common.Logger;

public class Loggger : ILogger
{
    private readonly string _loggerName;

    public Loggger(string loggerName)
    {
        _loggerName = loggerName;
    }

    public void Debug(string message)
    {
        WriteMessage("|DEBUG: " + message);
    }

    public void Debug(Exception exception, string message)
    {
        WriteMessage(exception, "|DEBUG: " + message);
    }

    public void Error(string message)
    {
        WriteMessage("|ERROR: " + message);
    }

    public void Error(Exception exception, string message)
    {
        WriteMessage(exception, "|ERROR: " + message);
    }

    public void Info(string message)
    {
        WriteMessage("|INFO: " + message);
    }

    public void Info(Exception exception, string message)
    {
        WriteMessage(exception, "|INFO: " + message);
    }

    public void Trace(string message)
    {
        WriteMessage("|TRACE: " + message);
    }

    public void Trace(Exception exception, string message)
    {
        WriteMessage(exception, "|TRACE: " + message);
    }

    public void Warn(string message)
    {
        WriteMessage("|WARN: " + message);
    }

    public void Warn(Exception exception, string message)
    {
        WriteMessage(exception, "|WARN: " + message);
    }

    private void WriteMessage(string message)
    {
        LogManager.WriteMessage(_loggerName + message);
    }

    private void WriteMessage(Exception exception, string message)
    {
        LogManager.WriteMessage(_loggerName + message);
        LogManager.WriteMessage(exception.ToString());
    }
}