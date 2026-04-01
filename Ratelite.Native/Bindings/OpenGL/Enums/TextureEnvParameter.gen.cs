#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum TextureEnvParameter : int
{
	TextureLodBias = 0x8501,
	CombineArb = 0x8570,
	CombineExt = 0x8570,
	
	CombineRgbArb = 0x8571,
	
	CombineRgbExt = 0x8571,
	
	CombineAlphaArb = 0x8572,
	
	CombineAlphaExt = 0x8572,
	
	RgbScaleArb = 0x8573,
	
	RgbScaleExt = 0x8573,
	
	AddSignedArb = 0x8574,
	
	AddSignedExt = 0x8574,
	
	InterpolateArb = 0x8575,
	
	InterpolateExt = 0x8575,
	
	ConstantArb = 0x8576,
	
	ConstantExt = 0x8576,
	ConstantNV = 0x8576,
	
	PrimaryColorArb = 0x8577,
	
	PrimaryColorExt = 0x8577,
	
	PreviousArb = 0x8578,
	
	PreviousExt = 0x8578,
	
	Source0RgbArb = 0x8580,
	
	Source0RgbExt = 0x8580,
	
	Source1RgbArb = 0x8581,
	
	Source1RgbExt = 0x8581,
	
	Source2RgbArb = 0x8582,
	
	Source2RgbExt = 0x8582,
	
	Source3RgbNV = 0x8583,
	
	Source0AlphaArb = 0x8588,
	
	Source0AlphaExt = 0x8588,
	
	Source1AlphaArb = 0x8589,
	
	Source1AlphaExt = 0x8589,
	Src1Alpha = 0x8589,
	
	Src1AlphaExt = 0x8589,
	
	Source2AlphaArb = 0x858A,
	
	Source2AlphaExt = 0x858A,
	
	Source3AlphaNV = 0x858B,
	
	Operand0RgbArb = 0x8590,
	
	Operand0RgbExt = 0x8590,
	
	Operand1RgbArb = 0x8591,
	
	Operand1RgbExt = 0x8591,
	
	Operand2RgbArb = 0x8592,
	
	Operand2RgbExt = 0x8592,
	
	Operand3RgbNV = 0x8593,
	
	Operand0AlphaArb = 0x8598,
	
	Operand0AlphaExt = 0x8598,
	
	Operand1AlphaArb = 0x8599,
	
	Operand1AlphaExt = 0x8599,
	
	Operand2AlphaArb = 0x859A,
	
	Operand2AlphaExt = 0x859A,
	
	Operand3AlphaNV = 0x859B
}