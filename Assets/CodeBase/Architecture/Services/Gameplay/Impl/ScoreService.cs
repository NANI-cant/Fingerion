using System;
using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Metric;
using Zenject;

namespace Architecture.Services.Gameplay.Impl {
    public class ScoreService : IScoreService, ITickable {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ITimeProvider _timeProvider;
        private readonly IPlayerMetric _metric;

        private bool _running;
        private float _currentScore;

        public int BestScore { get; private set; }
        public int CurrentScore => (int) _currentScore;
        
        public event Action Modified;

        public ScoreService(
            IPersistentProgressService persistentProgressService,
            IMetricProvider metricProvider,
            ITimeProvider timeProvider
        ) {
            _persistentProgressService = persistentProgressService;
            _timeProvider = timeProvider;
            _metric = metricProvider.PlayerMetric;

            BestScore = _persistentProgressService.Progress.BestScore;
        }

        public void Start() => _running = true;

        public void Stop() {
            _running = false;

            if (CurrentScore >= BestScore) {
                BestScore = CurrentScore;
                _persistentProgressService.Progress.BestScore = BestScore;
                _persistentProgressService.Save();   
            }
            
            Modified?.Invoke();
        }

        public void Tick() {
            if(!_running) return;

            _currentScore += _timeProvider.DeltaTime * _metric.Speed;
            Modified?.Invoke();
        }
    }
}