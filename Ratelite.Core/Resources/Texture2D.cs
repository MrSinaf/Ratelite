using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite.Resources;

public class Texture2D : Texture, IResourceAsync<Texture2D>, IDisposable
{
	public static readonly Config internalConfig = new (
		TextureMin.Nearest,
		TextureMag.Nearest,
		TextureWrap.ClampToEdge
	);
	public static Config defaultConfig = internalConfig;
	
	public readonly Color[] pixels;
	
	public readonly Vector2Int size;
	public readonly Vector2 texel;
	
	public Color this[int x, int y]
	{
		set => pixels[x + y * size.x] = value;
		get => pixels[x + y * size.x];
	}
	
	public Texture2D(int width, int height, Color[] pixels, Config? config)
	{
		config ??= defaultConfig;
		size = new Vector2Int(width, height);
		texel = Vector2.one / size;
		
		this.pixels = pixels;
		
		MainThread.Enqueue(() =>
			{
				gTexture = new GTexture();
				gTexture.SetImage2D((uint)width, (uint)height, pixels);
				SetFilter(config.minFilter, config.magFilter);
				SetWrap(config.wrap);
			}
		);
	}
	
	public Region GetUVRegion(RectInt target)
		=> new (target.position * texel, (target.position + target.size) * texel);
	
	public Rect GetUVRect(RectInt target)
		=> new (target.position * texel, target.size * texel);
	
	public RawImage AsRawImage() => new (size.x, size.y, Color.AsBytes(pixels).ToArray());
	
	public void Dispose() => MainThread.Enqueue(() =>
		{
			gTexture.Dispose();
			GC.SuppressFinalize(this);
		}
	);
	
	public static Texture2D Load(VaultRessource ress)
	{
		var config = defaultConfig;
		if (ress.config is Config c)
			config = c;
		
		var image = ImageResult.FromStream(ress.stream, ColorComponents.RedGreenBlueAlpha);
		return new Texture2D(
			image.width,
			image.height,
			Color.AsColors(image.data).ToArray(),
			config
		);
	}
	
	public static Task<Texture2D> LoadAsync(VaultRessource ress)
		=> Task.FromResult(Load(ress));
	
	public static bool ValidateExtension(string extension)
		=> extension is ".png" or ".jpg" or ".jpeg";
	
	public record class Config(TextureMin minFilter, TextureMag magFilter, TextureWrap wrap)
			: IResourceConfig;
}