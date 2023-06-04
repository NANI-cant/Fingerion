using UnityEngine;

namespace Achievements.Impl {
    [CreateAssetMenu(fileName = "Lose Achievement", menuName = "Achievements/Lose Achievement")]
    public class LoseAchievement : Achievement {
        public override bool TryCollect(AchievementsProgress achievementsProgress) {
            return achievementsProgress.Lose >= TargetValue;
        }
    }
}