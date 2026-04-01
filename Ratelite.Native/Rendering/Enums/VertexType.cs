using Ratelite.Bindings;

namespace Ratelite.Rendering;

public enum VertexType
{
	Byte = VertexAttribPointerType.Byte,
	UByte = VertexAttribPointerType.UnsignedByte,
	Short = VertexAttribPointerType.Short,
	UShort = VertexAttribPointerType.UnsignedShort,
	Int = VertexAttribPointerType.Int,
	UInt = VertexAttribPointerType.UnsignedInt,
	Float = VertexAttribPointerType.Float
}