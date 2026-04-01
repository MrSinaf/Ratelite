using Ratelite.Bindings;

namespace Ratelite.Rendering;

public enum TextureWrap
{
	Repeat = GLEnum.Repeat,
	ClampToEdge = GLEnum.ClampToEdge,
	MirroredRepeat = GLEnum.MirroredRepeat,
	ClampToBorder = GLEnum.ClampToBorder
}