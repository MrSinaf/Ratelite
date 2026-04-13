using Ratelite.Sounds.Bindings;
using Ratelite.Utils;

namespace Ratelite.Sounds;

public class AudioClip : IResource<AudioClip>
{
	public uint handle { get; private set; }
	
	private AudioClip(byte[] bytes, ALFormat format, int dataSize, int dataOffset, int sampleRate)
	{
		MainThreadQueue.Enqueue(() =>
			{
				unsafe
				{
					var buffer = AL.GenBuffer();
					fixed (byte* ptr = &bytes[dataOffset])
					{
						AL.BufferData(buffer, format, ptr, dataSize, sampleRate);
					}
					handle = buffer;
				}
			}
		);
	}
	
	public static AudioClip Load(VaultRessource ress)
	{
		// Ne support que le .wav
		const int dataOffset = 44;
		
		using var ms = new MemoryStream();
		ress.stream.CopyTo(ms);
		var bytes = ms.ToArray();
		
		var channels = BitConverter.ToInt16(bytes, 22);
		var sampleRate = BitConverter.ToInt32(bytes, 24);
		var bitDepth = BitConverter.ToInt16(bytes, 34);
		var dataSize = BitConverter.ToInt32(bytes, 40);
		
		var format = channels == 1
				? bitDepth == 8 ? ALFormat.Mono8 : ALFormat.Mono16
				: bitDepth == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
		
		return new AudioClip(bytes, format, dataSize, dataOffset, sampleRate);
	}
	
	public static bool ValidateExtension(string extension)
		=> extension == ".wav";
}