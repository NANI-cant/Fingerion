using System.Collections.Generic;

namespace Achievements {
    [System.Serializable]
    public class AchievementsProgress {
        public int Run = 0;
        public int Jump = 0;
        public int Buy = 0;
        public int Lose = 0;

        public List<int> Collected = new();
    }
}