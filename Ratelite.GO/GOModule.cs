using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite.GO;

public class GOModule : ILoadableModule
{
	public const string DEFAULT_SHADER = "@go/default.rshad";
	public const string DEFAULT_MATERIAL = "@go/default.mat";
	
	public int priority => 15;
	
	public void Init() { }
	
	public async Task Load()
	{
		if (!Vault.ContainsAsset(DEFAULT_SHADER))
		{
			var shader = await Vault.LoadManifestResource<Shader>(
				GetType().Assembly,
				"Ratelite.GO.assets.default.rshad",
				DEFAULT_SHADER
			);
			
			MainThreadQueue.EnqueueRenderer(() => Vault.AddAsset(
						DEFAULT_MATERIAL,
						new MaterialObject(shader)
					)
			);
		}
	}
}