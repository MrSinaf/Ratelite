using Ratelite.Rendering;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite.UI;

public class BitmapFont : IResourceAsync<BitmapFont>
{
	public static readonly Config internalConfig = new (new Vector2Int(256), 18);
	public static Config defaultConfig = internalConfig;
	
	public required MaterialUI material { get; init; }
	public required Font data { get; init; }
	
	public static BitmapFont Load(VaultRessource ress)
	{
		var config = defaultConfig;
		if (ress.config is Config c)
			config = c;
		
		using var memoryStream = new MemoryStream();
		ress.stream.CopyTo(memoryStream);
		
		var font = new Font(
			memoryStream.ToArray(),
			config.textureSize,
			config.fontSize,
			config.baselineOffset
		);
		var material = new MaterialUI();
		font.GetBitmap();
		var texture = new Texture2D(font.size.x, font.size.y, font.colors!);
		material.SetTexture(texture);
		
		MainThreadQueue.EnqueueRenderer(() =>
			{
				texture.SetFilter(TextureMin.Linear, TextureMag.Linear);
				texture.SetWrap(TextureWrap.ClampToEdge);
				texture.gTexture.GenerateMipmap();
			}
		);
		
		return new BitmapFont
		{
			data = font,
			material = material
		};
	}
	
	public static async Task<BitmapFont> LoadAsync(VaultRessource ress)
	{
		var config = defaultConfig;
		if (ress.config is Config c)
			config = c;
		
		using var memoryStream = new MemoryStream();
		await ress.stream.CopyToAsync(memoryStream);
		
		var font = new Font(
			memoryStream.ToArray(),
			config.textureSize,
			config.fontSize,
			config.baselineOffset
		);
		var material = new MaterialUI();
		font.GetBitmap();
		var texture = new Texture2D(font.size.x, font.size.y, font.colors!);
		material.SetTexture(texture);
		
		MainThreadQueue.EnqueueRenderer(() =>
			{
				texture.SetFilter(TextureMin.Linear, TextureMag.Linear);
				texture.SetWrap(TextureWrap.ClampToEdge);
				texture.gTexture.GenerateMipmap();
			}
		);
		await MainThreadQueue.Wait();
		
		return new BitmapFont
		{
			data = font,
			material = material
		};
	}
	
	public static bool ValidateExtension(string extension)
		=> extension is ".ttf";
	
	public record class Config(Vector2Int textureSize, uint fontSize, int baselineOffset = 0)
			: IResourceConfig;
}