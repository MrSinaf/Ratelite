namespace Ratelite.Rendering;


public abstract class Texture
{
	public GTexture gTexture = null!;
	
	public void SetWrap(TextureWrap wrap)
	{
		gTexture.SetWrapS(wrap);
		gTexture.SetWrapT(wrap);
	}
	
	public void SetFilter(TextureMin minFilter, TextureMag magFilter)
	{
		gTexture.SetMinFilter(minFilter);
		gTexture.SetMagFilter(magFilter);
	}
}