using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class ProgressBar : UIElement
{
	public readonly UIElement cursor;
	public event Action<float> onValueChanged = delegate { };
	
	private float _value;
	
	public float maxValue
	{
		get;
		set
		{
			field = value;
			CheckValue();
		}
	}
	
	public float minValue
	{
		get;
		set
		{
			field = value;
			CheckValue();
		}
	}
	
	public float value
	{
		get => _value;
		set
		{
			_value = value;
			CheckValue();
		}
	}
	
	public ProgressBar(float value, float minValue = 0, float maxValue = 1, string? prefab = "")
	{
		base.AddChild(cursor = new UIElement { name = "cursor" });
		
		this.maxValue = maxValue;
		this.minValue = minValue;
		this.value = value;
		
		UIPrefab.Apply(prefab, this);
	}
	
	private void CheckValue()
	{
		_value = _value > maxValue ? maxValue : _value < minValue ? minValue : _value;
		cursor.scale = new Vector2((_value - minValue) / (-minValue + maxValue), 1);
		onValueChanged(_value);
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefab(ProgressBar e)
	{
		e.cursor.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.cursor.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.cursor.anchorMin = Vector2.zero;
		e.cursor.anchorMax = Vector2.one;
		e.cursor.tint = new Color(0x00FF00);
		
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.size = new Vector2Int(200, 10);
	}
}