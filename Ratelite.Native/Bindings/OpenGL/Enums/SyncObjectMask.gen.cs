#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SyncObjectMask : int
{
	[Obsolete("Deprecated in favour of \"Bit\"")]
	SyncFlushCommandsBit = 0x1,
	[Obsolete("Deprecated in favour of \"BitApple\"")]
	SyncFlushCommandsBitApple = 0x1,
	
	Bit = 0x1,
	
	BitApple = 0x1
}