namespace Ratelite.Rendering;


public abstract class Texture
{
	public readonly GTexture gTexture = new ();
	
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