namespace Ratelite;

// TODO > Avoir une variante Async
public interface IResource<out T> : IAsset where T : IAsset
{
	protected internal static abstract T Load(Stream stream);
	protected internal static abstract bool ValidateExtension(string extension);
}