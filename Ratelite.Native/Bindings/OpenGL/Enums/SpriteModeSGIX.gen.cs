#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SpriteModeSGIX : int
{
	[Obsolete("Deprecated in favour of \"AxialSgix\"")]
	SpriteAxialSgix = 0x814C,
	[Obsolete("Deprecated in favour of \"ObjectAlignedSgix\"")]
	SpriteObjectAlignedSgix = 0x814D,
	[Obsolete("Deprecated in favour of \"EyeAlignedSgix\"")]
	SpriteEyeAlignedSgix = 0x814E,
	
	AxialSgix = 0x814C,
	
	ObjectAlignedSgix = 0x814D,
	
	EyeAlignedSgix = 0x814E
}