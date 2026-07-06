namespace Ratelite;

public interface IResource<out T> : IAsset where T : IAsset
{
	protected internal static abstract T Load(VaultRessource ress);
	protected internal static abstract bool ValidateExtension(string extension);
}

public interface IResourceAsync<T> : IResource<T> where T : IAsset
{
	protected internal static abstract Task<T> LoadAsync(VaultRessource ress);
}

public interface IHotReloadResource
{
	protected internal void HotReload(Stream stream);
}

public interface IHotReloadResourceAsync: IHotReloadResource
{
	protected internal Task HotReloadAsync(Stream stream);
}