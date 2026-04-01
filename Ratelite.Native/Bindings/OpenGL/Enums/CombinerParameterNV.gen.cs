#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum CombinerParameterNV : int
{
	[Obsolete("Deprecated in favour of \"InputNV\"")]
	CombinerInputNV = 0x8542,
	[Obsolete("Deprecated in favour of \"MappingNV\"")]
	CombinerMappingNV = 0x8543,
	[Obsolete("Deprecated in favour of \"ComponentUsageNV\"")]
	CombinerComponentUsageNV = 0x8544,
	
	InputNV = 0x8542,
	
	MappingNV = 0x8543,
	
	ComponentUsageNV = 0x8544
}