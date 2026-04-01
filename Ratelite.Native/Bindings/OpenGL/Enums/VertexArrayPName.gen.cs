#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum VertexArrayPName : int
{
	[Obsolete("Deprecated in favour of \"RelativeOffset\"")]
	VertexAttribRelativeOffset = 0x82D5,
	[Obsolete("Deprecated in favour of \"ArrayEnabled\"")]
	VertexAttribArrayEnabled = 0x8622,
	[Obsolete("Deprecated in favour of \"ArraySize\"")]
	VertexAttribArraySize = 0x8623,
	[Obsolete("Deprecated in favour of \"ArrayStride\"")]
	VertexAttribArrayStride = 0x8624,
	[Obsolete("Deprecated in favour of \"ArrayType\"")]
	VertexAttribArrayType = 0x8625,
	[Obsolete("Deprecated in favour of \"ArrayLong\"")]
	VertexAttribArrayLong = 0x874E,
	[Obsolete("Deprecated in favour of \"ArrayNormalized\"")]
	VertexAttribArrayNormalized = 0x886A,
	[Obsolete("Deprecated in favour of \"ArrayInteger\"")]
	VertexAttribArrayInteger = 0x88FD,
	[Obsolete("Deprecated in favour of \"ArrayDivisor\"")]
	VertexAttribArrayDivisor = 0x88FE,
	
	RelativeOffset = 0x82D5,
	
	ArrayEnabled = 0x8622,
	
	ArraySize = 0x8623,
	
	ArrayStride = 0x8624,
	
	ArrayType = 0x8625,
	
	ArrayLong = 0x874E,
	
	ArrayNormalized = 0x886A,
	
	ArrayInteger = 0x88FD,
	
	ArrayDivisor = 0x88FE
}