#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum GetTextureParameter : int
{
	TextureWidth = 0x1000,
	
	TextureHeight = 0x1001,
	
	TextureInternalFormat = 0x1003,
	
	TextureBorderColor = 0x1004,
	
	TextureBorderColorNV = 0x1004,
	
	TextureMagFilter = 0x2800,
	
	TextureMinFilter = 0x2801,
	
	TextureWrapS = 0x2802,
	
	TextureWrapT = 0x2803,
	
	TextureRedSize = 0x805C,
	
	TextureGreenSize = 0x805D,
	
	TextureBlueSize = 0x805E,
	
	TextureAlphaSize = 0x805F,
	
	TextureDepthExt = 0x8071,
	
	TextureWrapRExt = 0x8072,
	
	DetailTextureLevelSgis = 0x809A,
	
	DetailTextureModeSgis = 0x809B,
	
	DetailTextureFuncPointsSgis = 0x809C,
	
	SharpenTextureFuncPointsSgis = 0x80B0,
	
	ShadowAmbientSgix = 0x80BF,
	
	DualTextureSelectSgis = 0x8124,
	
	QuadTextureSelectSgis = 0x8125,
	
	Texture4DsizeSgis = 0x8136,
	
	TextureWrapQSgis = 0x8137,
	
	TextureMinLodSgis = 0x813A,
	
	TextureMaxLodSgis = 0x813B,
	
	TextureBaseLevelSgis = 0x813C,
	
	TextureMaxLevelSgis = 0x813D,
	
	TextureFilter4SizeSgis = 0x8147,
	
	TextureClipmapCenterSgix = 0x8171,
	
	TextureClipmapFrameSgix = 0x8172,
	
	TextureClipmapOffsetSgix = 0x8173,
	
	TextureClipmapVirtualDepthSgix = 0x8174,
	
	TextureClipmapLodOffsetSgix = 0x8175,
	
	TextureClipmapDepthSgix = 0x8176,
	
	PostTextureFilterBiasSgix = 0x8179,
	
	PostTextureFilterScaleSgix = 0x817A,
	
	TextureLodBiasSSgix = 0x818E,
	
	TextureLodBiasTSgix = 0x818F,
	
	TextureLodBiasRSgix = 0x8190,
	
	GenerateMipmapSgis = 0x8191,
	
	TextureCompareSgix = 0x819A,
	
	TextureCompareOperatorSgix = 0x819B,
	
	TextureLequalRSgix = 0x819C,
	
	TextureGequalRSgix = 0x819D,
	
	TextureMaxClampSSgix = 0x8369,
	
	TextureMaxClampTSgix = 0x836A,
	
	TextureMaxClampRSgix = 0x836B,
	
	NormalMapArb = 0x8511,
	
	NormalMapExt = 0x8511,
	
	NormalMapNV = 0x8511,
	
	NormalMapOes = 0x8511,
	
	ReflectionMapArb = 0x8512,
	
	ReflectionMapExt = 0x8512,
	
	ReflectionMapNV = 0x8512,
	
	ReflectionMapOes = 0x8512,
	
	TextureUnnormalizedCoordinatesArm = 0x8F6A,
	
	SurfaceCompressionExt = 0x96C0,
	
	TextureYDegammaQCom = 0x9710,
	
	TextureCbcrDegammaQCom = 0x9711
}