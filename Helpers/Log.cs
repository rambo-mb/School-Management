namespace SM.Helpers;

public static class Log
{
	private static readonly string _logPath = "log.txt";

	public static void Write(string message)
	{
		string currentTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
		Console.WriteLine($"[{currentTime}] {message}");
		using StreamWriter writer = new StreamWriter(_logPath, append: true);
		writer.WriteLine($"[{currentTime}] {message}");
	}
}