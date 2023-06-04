namespace Metric {
    public interface IGameMetric {
        int ChunkBuffer { get; }
        int MinChunksForBiom { get; }
        int MaxChunksForBiom { get; }

        float CalculateParallaxSpeed(float originalSpeed, float offset);
    }
}