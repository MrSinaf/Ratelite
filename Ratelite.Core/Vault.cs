using System.Reflection;

namespace Ratelite;

public static class Vault
{
	private static readonly Dictionary<string, AssetReference> assets = [];
	
	public static bool ContainsAsset(string name) => assets.ContainsKey(name);
	
	public static T? GetAsset<T>(string name) where T : class, IAsset
		=> TryGetAsset<T>(name, out var asset)
				? asset
				: throw new NullReferenceException(
					$"The asset '{name}' with type '{typeof(T)}' is not present in the cache. " +
					$"(>ლ) Use '{nameof(TryGetAsset)}' to check if it exists!"
				);
	
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
	
	public static void RemoveAsset(string name)
	{
		if (name[0] == '@')
			throw new AccessViolationException("You cannot remove assets with '@' prefix!");
		
		if (assets.Remove(name, out var reference) && reference.asset is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
	
	public static bool ReplaceAsset<T>(string name, T asset) where T : class, IAsset
	{
		var contains = assets.ContainsKey(name);
		if (contains)
			assets[name] = new AssetReference(asset);
		
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
	
	/// <summary>
	/// Charge une ressource de type spécifié à partir d'un chemin relative à 'assets' donné.
	/// </summary>
	/// <typeparam name="T">
	/// Le type de la ressource à charger.
	/// </typeparam>
	/// <param name="path">
	/// Le chemin de la ressource à charger.
	/// </param>
	/// <param name="config">
	/// Une configuration optionnelle (≧ω≦) pour le chargement de la ressource.
	/// </param>
	/// <returns>
	/// La ressource chargée si elle a été validée avec succès. ヾ(＾∇＾)
	/// </returns>
	/// <exception cref="FileNotFoundException">
	/// Levée si le fichier spécifié n'existe pas.
	/// </exception>
	/// <exception cref="ArgumentException">
	/// Levée si l'extension de fichier n'est pas prise en charge.
	/// </exception>
	/// <remarks>
	/// Cette méthode doit être êxécuté exclusivement dans le Thread principal, sinon utiliser sa
	/// version async!
	/// </remarks>
	public static T LoadResource<T>(string path, IResourceConfig? config = null)
			where T : class, IResource<T>
	{
		var fullPath = Path.Combine("assets", path);
		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(fullPath);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		using var stream = File.OpenRead(fullPath);
		var asset = T.Load(new VaultRessource(stream, extension, config));
		return asset;
	}
	
	/// <summary>
	/// Charge de manière asynchrone une ressource de type spécifié à partir d'un chemin relatif à
	/// 'assets' donné. (✿^‿^)
	/// </summary>
	/// <typeparam name="T">
	/// Le type de la ressource à charger. ( ^_^)/
	/// </typeparam>
	/// <param name="path">
	/// Le chemin relatif de la ressource à charger. (/^▽^)/
	/// </param>
	/// <param name="config">
	/// Une configuration optionnelle pour personnaliser le chargement de la ressource.
	/// </param>
	/// <returns>
	/// Une tâche représentant la ressource chargée si l'opération est réussie. ✧٩(ˊωˋ*)و✧
	/// </returns>
	/// <exception cref="FileNotFoundException">
	/// Levée si le fichier spécifié n'existe pas.
	/// </exception>
	/// <exception cref="ArgumentException">
	/// Levée si l'extension de fichier n'est pas prise en charge.
	/// </exception>
	public static async Task<T> LoadResourceAsync<T>(string path, IResourceConfig? config = null)
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
		var asset = await T.LoadAsync(new VaultRessource(stream, extension, config));
		return asset;
	}
	
	/// <summary>
	/// Charge une ressource à partir d'un chemin donné et l'ajoute au cache sous un nom spécifique.
	/// </summary>
	/// <typeparam name="T">
	/// Le type de la ressource à charger.
	/// </typeparam>
	/// <param name="path">
	/// Le chemin relatif à 'assets' de la ressource à charger.
	/// </param>
	/// <param name="name">
	/// Le nom unique sous lequel la ressource sera ajoutée au cache (★^O^★).
	/// </param>
	/// <param name="config">
	/// Une configuration optionnelle pour le chargement de la ressource.
	/// </param>
	/// <returns>
	/// Retourne la ressource chargée si elle est validée avec succès et ajoutée au cache
	/// ─=≡Σ((((((つ•̀ω•́)つ.<br/> Si la ressource existe déjà dans le cache avec le même nom et le
	/// même type, cette ressource est retournée. Retourne <c>null</c> si le type de la ressource ne
	/// correspond pas à celui dans le cache (。´・ω・)ん?.
	/// </returns>
	/// <exception cref="FileNotFoundException">
	/// Levée si le fichier spécifié dans le chemin n'existe pas.
	/// </exception>
	/// <exception cref="ArgumentException">
	/// Levée si l'extension du fichier n'est pas prise en charge.
	/// </exception>
	/// <remarks>
	/// Si une ressource avec le même nom existe déjà mais avec un type différent, elle ne sera pas
	/// remplacée (￣^￣)ゞ.<br/>
	/// Cette méthode doit être êxécuté exclusivement dans le Thread principal, sinon utiliser sa
	/// version async!
	/// </remarks>
	public static T? LoadResource<T>(
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
		
		var fullPath = Path.Combine("assets", path);
		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(fullPath);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		using var stream = File.OpenRead(fullPath);
		var asset = T.Load(new VaultRessource(stream, extension, config));
		AddAsset(name, asset);
		
		return asset;
	}
	
	public static async Task<T?> LoadResourceAsync<T>(
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
		
		var fullPath = Path.Combine("assets", path);
		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"The resource '{path}' does not exist! (￣_￣|||)");
		
		var extension = Path.GetExtension(fullPath);
		if (!T.ValidateExtension(extension))
			throw new ArgumentException(
				"Unsupported format ⊙﹏⊙∥:" + extension
			);
		
		await using var stream = File.OpenRead(fullPath);
		var asset = await T.LoadAsync(new VaultRessource(stream, extension, config));
		AddAsset(name, asset);
		
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
		AddAsset(name, asset);
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
		AddAsset(name, asset);
		return asset;
	}
}

public class AssetReference(object asset)
{
	public readonly object asset = asset;
}

public interface IResourceConfig;

public record class VaultRessource(Stream stream, string extension, IResourceConfig? config);