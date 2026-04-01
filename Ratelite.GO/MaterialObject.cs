using Ratelite.Rendering;
using Ratelite.Resources;

namespace Ratelite.GO;

public class MaterialObject(Shader? shader = null, params (string, object)[] properties) :
		Material(shader ?? Vault.GetAsset<Shader>(GOModule.DEFAULT_SHADER)!, properties)
{
	public const string TEXTURE = "u_texture";
	public const string TINT = "u_tint";
	public const string UV_RECT = "u_uv";
	public const string ALPHA = "u_alpha";
	
	public MaterialObject SetTexture(Texture texture)
	{
		SetProperty(TEXTURE, texture);
		return this;
	}
	
	public MaterialObject SetUVRect(Rect rect)
	{
		SetProperty(UV_RECT, rect);
		return this;
	}
	
	public MaterialObject SetAlpha(float alpha)
	{
		SetProperty(ALPHA, alpha);
		return this;
	}
	
	public MaterialObject SetTint(Color color)
	{
		SetProperty(TINT, color);
		return this;
	}
}