using System.Reflection;

namespace Ratelite;

public static class Vault
{
	private static readonly Dictionary<string, AssetReference> assets = [];
	
	public static string projectRoot;
	public static bool ContainsAsset(string name) => assets.ContainsKey(name);
	
	static Vault()
	{
		var directory = new DirectoryInfo(AppContext.BaseDirectory);
		while (directory != null)
		{
			if (directory.GetFiles("*.csproj").Length > 0)
			{
				projectRoot = directory.FullName;
				return;
			}
			directory = directory.Parent;
		}
		projectRoot = Directory.GetCurrentDirectory();
	}
	
	public static void HotReloadAsset(string name)
	{
		if (assets.TryGetValue(name, out var assetRef) &&
			assetRef is { initialPath: not null, asset: IHotReloadResource asset })
		{
			var fullPath = Path.Combine(projectRoot, assetRef.initialPath);
			if (!File.Exists(fullPath))
				throw new FileNotFoundException(
					$"The resource '{fullPath}' does not exist! (￣_￣|||)"
				);
			
			using var stream = File.OpenRead(fullPath);
			try
			{
				asset.HotReload(stream);
				Log.Write($"Hot reloaded asset '{name}' (○｀ 3′○)", Log.Level.Info);
			}
			catch (Exception e)
			{
				Log.Write($"Failed to hot reload asset '{name}'", e, true);
			}
		}
	}
	
	public static async Task HotReloadAssetAsync(string name)
	{
		if (assets.TryGetValue(name, out var assetRef) &&
			assetRef is { initialPath: not null, asset: IHotReloadResourceAsync asset })
		{
			var fullPath = Path.Combine(projectRoot, assetRef.initialPath);
			if (!File.Exists(fullPath))
				throw new FileNotFoundException(
					$"The resource '{fullPath}' does not exist! (￣_￣|||)"
				);
			
			await using var stream = File.OpenRead(fullPath);
			await asset.HotReloadAsync(stream);
		}
	}
	
	public static T? GetAsset<T>(string name) where T : class, IAsset
		=> TryGetAsset<T>(name, out var asset)
				? asset
				: throw new NullReferenceException(
					$"The asset '{name}' with type '{typeof(T)}' is not present in the cache. " +
					$"(>ლ) Use '{nameof(TryGetAsset)}' to check if it exists!"
				);
	
	public static bool AddAsset<T>(
		string name,
		T asset,
		string? initialPath = null,
		IResourceConfig? config = null
	)
			where T : class, IAsset
	{
		if (!assets.TryAdd(name, new AssetReference(asset, initialPath)))
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
	
	public static void RemoveAsset(string name)
	{
		if (name[0] == '@')
			throw new AccessViolationException("You cannot remove assets with '@' prefix!");
		
		if (assets.Remove(name, out var reference) && reference.asset is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
	
	public static bool ReplaceAsset<T>(
		string name,
		T asset,
		string? initialPath = null,
		IResourceConfig? config = null
	)
			where T : class, IAsset
	{
		var contains = assets.ContainsKey(name);
		if (contains)
			assets[name] = new AssetReference(asset, initialPath);
		
		return contains;
	}
	
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
	
	public static T LoadResource<T>(string path, IResourceConfig? config = null)
			where T : class, IResource<T>
		=> LoadExternResource<T>(Path.Combine("assets", path), config);
	
	public static T LoadExternResource<T>(string path, IResourceConfig? config = null)
			where T : class, IResource<T>
	{
		if (!File.Exists(path))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(path);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		using var stream = File.OpenRead(path);
		var asset = T.Load(new VaultRessource(stream, extension, config));
		return asset;
	}
	
	public static async Task<T> LoadResourceAsync<T>(string path, IResourceConfig? config = null)
			where T : class, IResourceAsync<T>
		=> await LoadExternResourceAsync<T>(Path.Combine("assets", path), config);
	
	public static async Task<T> LoadExternResourceAsync<T>(
		string path,
		IResourceConfig? config = null
	) where T : class, IResourceAsync<T>
	{
		if (!File.Exists(path))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(path);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		await using var stream = File.OpenRead(path);
		var asset = await T.LoadAsync(new VaultRessource(stream, extension, config));
		return asset;
	}
	
	public static T? LoadResource<T>(
		string path,
		string name,
		IResourceConfig? config = null
	) where T : class, IResource<T>
		=> LoadExternResource<T>(Path.Combine("assets", path), name, config);
	
	public static T? LoadExternResource<T>(
		string path,
		string name,
		IResourceConfig? config = null
	) where T : class, IResource<T>
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
		
		if (!File.Exists(path))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(path);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		using var stream = File.OpenRead(path);
		var asset = T.Load(new VaultRessource(stream, extension, config));
		AddAsset(name, asset, path, config);
		
		return asset;
	}
	
	
	public static async Task<T?> LoadResourceAsync<T>(
		string path,
		string name,
		IResourceConfig? config = null
	) where T : class, IResourceAsync<T>
		=> await LoadExternResourceAsync<T>(Path.Combine("assets", path), name, config);
	
	public static async Task<T?> LoadExternResourceAsync<T>(
		string path,
		string name,
		IResourceConfig? config = null
	) where T : class, IResourceAsync<T>
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
		
		if (!File.Exists(path))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(path);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		await using var stream = File.OpenRead(path);
		var asset = await T.LoadAsync(new VaultRessource(stream, extension, config));
		AddAsset(name, asset, path, config);
		
		return asset;
	}
	
	public static T LoadManifestResource<T>(
		Assembly assembly,
		string path,
		IResourceConfig? config = null
	) where T : class, IResource<T>
	{
		using var stream = assembly.GetManifestResourceStream(path);
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
		
		var asset = T.Load(new VaultRessource(stream, extension, config));
		return asset;
	}
	
	public static async Task<T> LoadManifestResourceAsync<T>(
		Assembly assembly,
		string path,
		IResourceConfig? config = null
	) where T : class, IResourceAsync<T>
	{
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
		
		var asset = await T.LoadAsync(new VaultRessource(stream, extension, config));
		return asset;
	}
	
	public static T? LoadManifestResource<T>(
		Assembly assembly,
		string path,
		string name,
		IResourceConfig? config = null
	) where T : class, IResource<T>
	{
		if (name[0] != '@')
			name = "@" + name;
		
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
		
		using var stream = assembly.GetManifestResourceStream(path);
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
		
		var asset = T.Load(new VaultRessource(stream, extension, config));
		AddAsset(name, asset, path, config);
		return asset;
	}
	
	public static async Task<T?> LoadManifestResourceAsync<T>(
		Assembly assembly,
		string path,
		string name,
		IResourceConfig? config = null
	) where T : class, IResourceAsync<T>
	{
		if (name[0] != '@')
			name = "@" + name;
		
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
		
		var asset = await T.LoadAsync(new VaultRessource(stream, extension, config));
		AddAsset(name, asset, path);
		return asset;
	}
}

public class AssetReference
{
	public readonly object asset;
	public string? initialPath;
	
	internal AssetReference(object asset, string? initialPath)
	{
		this.asset = asset;
		this.initialPath = initialPath;
	}
}

public interface IResourceConfig;

public record class VaultRessource(Stream stream, string extension, IResourceConfig? config);