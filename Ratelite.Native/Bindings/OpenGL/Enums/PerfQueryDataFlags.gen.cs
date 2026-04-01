#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PerfQueryDataFlags : int
{
	[Obsolete("Deprecated in favour of \"DonotFlushIntel\"")]
	PerfqueryDonotFlushIntel = 0x83F9,
	[Obsolete("Deprecated in favour of \"FlushIntel\"")]
	PerfqueryFlushIntel = 0x83FA,
	[Obsolete("Deprecated in favour of \"WaitIntel\"")]
	PerfqueryWaitIntel = 0x83FB,
	
	DonotFlushIntel = 0x83F9,
	
	FlushIntel = 0x83FA,
	
	WaitIntel = 0x83FB
}