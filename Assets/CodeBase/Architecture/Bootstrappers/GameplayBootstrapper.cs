using Achievements.Detecting;
using Architecture.Services.AssetProviding.Impl;
using Architecture.Services.Factories.Impl;
using Architecture.Services.Gameplay.Impl;
using Architecture.StateMachine;
using Gameplay.Environment.BackgroundGeneration;
using Gameplay.Environment.ChunkGeneration.Impl;
using Gameplay.Setup.SpawnPoints;
using Gameplay.Setup.SpawnPoints.Impl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Architecture.Bootstrappers {
    public class GameplayBootstrapper: MonoInstaller {
        [SerializeField] private Chunk _startChunk;
        [SerializeField] private Background _backgroundContainer;
        [SerializeField] private Button _startButton;
        
        public override void InstallBindings() {
            BindPlayerSpawnPoint();

            BindService<GameStateMachine>();

            BindService<GameplayFactory>();

            BindService<PlayerPointer>();
            //BindService<ChunkGenerator>();
            //BindService<BackgroundGenerationService>();
            BindService<ChunkTranslation>();
            BindService<BiomGenerationService>();
            BindService<BackgroundParallaxService>();


            BindService<RunDetector>();
            BindService<LoseDetector>();
            BindService<BuyDetector>();

            BindService<ScoreService>();
            BindService<RingService>();

            Container.Bind<Button>().FromInstance(_startButton).AsSingle().NonLazy();
            Container.Bind<Background>().FromInstance(_backgroundContainer).AsSingle().NonLazy();
            Container.Bind<Chunk>().FromInstance(_startChunk).AsSingle().NonLazy();
        }

        private void BindPlayerSpawnPoint() {
            var playerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            Container.Bind<IPlayerSpawnPoint>().FromInstance(playerSpawnPoint).AsSingle().NonLazy();
        }

        private void BindService<TService>() 
            => Container.BindInterfacesAndSelfTo<TService>().AsSingle().NonLazy();
    }
}