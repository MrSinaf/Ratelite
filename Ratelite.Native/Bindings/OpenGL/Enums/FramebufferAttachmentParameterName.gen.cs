#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum FramebufferAttachmentParameterName : int
{
	[Obsolete("Deprecated in favour of \"ColorEncoding\"")]
	FramebufferAttachmentColorEncoding = 0x8210,
	[Obsolete("Deprecated in favour of \"ColorEncodingExt\"")]
	FramebufferAttachmentColorEncodingExt = 0x8210,
	[Obsolete("Deprecated in favour of \"ComponentType\"")]
	FramebufferAttachmentComponentType = 0x8211,
	[Obsolete("Deprecated in favour of \"ComponentTypeExt\"")]
	FramebufferAttachmentComponentTypeExt = 0x8211,
	[Obsolete("Deprecated in favour of \"RedSize\"")]
	FramebufferAttachmentRedSize = 0x8212,
	[Obsolete("Deprecated in favour of \"GreenSize\"")]
	FramebufferAttachmentGreenSize = 0x8213,
	[Obsolete("Deprecated in favour of \"BlueSize\"")]
	FramebufferAttachmentBlueSize = 0x8214,
	[Obsolete("Deprecated in favour of \"AlphaSize\"")]
	FramebufferAttachmentAlphaSize = 0x8215,
	[Obsolete("Deprecated in favour of \"DepthSize\"")]
	FramebufferAttachmentDepthSize = 0x8216,
	[Obsolete("Deprecated in favour of \"StencilSize\"")]
	FramebufferAttachmentStencilSize = 0x8217,
	[Obsolete("Deprecated in favour of \"ObjectType\"")]
	FramebufferAttachmentObjectType = 0x8CD0,
	[Obsolete("Deprecated in favour of \"ObjectTypeExt\"")]
	FramebufferAttachmentObjectTypeExt = 0x8CD0,
	[Obsolete("Deprecated in favour of \"ObjectTypeOes\"")]
	FramebufferAttachmentObjectTypeOes = 0x8CD0,
	[Obsolete("Deprecated in favour of \"ObjectName\"")]
	FramebufferAttachmentObjectName = 0x8CD1,
	[Obsolete("Deprecated in favour of \"ObjectNameExt\"")]
	FramebufferAttachmentObjectNameExt = 0x8CD1,
	[Obsolete("Deprecated in favour of \"ObjectNameOes\"")]
	FramebufferAttachmentObjectNameOes = 0x8CD1,
	[Obsolete("Deprecated in favour of \"TextureLevel\"")]
	FramebufferAttachmentTextureLevel = 0x8CD2,
	[Obsolete("Deprecated in favour of \"TextureLevelExt\"")]
	FramebufferAttachmentTextureLevelExt = 0x8CD2,
	[Obsolete("Deprecated in favour of \"TextureLevelOes\"")]
	FramebufferAttachmentTextureLevelOes = 0x8CD2,
	[Obsolete("Deprecated in favour of \"TextureCubeMapFace\"")]
	FramebufferAttachmentTextureCubeMapFace = 0x8CD3,
	[Obsolete("Deprecated in favour of \"TextureCubeMapFaceExt\"")]
	FramebufferAttachmentTextureCubeMapFaceExt = 0x8CD3,
	[Obsolete("Deprecated in favour of \"TextureCubeMapFaceOes\"")]
	FramebufferAttachmentTextureCubeMapFaceOes = 0x8CD3,
	[Obsolete("Deprecated in favour of \"Texture3DZoffsetExt\"")]
	FramebufferAttachmentTexture3DZoffsetExt = 0x8CD4,
	[Obsolete("Deprecated in favour of \"Texture3DZoffsetOes\"")]
	FramebufferAttachmentTexture3DZoffsetOes = 0x8CD4,
	[Obsolete("Deprecated in favour of \"TextureLayer\"")]
	FramebufferAttachmentTextureLayer = 0x8CD4,
	[Obsolete("Deprecated in favour of \"TextureLayerExt\"")]
	FramebufferAttachmentTextureLayerExt = 0x8CD4,
	[Obsolete("Deprecated in favour of \"TextureSamplesExt\"")]
	FramebufferAttachmentTextureSamplesExt = 0x8D6C,
	[Obsolete("Deprecated in favour of \"Layered\"")]
	FramebufferAttachmentLayered = 0x8DA7,
	[Obsolete("Deprecated in favour of \"LayeredArb\"")]
	FramebufferAttachmentLayeredArb = 0x8DA7,
	[Obsolete("Deprecated in favour of \"LayeredExt\"")]
	FramebufferAttachmentLayeredExt = 0x8DA7,
	[Obsolete("Deprecated in favour of \"LayeredOes\"")]
	FramebufferAttachmentLayeredOes = 0x8DA7,
	[Obsolete("Deprecated in favour of \"TextureScaleImg\"")]
	FramebufferAttachmentTextureScaleImg = 0x913F,
	[Obsolete("Deprecated in favour of \"TextureNumViewsOvr\"")]
	FramebufferAttachmentTextureNumViewsOvr = 0x9630,
	[Obsolete("Deprecated in favour of \"TextureBaseViewIndexOvr\"")]
	FramebufferAttachmentTextureBaseViewIndexOvr = 0x9632,
	
	ColorEncoding = 0x8210,
	
	ColorEncodingExt = 0x8210,
	
	ComponentType = 0x8211,
	
	ComponentTypeExt = 0x8211,
	
	RedSize = 0x8212,
	
	GreenSize = 0x8213,
	
	BlueSize = 0x8214,
	
	AlphaSize = 0x8215,
	
	DepthSize = 0x8216,
	
	StencilSize = 0x8217,
	
	ObjectType = 0x8CD0,
	
	ObjectTypeExt = 0x8CD0,
	
	ObjectTypeOes = 0x8CD0,
	
	ObjectName = 0x8CD1,
	
	ObjectNameExt = 0x8CD1,
	
	ObjectNameOes = 0x8CD1,
	
	TextureLevel = 0x8CD2,
	
	TextureLevelExt = 0x8CD2,
	
	TextureLevelOes = 0x8CD2,
	
	TextureCubeMapFace = 0x8CD3,
	
	TextureCubeMapFaceExt = 0x8CD3,
	
	TextureCubeMapFaceOes = 0x8CD3,
	
	Texture3DZoffsetExt = 0x8CD4,
	
	Texture3DZoffsetOes = 0x8CD4,
	
	TextureLayer = 0x8CD4,
	
	TextureLayerExt = 0x8CD4,
	
	TextureSamplesExt = 0x8D6C,
	
	Layered = 0x8DA7,
	
	LayeredArb = 0x8DA7,
	
	LayeredExt = 0x8DA7,
	
	LayeredOes = 0x8DA7,
	
	TextureScaleImg = 0x913F,
	
	TextureNumViewsOvr = 0x9630,
	
	TextureBaseViewIndexOvr = 0x9632
}