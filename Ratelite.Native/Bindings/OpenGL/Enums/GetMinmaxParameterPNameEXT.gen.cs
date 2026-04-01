#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum GetMinmaxParameterPNameEXT : int
{
	[Obsolete("Deprecated in favour of \"Format\"")]
	MinmaxFormat = 0x802F,
	[Obsolete("Deprecated in favour of \"FormatExt\"")]
	MinmaxFormatExt = 0x802F,
	[Obsolete("Deprecated in favour of \"Sink\"")]
	MinmaxSink = 0x8030,
	[Obsolete("Deprecated in favour of \"SinkExt\"")]
	MinmaxSinkExt = 0x8030,
	
	Format = 0x802F,
	
	FormatExt = 0x802F,
	Sink = 0x8030,
	
	SinkExt = 0x8030
}