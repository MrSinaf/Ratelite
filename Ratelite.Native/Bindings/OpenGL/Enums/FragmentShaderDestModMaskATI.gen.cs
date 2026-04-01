#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum FragmentShaderDestModMaskATI : int
{
	Gl2XBitAti = 0x1,
	Gl4XBitAti = 0x2,
	Gl8XBitAti = 0x4,
	
	HalfBitAti = 0x8,
	
	QuarterBitAti = 0x10,
	
	EighthBitAti = 0x20,
	
	SaturateBitAti = 0x40,
	None = 0x0
}