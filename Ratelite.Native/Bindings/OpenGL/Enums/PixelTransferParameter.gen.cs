#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PixelTransferParameter : int
{
	[Obsolete("Deprecated in favour of \"ConvolutionRedScale\"")]
	PostConvolutionRedScale = 0x801C,
	[Obsolete("Deprecated in favour of \"ConvolutionRedScaleExt\"")]
	PostConvolutionRedScaleExt = 0x801C,
	[Obsolete("Deprecated in favour of \"ConvolutionGreenScale\"")]
	PostConvolutionGreenScale = 0x801D,
	[Obsolete("Deprecated in favour of \"ConvolutionGreenScaleExt\"")]
	PostConvolutionGreenScaleExt = 0x801D,
	[Obsolete("Deprecated in favour of \"ConvolutionBlueScale\"")]
	PostConvolutionBlueScale = 0x801E,
	[Obsolete("Deprecated in favour of \"ConvolutionBlueScaleExt\"")]
	PostConvolutionBlueScaleExt = 0x801E,
	[Obsolete("Deprecated in favour of \"ConvolutionAlphaScale\"")]
	PostConvolutionAlphaScale = 0x801F,
	[Obsolete("Deprecated in favour of \"ConvolutionAlphaScaleExt\"")]
	PostConvolutionAlphaScaleExt = 0x801F,
	[Obsolete("Deprecated in favour of \"ConvolutionRedBias\"")]
	PostConvolutionRedBias = 0x8020,
	[Obsolete("Deprecated in favour of \"ConvolutionRedBiasExt\"")]
	PostConvolutionRedBiasExt = 0x8020,
	[Obsolete("Deprecated in favour of \"ConvolutionGreenBias\"")]
	PostConvolutionGreenBias = 0x8021,
	[Obsolete("Deprecated in favour of \"ConvolutionGreenBiasExt\"")]
	PostConvolutionGreenBiasExt = 0x8021,
	[Obsolete("Deprecated in favour of \"ConvolutionBlueBias\"")]
	PostConvolutionBlueBias = 0x8022,
	[Obsolete("Deprecated in favour of \"ConvolutionBlueBiasExt\"")]
	PostConvolutionBlueBiasExt = 0x8022,
	[Obsolete("Deprecated in favour of \"ConvolutionAlphaBias\"")]
	PostConvolutionAlphaBias = 0x8023,
	[Obsolete("Deprecated in favour of \"ConvolutionAlphaBiasExt\"")]
	PostConvolutionAlphaBiasExt = 0x8023,
	[Obsolete("Deprecated in favour of \"ColorMatrixRedScale\"")]
	PostColorMatrixRedScale = 0x80B4,
	[Obsolete("Deprecated in favour of \"ColorMatrixRedScaleSgi\"")]
	PostColorMatrixRedScaleSgi = 0x80B4,
	[Obsolete("Deprecated in favour of \"ColorMatrixGreenScale\"")]
	PostColorMatrixGreenScale = 0x80B5,
	[Obsolete("Deprecated in favour of \"ColorMatrixGreenScaleSgi\"")]
	PostColorMatrixGreenScaleSgi = 0x80B5,
	[Obsolete("Deprecated in favour of \"ColorMatrixBlueScale\"")]
	PostColorMatrixBlueScale = 0x80B6,
	[Obsolete("Deprecated in favour of \"ColorMatrixBlueScaleSgi\"")]
	PostColorMatrixBlueScaleSgi = 0x80B6,
	[Obsolete("Deprecated in favour of \"ColorMatrixAlphaScale\"")]
	PostColorMatrixAlphaScale = 0x80B7,
	[Obsolete("Deprecated in favour of \"ColorMatrixAlphaScaleSgi\"")]
	PostColorMatrixAlphaScaleSgi = 0x80B7,
	[Obsolete("Deprecated in favour of \"ColorMatrixRedBias\"")]
	PostColorMatrixRedBias = 0x80B8,
	[Obsolete("Deprecated in favour of \"ColorMatrixRedBiasSgi\"")]
	PostColorMatrixRedBiasSgi = 0x80B8,
	[Obsolete("Deprecated in favour of \"ColorMatrixGreenBias\"")]
	PostColorMatrixGreenBias = 0x80B9,
	[Obsolete("Deprecated in favour of \"ColorMatrixGreenBiasSgi\"")]
	PostColorMatrixGreenBiasSgi = 0x80B9,
	[Obsolete("Deprecated in favour of \"ColorMatrixBlueBias\"")]
	PostColorMatrixBlueBias = 0x80BA,
	[Obsolete("Deprecated in favour of \"ColorMatrixBlueBiasSgi\"")]
	PostColorMatrixBlueBiasSgi = 0x80BA,
	[Obsolete("Deprecated in favour of \"ColorMatrixAlphaBias\"")]
	PostColorMatrixAlphaBias = 0x80BB,
	[Obsolete("Deprecated in favour of \"ColorMatrixAlphaBiasSgi\"")]
	PostColorMatrixAlphaBiasSgi = 0x80BB,
	
	ConvolutionRedScale = 0x801C,
	
	ConvolutionRedScaleExt = 0x801C,
	
	ConvolutionGreenScale = 0x801D,
	
	ConvolutionGreenScaleExt = 0x801D,
	
	ConvolutionBlueScale = 0x801E,
	
	ConvolutionBlueScaleExt = 0x801E,
	
	ConvolutionAlphaScale = 0x801F,
	
	ConvolutionAlphaScaleExt = 0x801F,
	
	ConvolutionRedBias = 0x8020,
	
	ConvolutionRedBiasExt = 0x8020,
	
	ConvolutionGreenBias = 0x8021,
	
	ConvolutionGreenBiasExt = 0x8021,
	
	ConvolutionBlueBias = 0x8022,
	
	ConvolutionBlueBiasExt = 0x8022,
	
	ConvolutionAlphaBias = 0x8023,
	
	ConvolutionAlphaBiasExt = 0x8023,
	
	ColorMatrixRedScale = 0x80B4,
	
	ColorMatrixRedScaleSgi = 0x80B4,
	
	ColorMatrixGreenScale = 0x80B5,
	
	ColorMatrixGreenScaleSgi = 0x80B5,
	
	ColorMatrixBlueScale = 0x80B6,
	
	ColorMatrixBlueScaleSgi = 0x80B6,
	
	ColorMatrixAlphaScale = 0x80B7,
	
	ColorMatrixAlphaScaleSgi = 0x80B7,
	
	ColorMatrixRedBias = 0x80B8,
	
	ColorMatrixRedBiasSgi = 0x80B8,
	
	ColorMatrixGreenBias = 0x80B9,
	
	ColorMatrixGreenBiasSgi = 0x80B9,
	
	ColorMatrixBlueBias = 0x80BA,
	
	ColorMatrixBlueBiasSgi = 0x80BA,
	
	ColorMatrixAlphaBias = 0x80BB,
	
	ColorMatrixAlphaBiasSgi = 0x80BB
}