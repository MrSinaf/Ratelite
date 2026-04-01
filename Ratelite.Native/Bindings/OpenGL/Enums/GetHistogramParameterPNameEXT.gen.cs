#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum GetHistogramParameterPNameEXT : int
{
	[Obsolete("Deprecated in favour of \"Width\"")]
	HistogramWidth = 0x8026,
	[Obsolete("Deprecated in favour of \"WidthExt\"")]
	HistogramWidthExt = 0x8026,
	[Obsolete("Deprecated in favour of \"Format\"")]
	HistogramFormat = 0x8027,
	[Obsolete("Deprecated in favour of \"FormatExt\"")]
	HistogramFormatExt = 0x8027,
	[Obsolete("Deprecated in favour of \"RedSize\"")]
	HistogramRedSize = 0x8028,
	[Obsolete("Deprecated in favour of \"RedSizeExt\"")]
	HistogramRedSizeExt = 0x8028,
	[Obsolete("Deprecated in favour of \"GreenSize\"")]
	HistogramGreenSize = 0x8029,
	[Obsolete("Deprecated in favour of \"GreenSizeExt\"")]
	HistogramGreenSizeExt = 0x8029,
	[Obsolete("Deprecated in favour of \"BlueSize\"")]
	HistogramBlueSize = 0x802A,
	[Obsolete("Deprecated in favour of \"BlueSizeExt\"")]
	HistogramBlueSizeExt = 0x802A,
	[Obsolete("Deprecated in favour of \"AlphaSize\"")]
	HistogramAlphaSize = 0x802B,
	[Obsolete("Deprecated in favour of \"AlphaSizeExt\"")]
	HistogramAlphaSizeExt = 0x802B,
	[Obsolete("Deprecated in favour of \"LuminanceSize\"")]
	HistogramLuminanceSize = 0x802C,
	[Obsolete("Deprecated in favour of \"LuminanceSizeExt\"")]
	HistogramLuminanceSizeExt = 0x802C,
	[Obsolete("Deprecated in favour of \"Sink\"")]
	HistogramSink = 0x802D,
	[Obsolete("Deprecated in favour of \"SinkExt\"")]
	HistogramSinkExt = 0x802D,
	
	Width = 0x8026,
	
	WidthExt = 0x8026,
	
	Format = 0x8027,
	
	FormatExt = 0x8027,
	
	RedSize = 0x8028,
	
	RedSizeExt = 0x8028,
	
	GreenSize = 0x8029,
	
	GreenSizeExt = 0x8029,
	
	BlueSize = 0x802A,
	
	BlueSizeExt = 0x802A,
	
	AlphaSize = 0x802B,
	
	AlphaSizeExt = 0x802B,
	
	LuminanceSize = 0x802C,
	
	LuminanceSizeExt = 0x802C,
	
	Sink = 0x802D,
	
	SinkExt = 0x802D
}