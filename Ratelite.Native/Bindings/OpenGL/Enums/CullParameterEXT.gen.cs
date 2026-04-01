#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum CullParameterEXT : int
{
	[Obsolete("Deprecated in favour of \"EyePositionExt\"")]
	CullVertexEyePositionExt = 0x81AB,
	[Obsolete("Deprecated in favour of \"ObjectPositionExt\"")]
	CullVertexObjectPositionExt = 0x81AC,
	
	EyePositionExt = 0x81AB,
	
	ObjectPositionExt = 0x81AC
}