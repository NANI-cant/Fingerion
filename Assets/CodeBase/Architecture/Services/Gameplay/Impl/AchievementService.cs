using Achievements;
using Architecture.Services.AssetProviding;
using Architecture.Services.PersistentProgress;
using UnityEngine;

namespace Architecture.Services.Gameplay.Impl {
    public class AchievementService : IAchievementService {
        private readonly IBankService _bankService;
        private readonly IMetricProvider _metricProvider;
        private readonly IPersistentProgressService _persistentProgressService;

        public AchievementsProgress Progress { get; private set; }

        public AchievementService(
            IBankService bankService, 
            IMetricProvider metricProvider,
            IPersistentProgressService persistentProgressService
        ) {
            _bankService = bankService;
            _metricProvider = metricProvider;
            _persistentProgressService = persistentProgressService;
            
            if (_persistentProgressService.Progress == null) {
                _persistentProgressService.Loaded += InitializeDelayed;
            }
            else {
                Progress = _persistentProgressService.Progress.AchievementsProgress;   
            }
        }
        
        private void InitializeDelayed() {
            Progress = _persistentProgressService.Progress.AchievementsProgress;
            _persistentProgressService.Loaded -= InitializeDelayed;
        }

        public void TryCollect() {
            foreach (var achievement in _metricProvider.Achievements) {
                if (Progress.Collected.Contains(achievement.Id)) continue;
                if (!achievement.TryCollect(Progress)) continue;
                
                Progress.Collected.Add(achievement.Id);
                _bankService.Earn(achievement.Reward);
                _persistentProgressService.Progress.AchievementsProgress = Progress;
                _persistentProgressService.Save();
            }
        }
    }
}