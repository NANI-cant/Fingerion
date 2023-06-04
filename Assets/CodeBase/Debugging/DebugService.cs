using Architecture.Services.Gameplay;
using UnityEngine;
using Zenject;

namespace Debugging {
    public class DebugService: MonoBehaviour {
        private IAchievementService _achievementService;
        private IBankService _bankService;

        [Inject]
        public void Construct(
            IAchievementService achievementService,
            IBankService bankService
        ) {
            _achievementService = achievementService;
            _bankService = bankService;
        }

        [ContextMenu("Run 100")]
        public void Run100() {
            _achievementService.Progress.Run += 100;
            _achievementService.TryCollect();
        }
        
        [ContextMenu("Earn 100")]
        public void Earn100() {
            _bankService.Earn(100);
        }
        
        [ContextMenu("Spend 100")]
        public void Spend100() {
            _bankService.Spend(100);
        }
    }
}