using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Installer.Common.Logger;

public class LogManager
{
    private static FileStream? _logStream;
    private static StreamWriter? _logWriter;
    private static object _writeLock = new();

    public static void Initialize(string filePath)
    {
        _logStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        _logWriter = new StreamWriter(_logStream) { AutoFlush = true };
    }

    public static void Dispose()
    {
        lock (_writeLock)
        {
            _logWriter?.Dispose();
        }

        _logStream?.Dispose();
    }

    public static void WriteMessage(string message)
    {
        lock (_writeLock)
        {
            _logWriter?.WriteLine(DateTime.Now.ToLocalTime() + "|" + message);
        }

        if (Debugger.IsAttached)
        {
            Console.WriteLine(message);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ILogger GetLogger()
    {
        string className = new StackFrame(1).GetMethod()?.DeclaringType?.Name ?? throw new InvalidOperationException();
        return GetLogger(className);
    }

    public static ILogger GetLogger(string loggerName)
    {
        return new Loggger(loggerName);
    }
}