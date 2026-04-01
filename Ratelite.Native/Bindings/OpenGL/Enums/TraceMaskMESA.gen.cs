#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum TraceMaskMESA : int
{
	None = 0,
	[Obsolete("Deprecated in favour of \"OperationsBitMesa\"")]
	TraceOperationsBitMesa = 0x1,
	[Obsolete("Deprecated in favour of \"PrimitivesBitMesa\"")]
	TracePrimitivesBitMesa = 0x2,
	[Obsolete("Deprecated in favour of \"ArraysBitMesa\"")]
	TraceArraysBitMesa = 0x4,
	[Obsolete("Deprecated in favour of \"TexturesBitMesa\"")]
	TraceTexturesBitMesa = 0x8,
	[Obsolete("Deprecated in favour of \"PixelsBitMesa\"")]
	TracePixelsBitMesa = 0x10,
	[Obsolete("Deprecated in favour of \"ErrorsBitMesa\"")]
	TraceErrorsBitMesa = 0x20,
	[Obsolete("Deprecated in favour of \"AllBitsMesa\"")]
	TraceAllBitsMesa = 0xFFFF,
	
	OperationsBitMesa = 0x1,
	
	PrimitivesBitMesa = 0x2,
	
	ArraysBitMesa = 0x4,
	
	TexturesBitMesa = 0x8,
	
	PixelsBitMesa = 0x10,
	
	ErrorsBitMesa = 0x20,
	
	AllBitsMesa = 0xFFFF
}