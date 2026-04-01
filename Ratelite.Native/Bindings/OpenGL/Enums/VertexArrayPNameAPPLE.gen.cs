#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum VertexArrayPNameAPPLE : int
{
	[Obsolete("Deprecated in favour of \"ClientApple\"")]
	StorageClientApple = 0x85B4,
	[Obsolete("Deprecated in favour of \"CachedApple\"")]
	StorageCachedApple = 0x85BE,
	[Obsolete("Deprecated in favour of \"SharedApple\"")]
	StorageSharedApple = 0x85BF,
	
	ClientApple = 0x85B4,
	
	CachedApple = 0x85BE,
	
	SharedApple = 0x85BF
}