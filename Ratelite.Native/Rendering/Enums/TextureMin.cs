using Ratelite.Bindings;

namespace Ratelite.Rendering;

public enum TextureMin
{
	Nearest = GLEnum.Nearest, 
	NearestMipmapNearest = GLEnum.NearestMipmapNearest,
	NearestMipmapLinear = GLEnum.NearestMipmapLinear,
	
	Linear = GLEnum.Linear,
	LinearMipmapNearest = GLEnum.LinearMipmapNearest,
	LinearMipmapLinear = GLEnum.NearestMipmapLinear
}