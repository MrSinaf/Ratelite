using Ratelite.Sounds.Bindings;
using Ratelite.Sounds.Bindings.StbVorbis;

namespace Ratelite.Sounds;

public class AudioClip : IResourceAsync<AudioClip>
{
	public readonly uint handle;
	public readonly float duration;
	
	private AudioClip(
		byte[] bytes,
		ALFormat format,
		int dataSize,
		int dataOffset,
		int sampleRate,
		float duration
	)
	{
		unsafe
		{
			MainThread.Assert();
			this.duration = duration;
			var buffer = AL.GenBuffer();
			fixed (byte* ptr = &bytes[dataOffset])
			{
				AL.BufferData(buffer, format, ptr, dataSize, sampleRate);
			}
			handle = buffer;
		}
	}
	
	public static AudioClip Load(VaultRessource ress)
	{
		var result = ress.extension switch
		{
			".wav" => LoadWav(ress),
			".ogg" => LoadOgg(ress),
			_      => throw new Exception("Unsupported audio format (•_•)")
		};
		return new AudioClip(
			result.bytes, result.format, result.dataSize, result.dataOffset, result.sampleRate,
			result.duration
		);
	}
	
	public static async Task<AudioClip> LoadAsync(VaultRessource ress)
	{
		var result = ress.extension switch
		{
			".wav" => await LoadWavAsync(ress),
			".ogg" => await LoadOggAsync(ress),
			_      => throw new Exception("Unsupported audio format (•_•)")
		};
		return (await MainThread.EnqueueAndWaitAsync(() => new AudioClip(
			result.bytes, result.format, result.dataSize, result.dataOffset, result.sampleRate,
			result.duration
		)))!;
	}
	
	private static (byte[] bytes, ALFormat format, int dataSize, int dataOffset, int sampleRate,
			float duration) LoadOgg(VaultRessource ress)
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
		
		return (
			pcmBytes,
			channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16,
			pcmBytes.Length,
			0,
			sampleRate,
			pcmShorts.Length / (float)(channels * sampleRate)
		);
	}
	
	private static async Task<(byte[] bytes, ALFormat format, int dataSize, int dataOffset, int
		sampleRate, float duration)> LoadOggAsync(VaultRessource ress)
	{
		using var ms = new MemoryStream();
		await ress.stream.CopyToAsync(ms);
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
		
		return (
			pcmBytes,
			channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16,
			pcmBytes.Length,
			0,
			sampleRate,
			pcmShorts.Length / (float)(channels * sampleRate)
		);
	}
	
	private static (byte[] bytes, ALFormat format, int dataSize, int dataOffset,
			int sampleRate, float duration) LoadWav(VaultRessource ress)
	{
		using var ms = new MemoryStream();
		ress.stream.CopyTo(ms);
		var bytes = ms.ToArray();
		
		var channels = BitConverter.ToInt16(bytes, 22);
		var sampleRate = BitConverter.ToInt32(bytes, 24);
		var bitDepth = BitConverter.ToInt16(bytes, 34);
		var dataSize = BitConverter.ToInt32(bytes, 40);
		
		return (
			bytes,
			channels == 1
					? bitDepth == 8 ? ALFormat.Mono8 : ALFormat.Mono16
					: bitDepth == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16,
			dataSize,
			44,
			sampleRate,
			dataSize / (sampleRate * channels * (bitDepth / 8f))
		);
	}
	
	private static async Task<(byte[] bytes, ALFormat format, int dataSize, int dataOffset,
		int sampleRate, float duration)> LoadWavAsync(VaultRessource ress)
	{
		using var ms = new MemoryStream();
		await ress.stream.CopyToAsync(ms);
		var bytes = ms.ToArray();
		
		var channels = BitConverter.ToInt16(bytes, 22);
		var sampleRate = BitConverter.ToInt32(bytes, 24);
		var bitDepth = BitConverter.ToInt16(bytes, 34);
		var dataSize = BitConverter.ToInt32(bytes, 40);
		
		return (
			bytes,
			channels == 1
					? bitDepth == 8 ? ALFormat.Mono8 : ALFormat.Mono16
					: bitDepth == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16,
			dataSize,
			44,
			sampleRate,
			dataSize / (sampleRate * channels * (bitDepth / 8f))
		);
	}
	
	public static bool ValidateExtension(string extension)
		=> extension is ".wav" or ".ogg";
}