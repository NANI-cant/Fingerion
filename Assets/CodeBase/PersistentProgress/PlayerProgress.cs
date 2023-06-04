using System;
using System.Collections.Generic;
using Achievements;

namespace PersistentProgress {
    [Serializable]
    public class PlayerProgress {
        public int BestScore = 0;
        public int BankAmount = 0;
        public int SelectedItem = 0;
        public List<int> BuyedItems = new ();
        public AchievementsProgress AchievementsProgress = new();
        public float SoundsVolume = 1;
        public float MusicVolume = 1;
        public int Language = 0;
    }
}