using System.Runtime.InteropServices;

namespace Ratelite.Sounds.Bindings;

internal static unsafe partial class ALNative
{
	[LibraryImport("soft_oal")]
	internal static partial void* alcOpenDevice(byte* devicename);
	[LibraryImport("soft_oal")]
	internal static partial void alcCloseDevice(void* device);
	// Context actions
	[LibraryImport("soft_oal")]
	internal static partial void* alcCreateContext(void* device, int* attrlist);
	[LibraryImport("soft_oal")]
	internal static partial void alcDestroyContext(void* context);
	[LibraryImport("soft_oal")]
	internal static partial void alcMakeContextCurrent(void* context);
	// Source actions
	[LibraryImport("soft_oal")]
	internal static partial void alGenSources(int n, uint* sources);
	[LibraryImport("soft_oal")]
	internal static partial void alDeleteSources(int n, uint* sources);
	[LibraryImport("soft_oal")]
	internal static partial void alSourcePlay(uint source);
	[LibraryImport("soft_oal")]
	internal static partial void alSourceStop(uint source);
	[LibraryImport("soft_oal")]
	internal static partial void alSourcePause(uint source);
	// Source params
	[LibraryImport("soft_oal")]
	internal static partial void alSourcei(uint source, int param, int value);
	[LibraryImport("soft_oal")]
	internal static partial void alSourcef(uint source, int param, float value);
	[LibraryImport("soft_oal")]
	internal static partial void alGetSourcei(uint source, int param, int* value);
	[LibraryImport("soft_oal")]
	internal static partial void alGetSourcef(uint source, int param, float* value);
	// Buffer actions
	[LibraryImport("soft_oal")]
	internal static partial void alGenBuffers(int n, uint* buffers);
	[LibraryImport("soft_oal")]
	internal static partial void alDeleteBuffers(int n, uint* buffers);
	// Buffer params
	[LibraryImport("soft_oal")]
	internal static partial void alBufferi(uint source, int param, int value);
	[LibraryImport("soft_oal")]
	internal static partial void alGetBufferi(uint source, int param, int* value);
	
	[LibraryImport("soft_oal")]
	internal static partial void alListenerf(int param, float value);
	
	[LibraryImport("soft_oal")]
	internal static partial void alBufferData(
		uint buffer,
		int format,
		void* data,
		int size,
		int freq
	);
	[LibraryImport("soft_oal")]
	internal static partial int alGetError();
	
}