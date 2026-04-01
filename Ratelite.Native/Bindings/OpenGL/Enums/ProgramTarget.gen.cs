#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ProgramTarget : int
{
	TextFragmentShaderAti = 0x8200,
	
	VertexProgramArb = 0x8620,
	
	FragmentProgramArb = 0x8804,
	
	TessControlProgramNV = 0x891E,
	
	TessEvaluationProgramNV = 0x891F,
	
	GeometryProgramNV = 0x8C26,
	
	ComputeProgramNV = 0x90FB
}