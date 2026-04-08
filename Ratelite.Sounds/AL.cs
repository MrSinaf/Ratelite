using Ratelite.Sounds.Bindings;
using static Ratelite.Sounds.Bindings.ALNative;

namespace Ratelite.Sounds;

public static unsafe class AL
{
    public static void* OpenDevice()
        => alcOpenDevice(null);

    public static void CloseDevice(void* device)
        => alcCloseDevice(device);

    public static void* CreateContext(void* device)
        => alcCreateContext(device, null);

    public static void MakeContextCurrent(void* context)
        => alcMakeContextCurrent(context);

    public static void DestroyContext(void* context)
        => alcDestroyContext(context);

    public static uint GenSource()
    {
        uint id = 0;
        alGenSources(1, &id);
        return id;
    }

    public static void DeleteSource(uint source)
        => alDeleteSources(1, &source);

    public static void Source(uint source, ALSourceParam param, int value)
        => alSourcei(source, (int)param, value);

    public static void Source(uint source, ALSourceParam param, float value)
        => alSourcef(source, (int)param, value);

    public static void Source(uint source, ALSourceParam param, float x, float y, float z)
        => alSource3f(source, (int)param, x, y, z);

    public static void PlaySource(uint source)  => alSourcePlay(source);
    public static void StopSource(uint source)  => alSourceStop(source);
    public static void PauseSource(uint source) => alSourcePause(source);

    public static ALSourceState GetSourceState(uint source)
    {
        int state = 0;
        alGetSourcei(source, (int)ALSourceParam.State, &state);
        return (ALSourceState)state;
    }

    public static uint GenBuffer()
    {
        uint id = 0;
        alGenBuffers(1, &id);
        return id;
    }

    public static void DeleteBuffer(uint buffer)
        => alDeleteBuffers(1, &buffer);

    public static void BufferData(uint buffer, ALFormat format, void* data, int size, int frequency)
        => alBufferData(buffer, (int)format, data, size, frequency);

    public static ALError GetError()
        => (ALError)alGetError();
}