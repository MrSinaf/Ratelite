namespace Ratelite.Sounds;

public static class Audio
{
	public static float volume
	{
		get;
		set => AL.Volume(field = MathF.Max(0, MathF.Min(1, value)));
	}
}