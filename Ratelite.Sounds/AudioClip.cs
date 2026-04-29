using Ratelite.Sounds.Bindings;
using Ratelite.Sounds.Bindings.StbVorbis;
using Ratelite.Utils;

namespace Ratelite.Sounds;

public class AudioClip : IResourceAsync<AudioClip>
{
	public uint handle { get; private set; }
	public required float duration;
	
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
	
	private AudioClip(byte[] pcmBytes, ALFormat format, int sampleRate)
			: this(pcmBytes, format, pcmBytes.Length, 0, sampleRate) { }
	
	/*
		TODO > Comme nous ne sommes normalement pas en ASYNC il serait peut-être mieux de gérer le
		cas où il n'est pas nécessaire de l'écuter dans le MainThreadQueue
	*/
	public static AudioClip Load(VaultRessource ress) => ress.extension switch
	{
		".wav" => LoadWav(ress),
		".ogg" => LoadOgg(ress),
		_      => throw new Exception("Unsupported audio format (•_•)")
	};
	
	public static async Task<AudioClip> LoadAsync(VaultRessource ress)
	{
		var audio = Load(ress);
		await MainThreadQueue.Wait();
		return audio;
	}
	
	private static AudioClip LoadOgg(VaultRessource ress)
	{
		using var ms = new MemoryStream();
		ress.stream.CopyTo(ms);
		var bytes = ms.ToArray();
		
		using var vorbis = Vorbis.FromMemory(bytes);
		
		var channels = vorbis.Channels;
		var sampleRate = vorbis.SampleRate;
		
		var pcm = new List<short>(bytes.Length);
		vorbis.Restart();
		
		while (true)
		{
			vorbis.SubmitBuffer();
			if (vorbis.Decoded <= 0)
				break;
			
			for (var i = 0; i < vorbis.Decoded * channels; i++)
				pcm.Add(vorbis.SongBuffer[i]);
		}
		
		var pcmShorts = pcm.ToArray();
		var pcmBytes = new byte[pcmShorts.Length * sizeof(short)];
		Buffer.BlockCopy(pcmShorts, 0, pcmBytes, 0, pcmBytes.Length);
		
		var format = channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16;
		return new AudioClip(pcmBytes, format, sampleRate)
		{
			duration = pcmShorts.Length / (float)(channels * sampleRate)
		};
	}
	
	private static AudioClip LoadWav(VaultRessource ress)
	{
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
		
		return new AudioClip(bytes, format, dataSize, dataOffset, sampleRate)
		{
			duration = dataSize / (sampleRate * channels * (bitDepth / 8f))
		};
	}
	
	public static bool ValidateExtension(string extension)
		=> extension is ".wav" or ".ogg";
}