#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PixelStoreSubsampleRate : int
{
	[Obsolete("Deprecated in favour of \"Subsample4444Sgix\"")]
	PixelSubsample4444Sgix = 0x85A2,
	[Obsolete("Deprecated in favour of \"Subsample2424Sgix\"")]
	PixelSubsample2424Sgix = 0x85A3,
	[Obsolete("Deprecated in favour of \"Subsample4242Sgix\"")]
	PixelSubsample4242Sgix = 0x85A4,
	
	Subsample4444Sgix = 0x85A2,
	
	Subsample2424Sgix = 0x85A3,
	
	Subsample4242Sgix = 0x85A4
}