#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ArrayObjectPNameATI : int
{
	[Obsolete("Deprecated in favour of \"SizeAti\"")]
	ObjectBufferSizeAti = 0x8764,
	[Obsolete("Deprecated in favour of \"UsageAti\"")]
	ObjectBufferUsageAti = 0x8765,
	
	SizeAti = 0x8764,
	
	UsageAti = 0x8765
}