using Ratelite.Resources;

namespace Ratelite.UI;

public class UIElement
{
	public string name = string.Empty;
	
	public UIElement? parent { get; private set; }
	public bool isDirty { get; protected set; }
	
	private readonly List<UIElement> children = [];
	public UIElement this[int index] => children[index];
	public UIElement[] childrenArray => children.ToArray();
	
	public bool isActif => active;
	public bool canDraw => isObservable && material is not null && mesh is { isValid: true };
	public bool isObservable => clipArea.size != Vector2Int.zero;
	
	public event Action<UIElement> cursorEnter = delegate { };
	public event Action<UIElement> cursorExit = delegate { };
	
	public Matrix3X3 matrix { get; private set; } = Matrix3X3.Identity();
	public Matrix3X3 inversedMatrix { get; private set; } = Matrix3X3.Identity();
	public Matrix3X3 rotatedMatrix { get; private set; } = Matrix3X3.Identity();
	
	public Vector2 realPosition { get; private set; }
	public Vector2 calculatePosition { get; private set; }
	public Vector2 realSize { get; private set; }
	public RegionInt clipArea { get; private set; }
	public bool isCursorOver { get; private set; }
	
	public virtual bool active { get; set; } = true;
	public bool captureCursorEvent { get; set; } = true;
	public bool isInteractif { get; set; } = true;
	
	public virtual Mesh? mesh { get; set; }
	public virtual MaterialUI? material { get; set; }
	public virtual Color tint { get; set; } = Color.white;
	public virtual float opacity { get; set; } = 1F;
	public virtual bool overflowHidden { get; set; } = true;
	public virtual Region uv { get; set; } = new (Vector2.zero, Vector2.one);
	
	public event Action<UIElement> elementChanged = delegate { };
	
	public virtual Vector2 position
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public virtual float rotation
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public virtual Vector2 size
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	} = Vector2.one;
	public virtual Vector2 scale
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	} = Vector2.one;
	public virtual Vector2 pivot
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public virtual Vector2 anchorMin
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public virtual Vector2 anchorMax
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public virtual Region margin
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public virtual Region padding
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public virtual Region cornerRadius
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	}
	public bool useMeshBoundsSize
	{
		get;
		set
		{
			field = value;
			isDirty = true;
		}
	} = true;
	
	protected Vector2 meshOffset = Vector2.zero;
	
	public Vector2 anchors { set => (anchorMin, anchorMax) = (value, value); }
	
	public void MoveOnTop()
	{
		if (parent == null)
			return;
		
		parent.children.Remove(this);
		parent.children.Add(this);
	}
	
	public virtual void AddChild(UIElement element)
	{
		element.parent?.RemoveChild(element);
		element.parent = this;
		children.Add(element);
	}
	
	public virtual UIElement GetChild(int index) => children[index];
	
	public virtual UIElement? SearchChild(Predicate<UIElement> predicate)
		=> children.Find(predicate);
	
	public virtual void RemoveChild(UIElement element)
	{
		children.Remove(element);
		element.parent = null;
	}
	
	public bool ContainsPoint(Vector2 point)
	{
		if (overflowHidden && point.IsOutsideBounds(clipArea.position00, clipArea.position11))
			return false;
		
		if (mesh is { isValid: true })
		{
			var localPoint = inversedMatrix.TransformPoint(point - mesh.bounds.position);
			return useMeshBoundsSize
					? localPoint.IsInsideBounds(mesh.bounds.position, mesh.bounds.size)
					: localPoint.IsInsideBounds(Vector2.zero, realSize);
		}
		
		return point.IsInsideBounds(realPosition, realPosition + realSize);
	}
	
	public void GetBoundingCorners(
		out Vector2 topLeft,
		out Vector2 topRight,
		out Vector2 bottomLeft,
		out Vector2 bottomRight
	)
	{
		topLeft = rotatedMatrix.TransformPoint(
			new Vector2(realPosition.x, realPosition.y)
		);
		topRight = rotatedMatrix.TransformPoint(
			new Vector2(realPosition.x + realSize.x, realPosition.y)
		);
		bottomLeft = rotatedMatrix.TransformPoint(
			new Vector2(realPosition.x, realPosition.y + realSize.y)
		);
		bottomRight = rotatedMatrix.TransformPoint(
			new Vector2(realPosition.x + realSize.x, realPosition.y + realSize.y)
		);
	}
	
	protected virtual void BeginUpdate() { }
	protected virtual void EndUpdate() { }
	protected virtual void BeginRender() { }
	protected virtual void EndRender() { }
	public virtual void OnDestroy() { }
	
	public void ForceUpdate(UIElement parent) => InternalUpdate(parent, null);
	
	public void Destroy()
	{
		OnDestroy();
		parent?.RemoveChild(this);
		foreach (var child in childrenArray)
			child.Destroy();
	}
	
	internal void OnCursorEnter()
	{
		isCursorOver = true;
		cursorEnter.Invoke(this);
	}
	
	internal void OnCursorExit()
	{
		isCursorOver = false;
		cursorExit.Invoke(this);
	}
	
	internal void InternalUpdate(UIElement parent, Stack<UIElement>? stackElementHover)
	{
		if (!isActif)
			return;
		
		BeginUpdate();
		if (this.parent != parent)
		{
			this.parent = parent;
			isDirty = true;
		}
		
		if (isDirty || parent.isDirty)
		{
			isDirty = true;
			CalculeMatrix();
		}
		
		if (stackElementHover != null)
		{
			if (ContainsPoint(R.game.window.cursorPosition))
				stackElementHover.Push(this);
			else
				stackElementHover = null;
		}
		
		foreach (var child in children)
			child.InternalUpdate(this, stackElementHover);
		isDirty = false;
		EndUpdate();
	}
	
	internal void InternalRender()
	{
		if (!isActif)
			return;
		
		BeginRender();
		if (canDraw)
		{
			material!.ApplyProperties();
			
			var shader = material.shader;
			shader.gProgram.SetUniform("u_model", matrix);
			shader.gProgram.SetUniform("u_tint", tint);
			shader.gProgram.SetUniform("u_opacity", opacity);
			shader.gProgram.SetUniform("u_modelSize", realSize);
			shader.gProgram.SetUniform("u_cornerRadius", cornerRadius);
			shader.gProgram.SetUniform("u_uv", uv);
			
			mesh!.Draw();
		}
		
		foreach (var child in children)
			child.InternalRender();
		EndRender();
	}
	
	private void CalculeMatrix()
	{
		Rect bounds;
		if (mesh != null)
		{
			bounds = new Rect(mesh.bounds.position, mesh.bounds.size);
			bounds.size = !useMeshBoundsSize ? size : bounds.size * size;
		}
		else
			bounds = new Rect(Vector2.zero, size);
		
		CalculeAnchors(bounds, out var cSize, out var cOffset);
		
		calculatePosition = realPosition = parent!.calculatePosition + position - cOffset;
		realSize = cSize;
		
		if (!useMeshBoundsSize && mesh != null)
			realPosition += mesh.bounds.position + meshOffset * scale;
		
		var pivotPosition = realPosition + pivot * realSize;
		rotatedMatrix = (rotation == 0
								? Matrix3X3.Identity()
								: Matrix3X3.CreateTranslation(-pivotPosition) *
								  Matrix3X3.CreateRotation(float.DegreesToRadians(rotation)) *
								  Matrix3X3.CreateTranslation(pivotPosition))
						* parent.rotatedMatrix;
		matrix = Matrix3X3.CreateScale(
					 useMeshBoundsSize ? realSize.ToVector2Int() : scale.ToVector2Int()
				 ) *
				 Matrix3X3.CreateTranslation(realPosition.ToVector2Int()) * rotatedMatrix;
		inversedMatrix = matrix.Inverse();
		CalculeClipArea();
		elementChanged(this);
	}
	
	private void CalculeAnchors(Rect bounds, out Vector2 cSize, out Vector2 cOffset)
	{
		var parentPadding = parent!.padding;
		var parentInnerSize = parent!.realSize - new Vector2(
			parentPadding.left + parentPadding.right,
			parentPadding.top + parentPadding.bottom
		);
		var offset = new Vector2(parentPadding.left, parentPadding.bottom);
		var anchorMinPos = parentInnerSize * anchorMin + offset;
		var anchorMaxPos = parentInnerSize * anchorMax + offset;
		
		cSize = Vector2.zero;
		cOffset = Vector2.zero;
		if (Math.Abs(anchorMin.x - anchorMax.x) > float.Epsilon) // Anchor horizontal
		{
			cSize.x = anchorMaxPos.x - anchorMinPos.x - margin.left - margin.right;
			cSize.x *= scale.x;
			cOffset.x = -margin.position00.x;
		}
		else
		{
			cSize.x = bounds.size.x * scale.x;
			cOffset.x = pivot.x * cSize.x +
						(useMeshBoundsSize ? bounds.position.x * cSize.x : bounds.position.x);
		}
		
		if (Math.Abs(anchorMin.y - anchorMax.y) > float.Epsilon) // Anchor vertical
		{
			cSize.y = anchorMaxPos.y - anchorMinPos.y - margin.top - margin.bottom;
			cSize.y *= scale.y;
			cOffset.y = -margin.position00.y;
		}
		else
		{
			cSize.y = bounds.size.y * scale.y;
			cOffset.y = pivot.y * cSize.y +
						(useMeshBoundsSize ? bounds.position.y * cSize.y : bounds.position.y);
		}
		
		cOffset -= anchorMinPos;
	}
	
	private void CalculeClipArea()
	{
		GetBoundingCorners(
			out var topLeft,
			out var topRight,
			out var bottomLeft,
			out var bottomRight
		);
		clipArea = new RegionInt(
			new Vector2Int(
				(int)MathF.Floor(
					MathF.Min(
						topLeft.x,
						MathF.Min(topRight.x, MathF.Min(bottomLeft.x, bottomRight.x))
					)
				),
				(int)MathF.Floor(
					MathF.Min(
						topLeft.y,
						MathF.Min(topRight.y, MathF.Min(bottomLeft.y, bottomRight.y))
					)
				)
			),
			new Vector2Int(
				(int)MathF.Ceiling(
					MathF.Max(
						topLeft.x,
						MathF.Max(topRight.x, MathF.Max(bottomLeft.x, bottomRight.x))
					)
				),
				(int)MathF.Ceiling(
					MathF.Max(
						topLeft.y,
						MathF.Max(topRight.y, MathF.Max(bottomLeft.y, bottomRight.y))
					)
				)
			)
		);
		if (overflowHidden)
			clipArea = clipArea.Intersection(parent!.clipArea);
	}
	
	public override string ToString() => name;
	
	~UIElement()
	{
		Destroy();
	}
}