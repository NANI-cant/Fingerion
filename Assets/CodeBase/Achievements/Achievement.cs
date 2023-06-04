using UnityEngine;

namespace Achievements {
    public abstract class Achievement : ScriptableObject, IAchievement {
        [SerializeField] private int _id;
        [SerializeField] private string _label;
        [SerializeField][Min(0)] private int _reward;
        [SerializeField] private int _targetValue;

        public int Id => _id;
        public string Label => _label;
        public int Reward => _reward;
        public int TargetValue => _targetValue;
        
        public abstract bool TryCollect(AchievementsProgress achievementsProgress);
    }
}