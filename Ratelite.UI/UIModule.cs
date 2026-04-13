using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite.UI;

public class UIModule : ILoadableModule
{
	public const string DEFAULT_SHADER = "@ui/default.rshad";
	public const string DEFAULT_MATERIAL = "@ui/default.mat";
	public const string DEFAULT_MESH = "@ui/default.mesh";
	public const string DEFAULT_FONT = "@ui/default.font";
	
	public int priority => 10;
	
	public void Init()
	{
		Vault.AddAsset(
			DEFAULT_MESH,
			MeshFactory.CreateQuad(Vector2.one)
		);
	}
	
	public async Task Load()
	{
		if (!Vault.ContainsAsset(DEFAULT_SHADER))
		{
			var assembly = GetType().Assembly;
			var shader = await Vault.LoadManifestResource<Shader>(
				assembly,
				"Ratelite.UI.assets.default.rshad",
				DEFAULT_SHADER
			);
			
			MainThreadQueue.EnqueueRenderer(() => Vault.AddAsset(
						DEFAULT_MATERIAL,
						new MaterialUI(shader).SetTexture(Primitif.whitePixel)
					)
			);
			
			await Vault.LoadManifestResource<BitmapFont>(
				assembly,
				"Ratelite.UI.assets.compliance-sans.ttf",
				DEFAULT_FONT
			);
		}
	}
}