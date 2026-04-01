#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ShadingRateCombinerOp : int
{
	[Obsolete("Deprecated in favour of \"KeepExt\"")]
	FragmentShadingRateCombinerOpKeepExt = 0x96D2,
	[Obsolete("Deprecated in favour of \"ReplaceExt\"")]
	FragmentShadingRateCombinerOpReplaceExt = 0x96D3,
	[Obsolete("Deprecated in favour of \"MinExt\"")]
	FragmentShadingRateCombinerOpMinExt = 0x96D4,
	[Obsolete("Deprecated in favour of \"MaxExt\"")]
	FragmentShadingRateCombinerOpMaxExt = 0x96D5,
	[Obsolete("Deprecated in favour of \"MulExt\"")]
	FragmentShadingRateCombinerOpMulExt = 0x96D6,
	
	KeepExt = 0x96D2,
	
	ReplaceExt = 0x96D3,
	
	MinExt = 0x96D4,
	
	MaxExt = 0x96D5,
	
	MulExt = 0x96D6
}