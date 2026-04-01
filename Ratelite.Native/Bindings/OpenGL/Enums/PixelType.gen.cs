#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PixelType : int
{
	Byte = 0x1400,
	
	UnsignedByte = 0x1401,
	Short = 0x1402,
	
	UnsignedShort = 0x1403,
	Int = 0x1404,
	
	UnsignedInt = 0x1405,
	Float = 0x1406,
	HalfFloat = 0x140B,
	
	HalfFloatArb = 0x140B,
	
	HalfFloatNV = 0x140B,
	HalfApple = 0x140B,
	
	UnsignedByte332 = 0x8032,
	
	UnsignedByte332Ext = 0x8032,
	
	UnsignedShort4444 = 0x8033,
	
	UnsignedShort4444Ext = 0x8033,
	
	UnsignedShort5551 = 0x8034,
	
	UnsignedShort5551Ext = 0x8034,
	
	UnsignedInt8888 = 0x8035,
	
	UnsignedInt8888Ext = 0x8035,
	
	UnsignedInt1010102 = 0x8036,
	
	UnsignedInt1010102Ext = 0x8036,
	
	UnsignedByte233Rev = 0x8362,
	
	UnsignedByte233RevExt = 0x8362,
	
	UnsignedShort565 = 0x8363,
	
	UnsignedShort565Ext = 0x8363,
	
	UnsignedShort565Rev = 0x8364,
	
	UnsignedShort565RevExt = 0x8364,
	
	UnsignedShort4444Rev = 0x8365,
	
	UnsignedShort4444RevExt = 0x8365,
	
	UnsignedShort4444RevImg = 0x8365,
	
	UnsignedShort1555Rev = 0x8366,
	
	UnsignedShort1555RevExt = 0x8366,
	
	UnsignedInt8888Rev = 0x8367,
	
	UnsignedInt8888RevExt = 0x8367,
	
	UnsignedInt2101010Rev = 0x8368,
	
	UnsignedInt2101010RevExt = 0x8368,
	
	UnsignedInt248 = 0x84FA,
	
	UnsignedInt248Ext = 0x84FA,
	
	UnsignedInt248NV = 0x84FA,
	
	UnsignedInt248Oes = 0x84FA,
	
	UnsignedInt10f11f11fRev = 0x8C3B,
	
	UnsignedInt10f11f11fRevApple = 0x8C3B,
	
	UnsignedInt10f11f11fRevExt = 0x8C3B,
	
	UnsignedInt5999Rev = 0x8C3E,
	
	UnsignedInt5999RevApple = 0x8C3E,
	
	UnsignedInt5999RevExt = 0x8C3E,
	
	Float32UnsignedInt248Rev = 0x8DAD,
	
	Float32UnsignedInt248RevNV = 0x8DAD
}