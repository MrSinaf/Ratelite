using Ratelite.Sounds.Bindings;

namespace Ratelite.Sounds;

public class AudioSource : IDisposable
{
	private readonly uint handle = AL.GenSource();
	private bool isDisposed;
	
	public bool isPlaying => AL.GetSourceState(handle) == ALSourceState.Playing;
	
	public AudioClip? audio
	{
		get;
		set
		{
			field = value;
			AL.SetSource(handle, ALSourceParam.Buffer, (int)audio.handle);
		}
	}
	public float volume
	{
		get => AL.GetSourceF(handle, ALSourceParam.Gain);
		set => AL.SetSource(handle, ALSourceParam.Gain, value);
	}
	public float pitch
	{
		get => AL.GetSourceF(handle, ALSourceParam.Pitch);
		set => AL.SetSource(handle, ALSourceParam.Pitch, value);
	}
	public bool looping
	{
		get => AL.GetSourceB(handle, ALSourceParam.Looping);
		set => AL.SetSource(handle, ALSourceParam.Looping, value ? 1 : 0);
	}
	
	public void Play() => AL.PlaySource(handle);
	public void Pause() => AL.PauseSource(handle);
	public void Stop() => AL.StopSource(handle);
	
	public void Dispose()
	{
		if (isDisposed) return;
		isDisposed = true;
		
		AL.DeleteSource(handle);
		GC.SuppressFinalize(this);
	}
}