#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ColorTableTarget : int
{
	ColorTable = 0x80D0,
	
	PostConvolutionColorTable = 0x80D1,
	
	PostColorMatrixColorTable = 0x80D2,
	
	ProxyColorTable = 0x80D3,
	
	ProxyPostConvolutionColorTable = 0x80D4,
	
	ProxyPostColorMatrixColorTable = 0x80D5
}