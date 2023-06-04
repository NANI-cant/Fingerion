namespace Metric {
    public interface IPlayerMetric {
        float Speed { get; }
        float JumpHeight { get; }
        float JumpDuration { get; }
        float FallSpeed { get; }
        float SlideDuration { get; }
        float GroundedBufferDistance { get; }
    }
}