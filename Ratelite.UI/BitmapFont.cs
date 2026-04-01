using Ratelite.Rendering;
using Ratelite.Resources;

namespace Ratelite.UI;

public class BitmapFont : IResource<BitmapFont>
{
	public required MaterialUI material { get; init; }
	public required Font data { get; init; }
	
	public static BitmapFont Load(Stream stream)
	{
		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		var font = new Font(memoryStream.ToArray());
		
		var material = new MaterialUI();
		font.GetBitmap();
		var texture = new Texture2D(font.size.x, font.size.y, font.colors);
		texture.SetFilter(TextureMin.Linear, TextureMag.Linear);
		texture.SetWrap(TextureWrap.ClampToEdge);
		texture.gTexture.GenerateMipmap();
		material.SetTexture(texture);
		
		return new BitmapFont {
			data = font,
			material = material
		};
	}
	
	public static bool ValidateExtension(string extension)
		=> extension is ".ttf";
}