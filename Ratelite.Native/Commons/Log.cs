namespace Ratelite;

public static class Log
{
	public enum Level { Verbose, Debug, Info, Warning, Error, Fatal }
	
	private static readonly Lock lockObj = new ();
	
#if DEBUG
	public static Level verbosityLevel = Level.Verbose;
#else
	public static Level verbosityLevel = Level.Info;
#endif
	
	public static void Write(object content, Level level = Level.Debug)
	{
		if (verbosityLevel > level)
			return;
		
		lock (lockObj)
		{
			var currentDate = DateTime.Now;
			var (type, color) = level switch
			{
				Level.Verbose => ("VER", ConsoleColor.Blue),
				Level.Debug   => ("DEB", ConsoleColor.Cyan),
				Level.Info    => ("INF", ConsoleColor.Green),
				Level.Warning => ("WAR", ConsoleColor.Yellow),
				Level.Error   => ("ERR", ConsoleColor.Red),
				Level.Fatal   => ("FAT", ConsoleColor.Magenta),
				_             => throw new ArgumentOutOfRangeException(nameof(level), level, null)
			};
			Console.ResetColor();
			Console.Write($"[{currentDate:HH:mm:ss.fff} ");
			Console.ForegroundColor = color;
			Console.Write(type);
			Console.ResetColor();
			Console.Write($"] {content}\n");
		}
	}
	
	public static void Verbose(object content) => Write(content, Level.Verbose);
	public static void Warning(object content) => Write(content, Level.Warning);
	public static void Error(object content)   => Write(content, Level.Error);
	
	public static void Write(Exception e, bool isWarning = false)
		=> Write(e.Message + "\n" + e.StackTrace, isWarning ? Level.Warning : Level.Error);
	
	public static void Write(object content, Exception e, bool isWarning = false)
		=> Write(
			$"{content}\n{e.Message}\n{e.StackTrace}",
			isWarning ? Level.Warning : Level.Error
		);
}