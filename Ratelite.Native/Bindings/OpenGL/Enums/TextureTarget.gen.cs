#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum TextureTarget : int
{
	Texture1D = 0xDE0,
	Texture2D = 0xDE1,
	
	ProxyTexture1D = 0x8063,
	
	ProxyTexture1DExt = 0x8063,
	
	ProxyTexture2D = 0x8064,
	
	ProxyTexture2DExt = 0x8064,
	Texture3D = 0x806F,
	
	Texture3DExt = 0x806F,
	
	Texture3DOes = 0x806F,
	
	ProxyTexture3D = 0x8070,
	
	ProxyTexture3DExt = 0x8070,
	
	DetailTexture2DSgis = 0x8095,
	
	Texture4DSgis = 0x8134,
	
	ProxyTexture4DSgis = 0x8135,
	
	TextureRectangle = 0x84F5,
	
	TextureRectangleArb = 0x84F5,
	
	TextureRectangleNV = 0x84F5,
	
	ProxyTextureRectangle = 0x84F7,
	
	ProxyTextureRectangleArb = 0x84F7,
	
	ProxyTextureRectangleNV = 0x84F7,
	
	TextureCubeMap = 0x8513,
	
	TextureCubeMapArb = 0x8513,
	
	TextureCubeMapExt = 0x8513,
	
	TextureCubeMapOes = 0x8513,
	
	TextureCubeMapPositiveX = 0x8515,
	
	TextureCubeMapPositiveXArb = 0x8515,
	
	TextureCubeMapPositiveXExt = 0x8515,
	
	TextureCubeMapPositiveXOes = 0x8515,
	
	TextureCubeMapNegativeX = 0x8516,
	
	TextureCubeMapNegativeXArb = 0x8516,
	
	TextureCubeMapNegativeXExt = 0x8516,
	
	TextureCubeMapNegativeXOes = 0x8516,
	
	TextureCubeMapPositiveY = 0x8517,
	
	TextureCubeMapPositiveYArb = 0x8517,
	
	TextureCubeMapPositiveYExt = 0x8517,
	
	TextureCubeMapPositiveYOes = 0x8517,
	
	TextureCubeMapNegativeY = 0x8518,
	
	TextureCubeMapNegativeYArb = 0x8518,
	
	TextureCubeMapNegativeYExt = 0x8518,
	
	TextureCubeMapNegativeYOes = 0x8518,
	
	TextureCubeMapPositiveZ = 0x8519,
	
	TextureCubeMapPositiveZArb = 0x8519,
	
	TextureCubeMapPositiveZExt = 0x8519,
	
	TextureCubeMapPositiveZOes = 0x8519,
	
	TextureCubeMapNegativeZ = 0x851A,
	
	TextureCubeMapNegativeZArb = 0x851A,
	
	TextureCubeMapNegativeZExt = 0x851A,
	
	TextureCubeMapNegativeZOes = 0x851A,
	
	ProxyTextureCubeMap = 0x851B,
	
	ProxyTextureCubeMapArb = 0x851B,
	
	ProxyTextureCubeMapExt = 0x851B,
	
	Texture1DArray = 0x8C18,
	
	ProxyTexture1DArray = 0x8C19,
	
	ProxyTexture1DArrayExt = 0x8C19,
	
	Texture2DArray = 0x8C1A,
	
	ProxyTexture2DArray = 0x8C1B,
	
	ProxyTexture2DArrayExt = 0x8C1B,
	
	TextureBuffer = 0x8C2A,
	
	Renderbuffer = 0x8D41,
	
	TextureCubeMapArray = 0x9009,
	
	TextureCubeMapArrayArb = 0x9009,
	
	TextureCubeMapArrayExt = 0x9009,
	
	TextureCubeMapArrayOes = 0x9009,
	
	ProxyTextureCubeMapArray = 0x900B,
	
	ProxyTextureCubeMapArrayArb = 0x900B,
	
	Texture2DMultisample = 0x9100,
	
	ProxyTexture2DMultisample = 0x9101,
	
	Texture2DMultisampleArray = 0x9102,
	
	ProxyTexture2DMultisampleArray = 0x9103
}