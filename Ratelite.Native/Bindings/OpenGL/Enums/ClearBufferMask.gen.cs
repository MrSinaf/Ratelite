#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum ClearBufferMask : int
{
	None = 0,
	
	DepthBufferBit = 0x100,
	
	StencilBufferBit = 0x400,
	
	ColorBufferBit = 0x4000,
	
	CoverageBufferBitNV = 0x8000
}