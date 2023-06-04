using UnityEngine;

namespace Metric.Impl {
    [CreateAssetMenu(fileName = "Game", menuName = "Metric/Game")]
    public class GameMetric : ScriptableObject, IGameMetric {
        [SerializeField][Min(1)] private int _chunkBuffer = 3;
        [SerializeField][Min(1)] private int _minChunksForBiom = 5;
        [SerializeField][Min(1)] private int _maxChunksForBiom = 10;
        [SerializeField][Min(1)] private float _parallaxCoefficient = 1.2f;

        public int ChunkBuffer => _chunkBuffer;
        public int MinChunksForBiom => _minChunksForBiom;
        public int MaxChunksForBiom => _maxChunksForBiom;
        
        public float CalculateParallaxSpeed(float originalSpeed, float offset) {
            return originalSpeed * Mathf.Pow(_parallaxCoefficient, -offset);
        }
    }
}