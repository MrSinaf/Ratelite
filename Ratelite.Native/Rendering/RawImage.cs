using Ratelite.Bindings;

namespace Ratelite.Rendering;

public class RawImage(int width, int height, byte[] pixels)
{
	public readonly byte[] pixels = pixels;
	public readonly Vector2Int size = new (width, height);
	
	public static RawImage Load(string path)
	{
		using Stream stream = new FileStream(
			path,
			FileMode.Open,
			FileAccess.Read,
			FileShare.ReadWrite,
			4096,
			true
		);
		return Load(stream);
	}
	
	public static RawImage Load(Stream stream)
	{
		var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
		return new RawImage(image.width, image.height, image.data);
	}
}