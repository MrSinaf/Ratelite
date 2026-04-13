using Ratelite.Rendering;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite.UI;

public class BitmapFont : IResource<BitmapFont>
{
	public required MaterialUI material { get; init; }
	public required Font data { get; init; }
	
	public static BitmapFont Load(VaultRessource ress)
	{
		using var memoryStream = new MemoryStream();
		ress.stream.CopyTo(memoryStream);
		var font = new Font(memoryStream.ToArray());
		
		var material = new MaterialUI();
		font.GetBitmap();
		var texture = new Texture2D(font.size.x, font.size.y, font.colors!);
		material.SetTexture(texture);
		
		MainThreadQueue.EnqueueRenderer(() =>
		{
			texture.SetFilter(TextureMin.Linear, TextureMag.Linear);
			texture.SetWrap(TextureWrap.ClampToEdge);
			texture.gTexture.GenerateMipmap();
		});
		
		return new BitmapFont {
			data = font,
			material = material
		};
	}
	
	public static bool ValidateExtension(string extension)
		=> extension is ".ttf";
}