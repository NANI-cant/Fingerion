using Ads;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.UI;
using Gameplay.PlayerLogic;
using Gameplay.Setup.SpawnPoints;
using General.StateMachine;
using UI;
using UI.Screens;
using UnityEngine.UI;

namespace Architecture.StateMachine.States {
    public class InitializeState : State {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameplayFactory _gameplayFactory;
        private readonly IPlayerSpawnPoint _playerSpawnPoint;
        private readonly IPlayerPointer _playerPointer;
        private readonly IUIService _uiService;
        private readonly IInputService _inputService;
        private readonly Button _startButton;
        private readonly IAdService _adService;

        public InitializeState(GameStateMachine gameStateMachine,
            IGameplayFactory gameplayFactory,
            IPlayerSpawnPoint playerSpawnPoint,
            IPlayerPointer playerPointer,
            IUIService uiService,
            IInputService inputService,
            Button startButton, 
            IAdService adService
        ) {
            _gameStateMachine = gameStateMachine;
            _gameplayFactory = gameplayFactory;
            _playerSpawnPoint = playerSpawnPoint;
            _playerPointer = playerPointer;
            _uiService = uiService;
            _inputService = inputService;
            _startButton = startButton;
            _adService = adService;
        }
        
        public override void Enter() {
            _adService.ShowInterstitial();
            _playerPointer.Player = _gameplayFactory
                .CreatePlayer(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation)
                .GetComponent<Player>();

            _inputService.Disable();
            _uiService.SwitchScreen(ScreenId.Main);

            _startButton.onClick.AddListener(MainGame);
        }

        public override void Exit() {
            _startButton.onClick.RemoveListener(MainGame);
            _uiService.CloseAllScreens();
        }

        private void MainGame() {
            _gameStateMachine.TranslateTo<GameLoopState>();
        }
    }
}