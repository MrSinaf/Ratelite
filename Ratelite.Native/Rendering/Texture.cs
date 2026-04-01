namespace Ratelite.Rendering;

// TODO > Voir pour le supprimer :O, je pense qu'on pourrait s'en passer!
// Faire que les Texture2D gère ça comme fait un mesh par exemple, avec un Bind simple!
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