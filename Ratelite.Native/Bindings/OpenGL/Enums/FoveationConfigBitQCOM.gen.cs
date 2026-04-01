#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum FoveationConfigBitQCOM : int
{
	None = 0,
	[Obsolete("Deprecated in favour of \"EnableBitQCom\"")]
	FoveationEnableBitQCom = 0x1,
	[Obsolete("Deprecated in favour of \"ScaledBinMethodBitQCom\"")]
	FoveationScaledBinMethodBitQCom = 0x2,
	[Obsolete("Deprecated in favour of \"SubsampledLayoutMethodBitQCom\"")]
	FoveationSubsampledLayoutMethodBitQCom = 0x4,
	
	EnableBitQCom = 0x1,
	
	ScaledBinMethodBitQCom = 0x2,
	
	SubsampledLayoutMethodBitQCom = 0x4
}