#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum MapAttribParameterNV : int
{
	[Obsolete("Deprecated in favour of \"UOrderNV\"")]
	MapAttribUOrderNV = 0x86C3,
	[Obsolete("Deprecated in favour of \"VOrderNV\"")]
	MapAttribVOrderNV = 0x86C4,
	
	UOrderNV = 0x86C3,
	
	VOrderNV = 0x86C4
}