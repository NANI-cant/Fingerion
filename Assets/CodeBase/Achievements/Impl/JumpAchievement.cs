using UnityEngine;

namespace Achievements.Impl {
    [CreateAssetMenu(fileName = "Jump Achievement", menuName = "Achievements/Jump Achievement")]
    public class JumpAchievement : Achievement {
        public override bool TryCollect(AchievementsProgress achievementsProgress) {
            return achievementsProgress.Jump >= TargetValue;
        }
    }
}