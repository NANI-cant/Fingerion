using Achievements;

namespace Architecture.Services.Gameplay {
    public interface IAchievementService {
        AchievementsProgress Progress { get; }
        public void TryCollect();
    }
}