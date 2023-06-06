using Achievements.Detecting;
using Architecture.Services.AssetProviding;
using Architecture.Services.Gameplay;
using Architecture.Services.General;
using Audio;
using Gameplay.Environment.BackgroundGeneration;
using Gameplay.Environment.ChunkGeneration.Impl;
using Gameplay.PlayerLogic;
using Rings;
using UnityEngine;

namespace Architecture.Services.Factories.Impl {
    public class GameplayFactory : IGameplayFactory {
        private readonly IPrefabProvider _prefabProvider;
        private readonly IInstantiateProvider _instantiateProvider;
        private readonly IMetricProvider _metricProvider;
        private readonly IInputService _inputService;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IRandomService _randomService;
        private readonly IBiomService _biomService;
        private readonly IAchievementService _achievementService;
        private readonly IAudioService _audioService;
        private readonly IRingService _ringService;

        public GameplayFactory(
            IPrefabProvider prefabProvider,
            IInstantiateProvider instantiateProvider,
            IMetricProvider metricProvider,
            IInputService inputService,
            ISchedulerFactory schedulerFactory,
            IRandomService randomService,
            IBiomService biomService,
            IAchievementService achievementService,
            IAudioService audioService,
            IRingService ringService
        ) {
            _prefabProvider = prefabProvider;
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
            _inputService = inputService;
            _schedulerFactory = schedulerFactory;
            _randomService = randomService;
            _biomService = biomService;
            _achievementService = achievementService;
            _audioService = audioService;
            _ringService = ringService;
        }
        
        public GameObject CreatePlayer(Vector3 position, Quaternion rotation) {
            var metric = _metricProvider.PlayerMetric;
            var player = _instantiateProvider.Instantiate(_prefabProvider.Player, position, rotation);

            player.GetComponent<Rigidbody2D>().gravityScale = 2 * metric.JumpHeight / Mathf.Pow(metric.JumpDuration / 2, 2);
            player.GetComponent<Player>().Construct(_inputService);
            player.GetComponent<GroundDetector>().Construct(metric.GroundedBufferDistance);
            player.GetComponent<Jumper>().Construct(metric.JumpHeight, metric.JumpDuration);
            player.GetComponent<Slider>().Construct(metric.FallSpeed, metric.SlideDuration, _schedulerFactory);
            player.GetComponentInChildren<RingGameplayView>().Construct(_ringService);
            ConstructAudio(player);

            return player;
        }

        public BackgroundTile CreateTile(Background background) {
            return _instantiateProvider.Instantiate(_biomService.Biom.Tile, background.transform);
        }

        public Chunk CreateChunk(Chunk lastChunk) {
            Chunk chosenChunk = null;
            
            while (chosenChunk == null) {
                int index = _randomService.Range(0, _biomService.Biom.Chunks.Length);
                var chunkTemplate = _biomService.Biom.Chunks[index].GetComponent<Chunk>();
                if (lastChunk.AbleToConcatWith(chunkTemplate)) {
                    chosenChunk = _instantiateProvider.Instantiate(chunkTemplate, Vector3.zero, Quaternion.identity);
                }
            }

            foreach (var jumpDetector in chosenChunk.GetComponentsInChildren<JumpDetector>()) {
                jumpDetector.Construct(_achievementService);
            }
            ConstructAudio(chosenChunk.gameObject);

            return chosenChunk;
        }

        private void ConstructAudio(GameObject gameObject) 
            => gameObject.DoForComponentsInChildren<Source>(source => source.Construct(_audioService));
    }
}