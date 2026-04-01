#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum LightTextureModeEXT : int
{
	[Obsolete("Deprecated in favour of \"MaterialExt\"")]
	FragmentMaterialExt = 0x8349,
	[Obsolete("Deprecated in favour of \"NormalExt\"")]
	FragmentNormalExt = 0x834A,
	[Obsolete("Deprecated in favour of \"ColorExt\"")]
	FragmentColorExt = 0x834C,
	[Obsolete("Deprecated in favour of \"DepthExt\"")]
	FragmentDepthExt = 0x8452,
	
	MaterialExt = 0x8349,
	
	NormalExt = 0x834A,
	
	ColorExt = 0x834C,
	
	DepthExt = 0x8452
}