#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum FragmentShaderDestMaskATI : int
{
	RedBitAti = 0x1,
	
	GreenBitAti = 0x2,
	
	BlueBitAti = 0x4,
	None = 0x0
}