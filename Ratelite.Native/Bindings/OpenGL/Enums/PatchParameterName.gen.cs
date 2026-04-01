#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PatchParameterName : int
{
	[Obsolete("Deprecated in favour of \"Vertices\"")]
	PatchVertices = 0x8E72,
	[Obsolete("Deprecated in favour of \"DefaultInnerLevel\"")]
	PatchDefaultInnerLevel = 0x8E73,
	[Obsolete("Deprecated in favour of \"DefaultOuterLevel\"")]
	PatchDefaultOuterLevel = 0x8E74,
	
	Vertices = 0x8E72,
	
	DefaultInnerLevel = 0x8E73,
	
	DefaultOuterLevel = 0x8E74
}