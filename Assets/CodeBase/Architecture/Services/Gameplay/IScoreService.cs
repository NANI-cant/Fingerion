using System;

namespace Architecture.Services.Gameplay {
    public interface IScoreService {
        event Action Modified;
        
        public int BestScore { get; }
        public int CurrentScore { get; }
        
        void Start();
        void Stop();
    }
}