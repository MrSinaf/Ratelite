using Ratelite.Bindings;
using Ratelite.Rendering;
using Ratelite.Utils;

namespace Ratelite.Resources;

public class Texture2D : Texture, IResource<Texture2D>, IDisposable
{
	public readonly Color[] pixels;
	
	public readonly Vector2Int size;
	public readonly Vector2 texel;
	
	public Color this[int x, int y]
	{
		set => pixels[x + y * size.x] = value;
		get => pixels[x + y * size.x];
	}
	
	public Texture2D(int width, int height, Color[] pixels)
	{
		size = new Vector2Int(width, height);
		texel = Vector2.one / size;
		
		this.pixels = pixels;
		
		MainThreadQueue.EnqueueRenderer(() =>
		{
			gTexture = new GTexture();
			gTexture.SetImage2D((uint)width, (uint)height, pixels);
			SetFilter(TextureMin.Nearest, TextureMag.Nearest);
			SetWrap(TextureWrap.ClampToEdge);
		});
	}
	
	public Region GetUVRegion(RectInt target)
		=> new (target.position * texel, (target.position + target.size) * texel);
	
	public Rect GetUVRect(RectInt target) 
		=> new (target.position * texel, target.size * texel);
	
	public RawImage AsRawImage() => new (size.x, size.y, Color.AsBytes(pixels).ToArray());
	
	public void Dispose()
	{
		gTexture.Dispose();
		GC.SuppressFinalize(this);
	}
	
	public static Texture2D Load(Stream stream)
	{
		var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
		return new Texture2D(image.width, image.height, Color.AsColors(image.data).ToArray());
	}
	
	public static bool ValidateExtension(string extension) 
		=> extension is ".png" or ".jpg" or ".jpeg";
}