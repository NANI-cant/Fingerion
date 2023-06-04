using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using Gameplay.Environment.BackgroundGeneration;
using Metric;
using UnityEngine;
using Zenject;

namespace Architecture.Services.Gameplay.Impl {
    public class BackgroundParallaxService: ITickable {
        private readonly Background _background;
        private readonly ITimeProvider _timeProvider;
        private readonly IPlayerMetric _playerMetric;
        private readonly IGameMetric _gameMetric;

        private bool _isRunning;
        private float _distanceAccumulator;
        private BackgroundTile _targetTile;

        public BackgroundParallaxService(
            Background background,
            IMetricProvider metricProvider,
            ITimeProvider timeProvider
        ) {
            _background = background;
            _timeProvider = timeProvider;
            _playerMetric = metricProvider.PlayerMetric;
            _gameMetric = metricProvider.GameMetric;
        }

        public void Start() => _isRunning = true;
        public void Stop() => _isRunning = false;

        public void Tick() {
            if (!_isRunning) return;

            float parallaxSpeed = _gameMetric.CalculateParallaxSpeed(_playerMetric.Speed, _background.transform.position.z);
            float translation = parallaxSpeed * _timeProvider.DeltaTime;
            _background.transform.Translate(Vector3.left * translation, Space.World);
        }
    }
}