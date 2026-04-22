using System.Runtime.InteropServices;
using Ratelite.Bindings;

namespace Ratelite.Rendering;

public unsafe class Font
{
	private static readonly FT ft = new (Platform.Windows);
	
	public Color[]? colors;
	public Vector2Int size;
	public Dictionary<char, Glyph> glyphs = [];
	public uint pixelSize => 18;
	public int baseLine;
	
	private readonly byte[] bytes;
	private FT_FaceRec_* face;
	private FT_LibraryRec_* library;
	
	public Font(byte[] bytes)
	{
		this.bytes = bytes;
		
		var fontBytesHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
		
		fixed (FT_LibraryRec_** fixedLibrary = &library)
		fixed (FT_FaceRec_** fixedFace = &face)
		{
			ft.FT_Init_FreeType(fixedLibrary);
			var pBytes = (byte*)fontBytesHandle.AddrOfPinnedObject();
			
			ft.FT_New_Memory_Face(
				library,
				pBytes,
				new CLong(bytes.Length),
				new CLong(0),
				fixedFace
			);
		}
		
		ft.FT_Set_Pixel_Sizes(face, 0, pixelSize);
		
		baseLine = (int)(-face->size->metrics.descender.Value.ToInt64() / 64);
	}
	
	public Vector2 CalculTextSize(string text)
	{
		var textSize = Vector2.zero;
		if (string.IsNullOrEmpty(text))
			return textSize;
		
		var maxWidth = 0f;
		var currentWidth = 0f;
		var lineCount = 1;
		
		foreach (var c in text)
		{
			if (c == '\n')
			{
				if (currentWidth > maxWidth)
					maxWidth = currentWidth;
				
				currentWidth = 0f;
				lineCount++;
				continue;
			}
			
			if (!glyphs.TryGetValue(c, out var glyph))
				if (!glyphs.TryGetValue('?', out glyph))
					continue;
			
			currentWidth += glyph.advance;
		}
		
		if (currentWidth > maxWidth)
			maxWidth = currentWidth;
		
		textSize.x = maxWidth;
		textSize.y = lineCount * pixelSize;
		
		return textSize;
	}
	
	public int GetIndexCharInPosition(string text, float x)
	{
		if (string.IsNullOrEmpty(text))
			return 0;
		
		var currentWidth = 0f;
		for (var i = 0; i < text.Length; i++)
		{
			var c = text[i];
			
			if (!glyphs.TryGetValue(c, out var glyph))
				if (!glyphs.TryGetValue('?', out glyph))
					continue;
			
			currentWidth += glyph.advance;
			
			if (currentWidth > x)
				return i;
		}
		
		return text.Length;
	}
	
	public Color[] GetBitmap()
	{
		glyphs.Clear();
		
		size = new Vector2Int(528);
		colors = new Color[size.x * size.y];
		var texel = 1 / size.ToVector2();
		
		var cursorX = 0;
		var cursorY = 0;
		var rowH = 0;
		const int padding = 2;
		
		var charcodes = GetLatin();
		foreach (var c in charcodes)
		{
			var loadFlags = ft.FT_LOAD_DEFAULT |
							(int)ft.FT_LOAD_FORCE_AUTOHINT.Value.ToInt64();
			
			ft.FT_Load_Char(face, new CULong(c), loadFlags);
			ft.FT_Render_Glyph(face->glyph, FT_Render_Mode_.FT_RENDER_MODE_NORMAL);
			
			var bitmap = face->glyph->bitmap;
			var metrics = face->glyph->metrics;
			
			// Retour à la ligne si dépassement en X
			if (cursorX + bitmap.width + padding > size.x)
			{
				cursorX = 0;
				cursorY += rowH + padding;
				rowH = 0;
			}
			
			// Sécurité si dépassement en Y (atlas trop petit)
			if (cursorY + bitmap.rows + padding > size.y)
				break;
			
			for (var y = 0; y < bitmap.rows; y++)
			for (var x = 0; x < bitmap.width; x++)
			{
				var dstX = cursorX + x;
				var dstY = cursorY + y;
				
				var dstIndex = dstY * size.x + dstX;
				var alpha = bitmap.buffer[y * bitmap.pitch + x];
				
				colors[dstIndex] = new Color(255, 255, 255, alpha);
			}
			
			var width = metrics.width.Value.ToInt64() / 64F;
			var height = metrics.height.Value.ToInt64() / 64F;
			glyphs.Add(
				(char)c,
				new Glyph(
					new Region(
						cursorX * texel.x,
						cursorY * texel.y,
						(cursorX + width) * texel.x,
						(cursorY + height) * texel.y
					),
					new Vector2(width, height),
					new Vector2(
						metrics.horiBearingX.Value.ToInt64() / 64F,
						metrics.horiBearingY.Value.ToInt64() / 64F
					),
					metrics.horiAdvance.Value.ToInt64() / 64F
				)
			);
			
			// Avance le curseur pour le prochain glyph
			cursorX += (int)bitmap.width + padding;
			if (bitmap.rows > rowH) rowH = (int)bitmap.rows;
		}
		
		return colors;
	}
	
	private static IEnumerable<uint> GetLatin()
	{
		for (uint i = 32; i < 127; i++) yield return i;
		for (uint i = 160; i < 255; i++) yield return i;
	}
	
	private static List<uint> GetAllCharCode(FT_FaceRec_* face)
	{
		var result = new List<uint>();
		
		uint glyphIndex;
		var charcode = ft.FT_Get_First_Char(face, &glyphIndex);
		while (glyphIndex != 0)
		{
			if (charcode.Value <= 0x10FFFF)
				Log.Write($"[{char.ConvertFromUtf32((int)charcode.Value)}] - {result.Count}");
			
			result.Add((uint)charcode.Value);
			charcode = ft.FT_Get_Next_Char(face, charcode, &glyphIndex);
		}
		
		return result;
	}
}

public record struct Glyph(Region uv, Vector2 size, Vector2 offset, float advance);