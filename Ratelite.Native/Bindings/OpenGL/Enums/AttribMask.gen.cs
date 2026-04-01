#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum AttribMask : int
{
	None = 0,
	
	DepthBufferBit = 0x100,
	
	StencilBufferBit = 0x400,
	
	ColorBufferBit = 0x4000,
	
	MultisampleBitArb = 0x20000000,
	
	MultisampleBitExt = 0x20000000,
	
	MultisampleBit3Dfx = 0x20000000
}