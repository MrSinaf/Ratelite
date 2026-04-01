#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum RenderbufferParameterName : int
{
	[Obsolete("Deprecated in favour of \"CoverageSamplesNV\"")]
	RenderbufferCoverageSamplesNV = 0x8CAB,
	[Obsolete("Deprecated in favour of \"Samples\"")]
	RenderbufferSamples = 0x8CAB,
	[Obsolete("Deprecated in favour of \"SamplesAngle\"")]
	RenderbufferSamplesAngle = 0x8CAB,
	[Obsolete("Deprecated in favour of \"SamplesApple\"")]
	RenderbufferSamplesApple = 0x8CAB,
	[Obsolete("Deprecated in favour of \"SamplesExt\"")]
	RenderbufferSamplesExt = 0x8CAB,
	[Obsolete("Deprecated in favour of \"SamplesNV\"")]
	RenderbufferSamplesNV = 0x8CAB,
	[Obsolete("Deprecated in favour of \"Width\"")]
	RenderbufferWidth = 0x8D42,
	[Obsolete("Deprecated in favour of \"WidthExt\"")]
	RenderbufferWidthExt = 0x8D42,
	[Obsolete("Deprecated in favour of \"WidthOes\"")]
	RenderbufferWidthOes = 0x8D42,
	[Obsolete("Deprecated in favour of \"Height\"")]
	RenderbufferHeight = 0x8D43,
	[Obsolete("Deprecated in favour of \"HeightExt\"")]
	RenderbufferHeightExt = 0x8D43,
	[Obsolete("Deprecated in favour of \"HeightOes\"")]
	RenderbufferHeightOes = 0x8D43,
	[Obsolete("Deprecated in favour of \"InternalFormat\"")]
	RenderbufferInternalFormat = 0x8D44,
	[Obsolete("Deprecated in favour of \"InternalFormatExt\"")]
	RenderbufferInternalFormatExt = 0x8D44,
	[Obsolete("Deprecated in favour of \"InternalFormatOes\"")]
	RenderbufferInternalFormatOes = 0x8D44,
	[Obsolete("Deprecated in favour of \"RedSize\"")]
	RenderbufferRedSize = 0x8D50,
	[Obsolete("Deprecated in favour of \"RedSizeExt\"")]
	RenderbufferRedSizeExt = 0x8D50,
	[Obsolete("Deprecated in favour of \"RedSizeOes\"")]
	RenderbufferRedSizeOes = 0x8D50,
	[Obsolete("Deprecated in favour of \"GreenSize\"")]
	RenderbufferGreenSize = 0x8D51,
	[Obsolete("Deprecated in favour of \"GreenSizeExt\"")]
	RenderbufferGreenSizeExt = 0x8D51,
	[Obsolete("Deprecated in favour of \"GreenSizeOes\"")]
	RenderbufferGreenSizeOes = 0x8D51,
	[Obsolete("Deprecated in favour of \"BlueSize\"")]
	RenderbufferBlueSize = 0x8D52,
	[Obsolete("Deprecated in favour of \"BlueSizeExt\"")]
	RenderbufferBlueSizeExt = 0x8D52,
	[Obsolete("Deprecated in favour of \"BlueSizeOes\"")]
	RenderbufferBlueSizeOes = 0x8D52,
	[Obsolete("Deprecated in favour of \"AlphaSize\"")]
	RenderbufferAlphaSize = 0x8D53,
	[Obsolete("Deprecated in favour of \"AlphaSizeExt\"")]
	RenderbufferAlphaSizeExt = 0x8D53,
	[Obsolete("Deprecated in favour of \"AlphaSizeOes\"")]
	RenderbufferAlphaSizeOes = 0x8D53,
	[Obsolete("Deprecated in favour of \"DepthSize\"")]
	RenderbufferDepthSize = 0x8D54,
	[Obsolete("Deprecated in favour of \"DepthSizeExt\"")]
	RenderbufferDepthSizeExt = 0x8D54,
	[Obsolete("Deprecated in favour of \"DepthSizeOes\"")]
	RenderbufferDepthSizeOes = 0x8D54,
	[Obsolete("Deprecated in favour of \"StencilSize\"")]
	RenderbufferStencilSize = 0x8D55,
	[Obsolete("Deprecated in favour of \"StencilSizeExt\"")]
	RenderbufferStencilSizeExt = 0x8D55,
	[Obsolete("Deprecated in favour of \"StencilSizeOes\"")]
	RenderbufferStencilSizeOes = 0x8D55,
	[Obsolete("Deprecated in favour of \"ColorSamplesNV\"")]
	RenderbufferColorSamplesNV = 0x8E10,
	[Obsolete("Deprecated in favour of \"SamplesImg\"")]
	RenderbufferSamplesImg = 0x9133,
	[Obsolete("Deprecated in favour of \"StorageSamplesAmd\"")]
	RenderbufferStorageSamplesAmd = 0x91B2,
	
	CoverageSamplesNV = 0x8CAB,
	
	Samples = 0x8CAB,
	
	SamplesAngle = 0x8CAB,
	
	SamplesApple = 0x8CAB,
	
	SamplesExt = 0x8CAB,
	
	SamplesNV = 0x8CAB,
	
	Width = 0x8D42,
	
	WidthExt = 0x8D42,
	
	WidthOes = 0x8D42,
	
	Height = 0x8D43,
	
	HeightExt = 0x8D43,
	
	HeightOes = 0x8D43,
	
	InternalFormat = 0x8D44,
	
	InternalFormatExt = 0x8D44,
	
	InternalFormatOes = 0x8D44,
	
	RedSize = 0x8D50,
	
	RedSizeExt = 0x8D50,
	
	RedSizeOes = 0x8D50,
	
	GreenSize = 0x8D51,
	
	GreenSizeExt = 0x8D51,
	
	GreenSizeOes = 0x8D51,
	
	BlueSize = 0x8D52,
	
	BlueSizeExt = 0x8D52,
	
	BlueSizeOes = 0x8D52,
	
	AlphaSize = 0x8D53,
	
	AlphaSizeExt = 0x8D53,
	
	AlphaSizeOes = 0x8D53,
	
	DepthSize = 0x8D54,
	
	DepthSizeExt = 0x8D54,
	
	DepthSizeOes = 0x8D54,
	
	StencilSize = 0x8D55,
	
	StencilSizeExt = 0x8D55,
	
	StencilSizeOes = 0x8D55,
	
	ColorSamplesNV = 0x8E10,
	
	SamplesImg = 0x9133,
	
	StorageSamplesAmd = 0x91B2
}