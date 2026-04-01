#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PerformanceQueryCapsMaskINTEL : int
{
	[Obsolete("Deprecated in favour of \"SingleContextIntel\"")]
	PerfquerySingleContextIntel = 0x0,
	[Obsolete("Deprecated in favour of \"GlobalContextIntel\"")]
	PerfqueryGlobalContextIntel = 0x1,
	
	SingleContextIntel = 0x0,
	
	GlobalContextIntel = 0x1
}