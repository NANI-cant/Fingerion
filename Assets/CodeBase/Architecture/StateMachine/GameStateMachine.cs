using System;
using System.Collections.Generic;
using Achievements.Detecting;
using Ads;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.General;
using Architecture.Services.UI;
using Architecture.StateMachine.States;
using Gameplay.Setup.SpawnPoints;
using General.StateMachine;
using UnityEngine.UI;
using Zenject;

namespace Architecture.StateMachine {
    public class GameStateMachine: General.StateMachine.StateMachine, IInitializable {
        protected override Dictionary<Type, State> States { get; }

        public GameStateMachine(IGameplayFactory gameplayFactory,
            IPlayerSpawnPoint playerSpawnPoint,
            IPlayerPointer playerPointer,
            ChunkTranslation chunkTranslation,
            IInputService inputService,
            IUIService uiService,
            ISceneLoadService sceneLoadService,
            BackgroundParallaxService backgroundParallax,
            RunDetector runDetector,
            Button startButton,
            LoseDetector loseDetector,
            IScoreService scoreService,
            IBiomGenerationService biomGenerationService, 
            IBiomService biomService,
            IAdService adService
        ) {
            States = new Dictionary<Type, State>() {
                [typeof(InitializeState)] = new InitializeState(this, gameplayFactory, playerSpawnPoint, playerPointer, uiService, inputService, startButton, adService),
                [typeof(GameLoopState)] = new GameLoopState(this, playerPointer, biomGenerationService, chunkTranslation, inputService, backgroundParallax, runDetector, scoreService, uiService, biomService),
                [typeof(LoseState)] = new LoseState(this, playerPointer, inputService, uiService, sceneLoadService, loseDetector, scoreService),
            };
        }

        public void Initialize() => TranslateTo<InitializeState>();
    }
}