using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Ratelite;

public static class MainThread
{
	public static bool isMainThread => Environment.CurrentManagedThreadId == mainThreadId;
	
	private static readonly ConcurrentQueue<Action> commands = [];
	private static int mainThreadId;
	
	internal static void Init() => mainThreadId = Environment.CurrentManagedThreadId;
	
	// TODO : Peut-être utiliser : [Conditional("DEBUG")]
	public static void Assert([CallerMemberName] string caller = "")
	{
		if (!isMainThread)
			throw new InvalidOperationException(
				string.IsNullOrEmpty(caller)
						? "This method must be called from the MainThread. ( ´･･)ﾉ(._.`)"
						: $"'{caller}' must be called from the MainThread. ( ´･･)ﾉ(._.`)"
			);
	}
	
	public static void Enqueue(Action command) => commands.Enqueue(command);
	
	public static async Task<T?> EnqueueAndWaitAsync<T>(Func<T> command)
	{
		var obj = default(T);
		Enqueue(() => obj = command());
		await Wait();
		
		return obj;
	}
	
	public static void ExecuteAll()
	{
		while (commands.TryDequeue(out var command))
			command();
	}
	
	public static Task Wait()
	{
		var completion = new TaskCompletionSource(
			TaskCreationOptions.RunContinuationsAsynchronously
		);
		
		Enqueue(completion.SetResult);
		return completion.Task;
	}
}