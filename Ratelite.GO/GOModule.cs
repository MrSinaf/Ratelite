using Ratelite.Resources;

namespace Ratelite.GO;

public class GOModule : ILoadableModule
{
	public const string CAMERA_SHADER = "@go/camera.rshad";
	public const string DEFAULT_SHADER = "@go/default.rshad";
	
	public int priority => 15;
	
	public void Init() { }
	
	public async Task Load()
	{
		if (!Vault.ContainsAsset(DEFAULT_SHADER))
		{
			await Vault.LoadManifestResource<Shader>(
				GetType().Assembly,
				"Ratelite.GO.assets.default.rshad",
				DEFAULT_SHADER
			);
			await Vault.LoadManifestResource<Shader>(
				GetType().Assembly,
				"Ratelite.GO.assets.camera.rshad",
				CAMERA_SHADER
			);
		}
	}
}