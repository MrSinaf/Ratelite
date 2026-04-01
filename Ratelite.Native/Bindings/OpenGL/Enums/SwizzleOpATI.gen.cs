#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SwizzleOpATI : int
{
	[Obsolete("Deprecated in favour of \"StrAti\"")]
	SwizzleStrAti = 0x8976,
	[Obsolete("Deprecated in favour of \"StqAti\"")]
	SwizzleStqAti = 0x8977,
	[Obsolete("Deprecated in favour of \"StrDRAti\"")]
	SwizzleStrDRAti = 0x8978,
	[Obsolete("Deprecated in favour of \"StqDQAti\"")]
	SwizzleStqDQAti = 0x8979,
	
	StrAti = 0x8976,
	
	StqAti = 0x8977,
	
	StrDRAti = 0x8978,
	
	StqDQAti = 0x8979
}