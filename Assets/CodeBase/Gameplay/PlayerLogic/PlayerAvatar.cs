using Audio;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class PlayerAvatar: MonoBehaviour {
        [SerializeField] private Animator _animator;
        [SerializeField] private Source _audioSource;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip _jumpClip;
        [SerializeField] private AudioClip _hitClip;
        
        private static readonly int GroundedBool = Animator.StringToHash("Grounded");
        private static readonly int RunningBool = Animator.StringToHash("Running");
        
        private GroundDetector _groundDetector;
        
        private const string IdleKey = "Idle";
        private const string SlideKey = "Slide";
        private const string JumpKey = "Jump";
        private const string LandKey = "Land";
        private const string HitKey = "Hit";
        private const string StandKey = "Stand";

        private void Awake() {
            _groundDetector = GetComponent<GroundDetector>();
        }

        private void Update() {
            _animator.SetBool(GroundedBool, _groundDetector.IsBufferGrounded);
        }

        public void Idle() {
            _animator.SetBool(RunningBool, false);
            _animator.Play(IdleKey);
        }
        
        public void Run() => _animator.SetBool(RunningBool, true);

        public void Jump() {
            _animator.Play(JumpKey);
            _audioSource.Play(_jumpClip);
        }

        public void Hit() {
            _animator.Play(HitKey);
            _audioSource.Play(_hitClip);
        }

        public void Slide() => _animator.Play(SlideKey);
        public void Land() => _animator.Play(LandKey);
        public void Stand() => _animator.Play(StandKey);
    }
}