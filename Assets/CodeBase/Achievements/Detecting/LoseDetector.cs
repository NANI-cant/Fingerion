using Architecture.Services.Gameplay;

namespace Achievements.Detecting {
    public class LoseDetector {
        private readonly IAchievementService _achievementService;

        public LoseDetector(IAchievementService achievementService) {
            _achievementService = achievementService;
        }
        
        public void Detect() {
            _achievementService.Progress.Lose++;
            _achievementService.TryCollect();
        }
    }
}