#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum AtomicCounterBufferPName : int
{
	[Obsolete("Deprecated in favour of \"ReferencedByComputeShader\"")]
	AtomicCounterBufferReferencedByComputeShader = 0x90ED,
	[Obsolete("Deprecated in favour of \"Binding\"")]
	AtomicCounterBufferBinding = 0x92C1,
	[Obsolete("Deprecated in favour of \"DataSize\"")]
	AtomicCounterBufferDataSize = 0x92C4,
	[Obsolete("Deprecated in favour of \"ActiveAtomicCounters\"")]
	AtomicCounterBufferActiveAtomicCounters = 0x92C5,
	[Obsolete("Deprecated in favour of \"ActiveAtomicCounterIndices\"")]
	AtomicCounterBufferActiveAtomicCounterIndices = 0x92C6,
	[Obsolete("Deprecated in favour of \"ReferencedByVertexShader\"")]
	AtomicCounterBufferReferencedByVertexShader = 0x92C7,
	[Obsolete("Deprecated in favour of \"ReferencedByTessControlShader\"")]
	AtomicCounterBufferReferencedByTessControlShader = 0x92C8,
	[Obsolete("Deprecated in favour of \"ReferencedByTessEvaluationShader\"")]
	AtomicCounterBufferReferencedByTessEvaluationShader = 0x92C9,
	[Obsolete("Deprecated in favour of \"ReferencedByGeometryShader\"")]
	AtomicCounterBufferReferencedByGeometryShader = 0x92CA,
	[Obsolete("Deprecated in favour of \"ReferencedByFragmentShader\"")]
	AtomicCounterBufferReferencedByFragmentShader = 0x92CB,
	
	ReferencedByComputeShader = 0x90ED,
	
	Binding = 0x92C1,
	
	DataSize = 0x92C4,
	
	ActiveAtomicCounters = 0x92C5,
	
	ActiveAtomicCounterIndices = 0x92C6,
	
	ReferencedByVertexShader = 0x92C7,
	
	ReferencedByTessControlShader = 0x92C8,
	
	ReferencedByTessEvaluationShader = 0x92C9,
	
	ReferencedByGeometryShader = 0x92CA,
	
	ReferencedByFragmentShader = 0x92CB
}