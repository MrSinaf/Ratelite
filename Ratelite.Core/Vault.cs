using System.Reflection;

namespace Ratelite;

public static class Vault
{
	private static readonly Dictionary<string, AssetReference> assets = [];
	
	public static bool AddAsset<T>(string name, T asset) where T : class, IAsset
	{
		if (!assets.TryAdd(name, new AssetReference(asset)))
		{
			Log.Write(
				$"You are trying to add asset `{name}`, but it is already present in the cache!" +
				" (。_。)",
				Log.Level.Warning
			);
			return false;
		}
		
		return true;
	}
	
	public static bool ReplaceAsset<T>(string name, T asset) where T : class, IAsset
	{
		var contains = assets.ContainsKey(name);
		if (contains)
			assets[name] = new AssetReference(asset);
		
		return contains;
	}
	
	public static void RemoveAsset(string name)
	{
		if (name[0] == '@')
			throw new AccessViolationException("You cannot remove assets with '@' prefix!");
		
		if (assets.Remove(name, out var reference) && reference.asset is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
	
	public static T? GetAsset<T>(string name) where T : class, IAsset
		=> TryGetAsset<T>(name, out var asset)
				? asset
				: throw new NullReferenceException(
					$"The asset '{name}' with type '{typeof(T)}' is not present in the cache. " +
					$"(>ლ) Use '{nameof(TryGetAsset)}' to check if it exists!"
				);
	
	public static bool ContainsAsset(string name)
		=> assets.ContainsKey(name);
	
	public static bool TryGetAsset<T>(string name, out T? asset) where T : class, IAsset
	{
		if (assets.TryGetValue(name, out var value) && value.asset is T result)
		{
			asset = result;
			return true;
		}
		
		asset = null;
		return false;
	}
	
	public static async Task<T?> LoadResource<T>(string path)
			where T : class, IResourceAsync<T>
	{
		var fullPath = Path.Combine("assets", path);
		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(fullPath);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		await using var stream = File.OpenRead(fullPath);
		var asset = await T.LoadAsync(new VaultRessource(stream, extension));
		return asset;
	}
	
	public static async Task<T?> LoadResource<T>(string path, string name)
			where T : class, IResource<T>
	{
		if (assets.TryGetValue(name, out var reference))
		{
			Log.Write(
				$"You are trying to add asset `{name}`, but it's already present in the cache " +
				$"(*/ω＼*)!",
				Log.Level.Warning
			);
			if (reference.asset is T refAsset)
				return refAsset;
			
			return null;
		}
		
		var fullPath = Path.Combine("assets", path);
		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(fullPath);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		await using var stream = File.OpenRead(fullPath);
		var asset = T.Load(new VaultRessource(stream, extension));
		AddAsset(name, asset);
		
		return asset;
	}
	
	public static async Task<T?> LoadManifestResource<T>(
		Assembly assembly,
		string path,
		string name,
		bool addInCache = true
	) where T : class, IResource<T>
	{
		if (name[0] != '@')
			name = "@" + name;
		
		if (addInCache && assets.TryGetValue(name, out var reference))
		{
			Log.Write(
				$"You are trying to add asset `{name}`, but it's already present in the cache " +
				$"(*/ω＼*)!",
				Log.Level.Warning
			);
			if (reference.asset is T refAsset)
				return refAsset;
			
			return null;
		}
		
		await using var stream = assembly.GetManifestResourceStream(path);
		if (stream == null)
			throw new FileNotFoundException(
				$"The resource '{path}' was not found in assembly '{assembly.GetName().Name}'" +
				"! (￣_￣|||)"
			);
		
		var extension = Path.GetExtension(path);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format format ⊙﹏⊙∥:" + extension
			);
		
		var asset = T.Load(new VaultRessource(stream, extension));
		
		if (addInCache)
			AddAsset(name, asset);
		
		return asset;
	}
}

internal class AssetReference(object asset)
{
	public readonly object asset = asset;
}

public record class VaultRessource(Stream stream, string extension);