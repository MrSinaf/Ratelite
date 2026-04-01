#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ShaderBinaryFormat : int
{
	SgxBinaryImg = 0x8C0A,
	
	MaliShaderBinaryArm = 0x8F60,
	
	ShaderBinaryViv = 0x8FC4,
	
	ShaderBinaryDmp = 0x9250,
	
	GccsoShaderBinaryFJ = 0x9260,
	
	ShaderBinaryFormatSpirV = 0x9551,
	
	HuaweiShaderBinary = 0x9770
}