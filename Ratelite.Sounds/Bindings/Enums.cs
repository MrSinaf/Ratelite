namespace Ratelite.Sounds.Bindings;

public enum ALFormat
{
	Mono8 = 0x1100,
	Mono16 = 0x1101,
	Stereo8 = 0x1102,
	Stereo16 = 0x1103,
}

public enum ALSourceState
{
	Initial = 0x1011,
	Playing = 0x1012,
	Paused = 0x1013,
	Stopped = 0x1014,
}

public enum ALSourceParam
{
	Pitch = 0x1003,
	Gain = 0x100A,
	Position = 0x1004,
	Velocity = 0x1006,
	Looping = 0x1007,
	Buffer = 0x1009,
	State = 0x1010,
	
	SecOffset = 0x1024,
	SampleOffset = 0x1025,
	ByteOffset = 0x1026
}

public enum ALBufferParam
{
	Frequency = 0x2001,
	Bits = 0x2002,
	Channels = 0x2003,
	Size = 0x2004
}

public enum ALError
{
	NoError = 0,
	InvalidName = 0xA001,
	InvalidEnum = 0xA002,
	InvalidValue = 0xA003,
	InvalidOperation = 0xA004,
	OutOfMemory = 0xA005,
}