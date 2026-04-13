namespace Ratelite.Sounds.Bindings.StbVorbis.Hebron.Runtime;

internal static class MemoryStats
{
	private static int _allocations;
	
	public static int allocations => _allocations;
	
	internal static void Allocated()
	{
		Interlocked.Increment(ref _allocations);
	}
	
	internal static void Freed()
	{
		Interlocked.Decrement(ref _allocations);
	}
}