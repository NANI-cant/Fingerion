using UnityEngine;

namespace Metric.Impl {
    [CreateAssetMenu(fileName = "PlayerMetric", menuName = "Metric/Player")]
    public class PlayerMetric: ScriptableObject, IPlayerMetric {
        [Header("Running")]
        [SerializeField][Min(float.Epsilon)] private float _speed;

        [Header("Jumping")]
        [SerializeField][Min(float.Epsilon)] private float _jumpHeight;
        [SerializeField][Min(float.Epsilon)] private float _jumpDuration;
        
        [Header("Sliding")]
        [SerializeField][Min(float.Epsilon)] private float _fallSpeed;
        [SerializeField][Min(float.Epsilon)] private float _slideDuration;

        [Header("Platforming expanses")] 
        [SerializeField] [Min(float.Epsilon)] private float _groundedBufferDistance = 0.1f;

        public float Speed => _speed;
        public float JumpHeight => _jumpHeight;
        public float JumpDuration => _jumpDuration;
        public float FallSpeed => _fallSpeed;
        public float SlideDuration => _slideDuration;
        public float GroundedBufferDistance => _groundedBufferDistance;
    }
}