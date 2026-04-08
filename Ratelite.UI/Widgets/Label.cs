using Ratelite.Rendering;
using Ratelite.Utils;

namespace Ratelite.UI.Widgets;

public class Label : UIElement
{
	public string text
	{
		get;
		set
		{
			if (field == value)
				return;
			
			field = value;
			isDirtyText = true;
		}
	} = string.Empty;
	public Font? font
	{
		get;
		set
		{
			if (field == value)
				return;
			
			field = value;
			isDirtyText = true;
		}
	}
	
	private bool isDirtyText;
	
	public Label(string? text = null, string? prefab = "")
	{
		if (text != null)
			this.text = text;
		
		captureCursorEvent = false;
		UIPrefab.Apply(prefab, this);
	}
	
	protected override void BeginUpdate()
	{
		if (isDirtyText)
			GenerateMeshes();
	}
	
	private void GenerateMeshes()
	{
		if (font == null)
			return;
		
		var meshes = new (Rect vertices, Region uvs)[text.Length];
		var position = Vector2.zero;
		for (var i = 0; i < text.Length; i++)
		{
			var c = text[i];
			
			if (c == '\n')
			{
				position.x = 0;
				position.y -= font.pixelSize;
				continue;
			}
			
			if (!font.glyphs.TryGetValue(c, out var glyph))
				glyph = font.glyphs['?'];
			
			var x = position.x + glyph.offset.x;
			var y = position.y - (glyph.size.y - glyph.offset.y);
			meshes[i] = (new Rect(new Vector2(x, y), glyph.size), glyph.uv);
			position.x += glyph.advance;
		}
		
		if (mesh?.vertices.Length == text.Length * 4)
			MeshFactory.SetQuadsVertices(mesh, meshes);
		else
		{
			meshOffset = new Vector2(0, -position.y + font.baseLine);
			mesh?.Dispose();
			mesh = MeshFactory.CreateQuads(meshes);
		}
		
		size = new Vector2(mesh.bounds.size.x, -position.y + font.pixelSize);
		isDirtyText = false;
		isDirty = true;
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(Label e)
	{
		var font = Vault.GetAsset<BitmapFont>(UIModule.DEFAULT_FONT)!;
		e.font = font.data;
		e.material = font.material;
		e.useMeshBoundsSize = false;
	}
}