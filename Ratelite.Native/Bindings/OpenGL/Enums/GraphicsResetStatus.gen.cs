#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum GraphicsResetStatus : int
{
	NoError = 0x0,
	
	GuiltyContextReset = 0x8253,
	
	InnocentContextReset = 0x8254,
	
	UnknownContextReset = 0x8255
}