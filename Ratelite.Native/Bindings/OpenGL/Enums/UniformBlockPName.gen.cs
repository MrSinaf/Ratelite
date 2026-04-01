#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum UniformBlockPName : int
{
	[Obsolete("Deprecated in favour of \"ReferencedByTessControlShader\"")]
	UniformBlockReferencedByTessControlShader = 0x84F0,
	[Obsolete("Deprecated in favour of \"ReferencedByTessEvaluationShader\"")]
	UniformBlockReferencedByTessEvaluationShader = 0x84F1,
	[Obsolete("Deprecated in favour of \"Binding\"")]
	UniformBlockBinding = 0x8A3F,
	[Obsolete("Deprecated in favour of \"DataSize\"")]
	UniformBlockDataSize = 0x8A40,
	[Obsolete("Deprecated in favour of \"NameLength\"")]
	UniformBlockNameLength = 0x8A41,
	[Obsolete("Deprecated in favour of \"ActiveUniforms\"")]
	UniformBlockActiveUniforms = 0x8A42,
	[Obsolete("Deprecated in favour of \"ActiveUniformIndices\"")]
	UniformBlockActiveUniformIndices = 0x8A43,
	[Obsolete("Deprecated in favour of \"ReferencedByVertexShader\"")]
	UniformBlockReferencedByVertexShader = 0x8A44,
	[Obsolete("Deprecated in favour of \"ReferencedByGeometryShader\"")]
	UniformBlockReferencedByGeometryShader = 0x8A45,
	[Obsolete("Deprecated in favour of \"ReferencedByFragmentShader\"")]
	UniformBlockReferencedByFragmentShader = 0x8A46,
	[Obsolete("Deprecated in favour of \"ReferencedByComputeShader\"")]
	UniformBlockReferencedByComputeShader = 0x90EC,
	
	ReferencedByTessControlShader = 0x84F0,
	
	ReferencedByTessEvaluationShader = 0x84F1,
	
	Binding = 0x8A3F,
	
	DataSize = 0x8A40,
	
	NameLength = 0x8A41,
	
	ActiveUniforms = 0x8A42,
	
	ActiveUniformIndices = 0x8A43,
	
	ReferencedByVertexShader = 0x8A44,
	
	ReferencedByGeometryShader = 0x8A45,
	
	ReferencedByFragmentShader = 0x8A46,
	
	ReferencedByComputeShader = 0x90EC
}