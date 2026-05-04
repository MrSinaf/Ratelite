using Ratelite.Resources;

namespace Ratelite.GO;

public class GOModule : ILoadableModule
{
	public const string CAMERA_SHADER = "@go/camera.rshad";
	public const string CAMERA_MATERIAL = "@go/camera.mat";
	
	public const string DEFAULT_SHADER = "@go/default.rshad";
	public const string DEFAULT_MATERIAL = "@go/default.mat";
	
	public int priority => 15;
	
	public void Init() { }
	
	public async Task Load()
	{
		if (!Vault.ContainsAsset(DEFAULT_SHADER))
		{
			Vault.AddAsset(
				DEFAULT_MATERIAL,
				new MaterialObject(
					await Vault.LoadManifestResource<Shader>(
						GetType().Assembly,
						"Ratelite.GO.assets.default.rshad",
						DEFAULT_SHADER
					)
				)
			);
			
			Vault.AddAsset(
				CAMERA_MATERIAL,
				new Material(
					(await Vault.LoadManifestResource<Shader>(
						GetType().Assembly,
						"Ratelite.GO.assets.camera.rshad",
						CAMERA_SHADER
					))!
				)
			);
		}
	}
}