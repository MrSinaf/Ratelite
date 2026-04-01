using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class Panel : UIElement
{
	public Panel(string prefab = "")
	{
		UIPrefab.Apply(prefab, this);
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(Panel e)
	{
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.size = new Vector2(400);
		e.tint = new Color(0x2C384A);
		e.cornerRadius = new Region(12);
	}
}