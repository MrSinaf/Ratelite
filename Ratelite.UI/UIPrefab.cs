using System.Reflection;
using JetBrains.Annotations;

namespace Ratelite.UI;

public static class UIPrefab
{
	private static readonly Dictionary<Type, Dictionary<string, Action<UIElement>>> prefabs = [];
	
	public static void Add<T>(string name, Action<T> action) where T : UIElement
	{
		if (!prefabs.TryGetValue(typeof(T), out var actions))
			prefabs[typeof(T)] = actions = new Dictionary<string, Action<UIElement>>();
		
		actions[name] = element => action(element as T ?? throw new InvalidCastException());
	}
	
	public static void Remove<T>(string name) where T : UIElement
	{
		if (prefabs.TryGetValue(typeof(T), out var actions))
		{
			actions.Remove(name);
			if (actions.Count == 0)
				prefabs.Remove(typeof(T));
		}
	}
	
	public static void Apply<T>(string name, T element) where T : UIElement
	{
		if (!prefabs.TryGetValue(typeof(T), out var actions))
		{
			if (name == string.Empty)
			{
				var method = typeof(T).GetMethods()
									  .FirstOrDefault(method
											  => method.GetCustomAttribute<IsDefaultPrefab>() !=
												 null
									  );
				if (method != null)
				{
					Add<T>(string.Empty, element => method.Invoke(null, [element]));
					method.Invoke(null, [element]);
					return;
				}
			}
			
			throw new NullReferenceException(
				$"The {(name == string.Empty ? "default prefab" : $"prefab '{name}'")} " +
				$"for {typeof(T)} is not found!"
			);
		}
		
		if (actions.TryGetValue(name, out var action))
			action.Invoke(element);
		else
			throw new NullReferenceException(">" + name + "< is not a valid prefab name!");
	}
	
	public static void WithPrefrab<T>(this T element, string name) where T : UIElement
		=> Apply(name, element);
}

[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public class IsDefaultPrefab : Attribute;