using Ratelite.Utils;

namespace Ratelite.Sounds;

public unsafe class SoundModule : ILoadableModule, IDisposableModule
{
	public int priority => 0;
	
	private void* device;
	private void* context;
	
	public void Init()
	{
	}
	
	public Task Load()
	{
		MainThreadQueue.Enqueue(() =>
		{
			device  = AL.OpenDevice();
			context = AL.CreateContext(device);
			AL.MakeContextCurrent(context);
		});
		
		MainThreadQueue.Wait().Wait();
		return Task.CompletedTask;
	}
	
	public void Dispose()
	{
		AL.DestroyContext(context);
		AL.CloseDevice(device);
	}
}