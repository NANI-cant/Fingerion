using UnityEngine;

namespace Achievements.Impl {
    [CreateAssetMenu(fileName = "RunAchievement", menuName = "Achievements/Run Achievement")]
    public class RunAchievement : Achievement {
        public override bool TryCollect(AchievementsProgress achievementsProgress) {
            return achievementsProgress.Run >= TargetValue;
        }
    }
}