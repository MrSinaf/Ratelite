#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SpecialNumbers : int
{
	False = 0x0,
	NoError = 0x0,
	Zero = 0x0,
	None = 0x0,
	NoneOes = 0x0,
	True = 0x1,
	One = 0x1,
	
	InvalidIndex = unchecked((int)0xFFFFFFFF),
	
	AllPixelsAmd = unchecked((int)0xFFFFFFFF),
	
	TimeoutIgnored = unchecked((int)0xFFFFFFFFFFFFFFFF),
	
	TimeoutIgnoredApple = unchecked((int)0xFFFFFFFFFFFFFFFF),
	
	VersionESCL10 = 0x1,
	
	VersionESCM11 = 0x1,
	
	VersionESCL11 = 0x1,
	
	UuidSizeExt = 0x16,
	
	LuidSizeExt = 0x8
}