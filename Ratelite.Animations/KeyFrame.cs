namespace Ratelite.Animations;

public record struct KeyFrame<T>(T value, float time, bool lerp = false);