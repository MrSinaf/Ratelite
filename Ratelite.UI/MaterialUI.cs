using Ratelite.Resources;

namespace Ratelite.UI;

public class MaterialUI(Shader? shader = null, params (string, object)[] properties) :
		Material(shader ?? Vault.GetAsset<Shader>(UIModule.DEFAULT_SHADER)!, properties)
{
	public const string TEXTURE = "u_texture";
	public const string NINEPATCH = "u_ninepatch";
	public const string NINEPATCH_SCALE = "u_ninepatchScale";
	
	public Texture2D? texture
	{
		get => GetProperty<Texture2D>(TEXTURE);
		set => SetProperty(TEXTURE, value);
	}
	public Rect ninepatch
	{
		get => GetProperty<Rect>(NINEPATCH);
		set => SetProperty(NINEPATCH, value);
	}
	public float ninepatchScale
	{
		get => GetProperty<float>(NINEPATCH_SCALE);
		set => SetProperty(NINEPATCH_SCALE, value);
	}
	
	public MaterialUI SetTexture(Texture2D? texture)
	{
		SetProperty(TEXTURE, texture);
		return this;
	}
	
	public MaterialUI SetNinePatch(Region region, float scale = 1)
	{
		SetProperty(NINEPATCH, region);
		SetProperty(NINEPATCH_SCALE, scale);
		return this;
	}
}