#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum CombinerRegisterNV : int
{
	Texture0Arb = 0x84C0,
	
	Texture1Arb = 0x84C1,
	
	PrimaryColorNV = 0x852C,
	
	SecondaryColorNV = 0x852D,
	Spare0NV = 0x852E,
	Spare1NV = 0x852F,
	DiscardNV = 0x8530
}