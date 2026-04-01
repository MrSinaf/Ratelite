#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum FenceParameterNameNV : int
{
	[Obsolete("Deprecated in favour of \"StatusNV\"")]
	FenceStatusNV = 0x84F3,
	[Obsolete("Deprecated in favour of \"ConditionNV\"")]
	FenceConditionNV = 0x84F4,
	
	StatusNV = 0x84F3,
	
	ConditionNV = 0x84F4
}