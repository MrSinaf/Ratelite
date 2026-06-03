using System.Reflection;
using Ratelite.Resources;

namespace Ratelite.Debugs.Windows;

public class VaultDebug
{
	private readonly Dictionary<string, AssetReference> assets;
	
	public bool show;
	public (string name, AssetReference assetRef)? selectedAsset;
	
	private int thumbnailSize = 64;
	private int spacing = 5;
	
	internal VaultDebug()
	{
		var cacheField = typeof(Vault).GetField(
			"assets",
			BindingFlags.NonPublic | BindingFlags.Static
		);
		assets = (Dictionary<string, AssetReference>)cacheField!.GetValue(null)!;
	}
	
	internal void Render()
	{
		if (show && ImGui.Begin("Vault", ref show, ImGuiWindowFlags.MenuBar))
		{
			ImGui.BeginMenuBar();
			{
				if (ImGui.BeginMenu("Options"))
				{
					ImGui.SliderInt("Thumbnail Size", ref thumbnailSize, 32, 256);
					ImGui.SliderInt("Spacing", ref spacing, 0, 20);
					ImGui.EndMenu();
				}
			}
			ImGui.EndMenuBar();
			
			if (selectedAsset != null)
			{
				ImGui.Columns(2);
				Listing();
				ImGui.NextColumn();
				SelectAsset();
				ImGui.Columns();
			}
			else
				Listing();
			
			ImGui.End();
		}
	}
	
	private void SelectAsset()
	{
		var asset = selectedAsset!.Value;
		if (ImGui.BeginChild("SelectAsset"))
		{
			if (asset.assetRef.asset is Texture2D texture)
				Texture2D_Property(texture);
			
			ImGui.EndChild();
		}
	}
	
	private void Listing()
	{
		if (ImGui.BeginChild("Listing"))
		{
			foreach (var (name, assetRef) in assets)
			{
				var cursorStart = ImGui.GetCursorPos();
				{
					var isSelected = selectedAsset?.name == name;
					if (ImGui.Selectable(
							$"##Selectable_{name}",
							isSelected,
							ImGuiSelectableFlags.None,
							new Vector2(0, thumbnailSize)
						))
						selectedAsset = (name, assetRef);
				}
				var cursorEnd = ImGui.GetCursorPos();
				
				ImGui.SetCursorPos(cursorStart);
				ImGui.BeginGroup();
				ImGui.PushID(name);
				{
					var thumbBoxSize = new Vector2(thumbnailSize);
					var thumbStart = ImGui.GetCursorScreenPos();
					
					ImGui.InvisibleButton($"##{name}_thumb", thumbBoxSize);
					
					var drawList = ImGui.GetWindowDrawList();
					drawList.AddRectFilled(
						thumbStart,
						thumbStart + thumbBoxSize,
						ImGui.GetColorU32(ImGuiCol.FrameBg),
						4f
					);
					
					if (assetRef.asset is Texture2D texture)
						Texture2D_Thumbnail(texture, drawList, thumbStart);
					
					ImGui.SameLine();
					
					ImGui.BeginGroup();
					{
						ImGui.Text(name);
						ImGui.TextColored(
							new Vector4(1, 1, 1, 0.5F),
							$" {assetRef.asset.GetType().Name}"
						);
					}
					ImGui.EndGroup();
				}
				ImGui.PopID();
				ImGui.EndGroup();
				
				ImGui.SetCursorPos(cursorEnd);
				ImGui.Dummy(new Vector2(0, spacing));
			}
			ImGui.EndChild();
		}
	}
	
	private void Texture2D_Thumbnail(Texture2D texture, ImDrawListPtr drawList, Vector2 thumbStart)
	{
		var textureWidth = texture.size.x;
		var textureHeight = texture.size.y;
		
		var scale = MathF.Min(
			1f * thumbnailSize / textureWidth,
			1f * thumbnailSize / textureHeight
		);
		
		var imageSize = new Vector2(
			textureWidth * scale,
			textureHeight * scale
		);
		
		var imagePos = thumbStart + (new Vector2(thumbnailSize) - imageSize) * 0.5f;
		
		drawList.AddImage(
			(IntPtr)texture.gTexture.handle,
			imagePos,
			imagePos + imageSize
		);
	}
	
	private void Texture2D_Property(Texture2D texture)
	{
		float sizeInBytes = texture.size.x * texture.size.y * 4;
		string sizeText;
		if (sizeInBytes < 1048576F)
		{
			var sizeInKo = sizeInBytes / 1024f;
			sizeText = $"{sizeInKo:F2} Ko";
		}
		else
		{
			var sizeInMo = sizeInBytes / 1048576F;
			sizeText = $"{sizeInMo:F2} Mo";
		}
		ImGui.Text($"{texture.size.x}x{texture.size.y} - " + sizeText);
		
		ImGui.Spacing();
		
		var availableWidth = ImGui.GetContentRegionAvail().x;
		var imageHeight = availableWidth * texture.size.y / texture.size.x;
		var imageSize = new Vector2(availableWidth, imageHeight);
		var cursorScreenPos = ImGui.GetCursorScreenPos();
		
		var drawList = ImGui.GetWindowDrawList();
		drawList.AddRectFilled(
			cursorScreenPos,
			cursorScreenPos + imageSize,
			ImGui.GetColorU32(ImGuiCol.FrameBg)
		);
		
		ImGui.Image(
			(IntPtr)texture.gTexture.handle,
			imageSize
		);
	}
}