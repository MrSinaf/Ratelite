using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class Image : UIElement
{
	public Texture2D? texture
	{
		get => material?.GetProperty<Texture2D>(MaterialUI.TEXTURE);
		set => material?.SetTexture(value);
	}
	
	public Image(Texture2D texture, MaterialUI material, string? prefab = "")
	{
		base.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		base.material = material;
		this.texture = texture;
		
		UIPrefab.Apply(prefab, this);
	}
	
	public Image(Texture2D texture) : this(
		texture,
		new MaterialUI()
	) { }
	
	public Image()
	{
		base.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		base.material = new MaterialUI();
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(Image e)
	{
		e.size = e.texture?.size ?? new Vector2(10);
	}
}