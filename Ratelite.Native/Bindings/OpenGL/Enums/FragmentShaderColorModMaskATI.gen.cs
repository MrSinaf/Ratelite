#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum FragmentShaderColorModMaskATI : int
{
	None = 0,
	Gl2XBitAti = 0x1,
	
	CompBitAti = 0x2,
	
	NegateBitAti = 0x4,
	
	BiasBitAti = 0x8
}