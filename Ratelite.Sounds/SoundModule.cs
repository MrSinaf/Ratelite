namespace Ratelite.Sounds;

public unsafe class SoundModule : IDisposableModule
{
	public int priority => 0;
	
	private void* device;
	private void* context;
	
	public void Init()
	{
		device  = AL.OpenDevice();
		context = AL.CreateContext(device);
		AL.MakeContextCurrent(context);
		
		
	}
	
	public void Dispose()
	{
		AL.DestroyContext(context);
		AL.CloseDevice(device);
	}
}