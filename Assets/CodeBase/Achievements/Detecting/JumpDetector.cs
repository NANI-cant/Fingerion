using Architecture.Services.Gameplay;
using Gameplay.PlayerLogic;
using UnityEngine;

namespace Achievements.Detecting {
    [RequireComponent(typeof(Collider2D))]
    public class JumpDetector: MonoBehaviour {
        private IAchievementService _achievementService;
        private Collider2D _collider;

        private void Awake() {
            _collider = GetComponent<Collider2D>();
        }

        public void Construct(IAchievementService achievementService) {
            _achievementService = achievementService;
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.TryGetComponent<Player>(out var player)) return;
            
            _collider.enabled = false;
            _achievementService.Progress.Jump++;
            _achievementService.TryCollect();
            Debug.Log("Jump" + _achievementService.Progress.Jump);
        }
    }
}