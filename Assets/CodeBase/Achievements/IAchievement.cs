namespace Achievements {
    public interface IAchievement {
        int Id { get; }
        string Label { get; }
        int Reward { get; }
        int TargetValue { get; }

        public bool TryCollect(AchievementsProgress achievementsProgress);
    }
}