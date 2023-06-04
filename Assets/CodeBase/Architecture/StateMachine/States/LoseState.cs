using Achievements.Detecting;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.General;
using Architecture.Services.UI;
using Architecture.Services.UI.Impl;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;
using UI;
using UI.Screens;

namespace Architecture.StateMachine.States {
    public class LoseState: State {
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerPointer _playerPointer;
        private readonly IInputService _inputService;
        private readonly IUIService _uiService;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly LoseDetector _loseDetector;
        private readonly IScoreService _scoreService;

        private LoseScreen _loseScreen;

        public LoseState(GameStateMachine stateMachine,
            IPlayerPointer playerPointer,
            IInputService inputService,
            IUIService uiService,
            ISceneLoadService sceneLoadService, 
            LoseDetector loseDetector,
            IScoreService scoreService
        ) {
            _stateMachine = stateMachine;
            _playerPointer = playerPointer;
            _inputService = inputService;
            _uiService = uiService;
            _sceneLoadService = sceneLoadService;
            _loseDetector = loseDetector;
            _scoreService = scoreService;
        }

        public override void Enter() {
            _inputService.Disable();
            _loseDetector.Detect();
            _playerPointer.Player.StateMachine.TranslateTo<DeathState>();
            _loseScreen = _uiService.SwitchScreen(ScreenId.Lose).GetComponent<LoseScreen>();
            _scoreService.Stop();

            _loseScreen.GameRestarted += RestartGame;
        }

        public override void Exit() {
            _loseScreen.GameRestarted -= RestartGame;
            _uiService.CloseScreen(_loseScreen.GetComponent<Screen>());
        }

        private void RestartGame() => _sceneLoadService.Reload();
    }
}