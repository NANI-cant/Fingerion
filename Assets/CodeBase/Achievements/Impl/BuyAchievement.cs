using UnityEngine;

namespace Achievements.Impl {
    [CreateAssetMenu(fileName = "Buy Achievement", menuName = "Achievements/Buy Achievement")]
    public class BuyAchievement : Achievement {
        public override bool TryCollect(AchievementsProgress achievementsProgress) {
            return achievementsProgress.Buy >= TargetValue;
        }
    }
}