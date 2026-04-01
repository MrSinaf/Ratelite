#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum UniformPName : int
{
	[Obsolete("Deprecated in favour of \"Type\"")]
	UniformType = 0x8A37,
	[Obsolete("Deprecated in favour of \"Size\"")]
	UniformSize = 0x8A38,
	[Obsolete("Deprecated in favour of \"NameLength\"")]
	UniformNameLength = 0x8A39,
	[Obsolete("Deprecated in favour of \"BlockIndex\"")]
	UniformBlockIndex = 0x8A3A,
	[Obsolete("Deprecated in favour of \"Offset\"")]
	UniformOffset = 0x8A3B,
	[Obsolete("Deprecated in favour of \"ArrayStride\"")]
	UniformArrayStride = 0x8A3C,
	[Obsolete("Deprecated in favour of \"MatrixStride\"")]
	UniformMatrixStride = 0x8A3D,
	[Obsolete("Deprecated in favour of \"IsRowMajor\"")]
	UniformIsRowMajor = 0x8A3E,
	[Obsolete("Deprecated in favour of \"AtomicCounterBufferIndex\"")]
	UniformAtomicCounterBufferIndex = 0x92DA,
	
	Type = 0x8A37,
	
	Size = 0x8A38,
	
	NameLength = 0x8A39,
	
	BlockIndex = 0x8A3A,
	
	Offset = 0x8A3B,
	
	ArrayStride = 0x8A3C,
	
	MatrixStride = 0x8A3D,
	
	IsRowMajor = 0x8A3E,
	
	AtomicCounterBufferIndex = 0x92DA
}