using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class Image : UIElement
{
	public Texture2D texture;
	
	public Image(Texture2D texture, MaterialUI material, string? prefab = "")
	{
		base.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		base.material = material;
		this.texture = texture;
		
		material.SetTexture(texture);
		UIPrefab.Apply(prefab, this);
	}
	
	public Image(Texture2D texture) : this(
		texture,
		new MaterialUI()
	) { }
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(Image e)
	{
		e.size = e.texture.size;
	}
}