using Achievements.Detecting;
using Architecture.Services.Gameplay;
using Architecture.Services.Gameplay.Impl;
using Architecture.Services.UI;
using Gameplay.PlayerLogic;
using Gameplay.PlayerLogic.StateMachine;
using General.StateMachine;
using UI;

namespace Architecture.StateMachine.States {
    public class GameLoopState : State {
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerPointer _playerPointer;

        private readonly IBiomGenerationService _biomGenerationService;

        //private readonly ChunkGenerator _chunkGenerator;
        private readonly ChunkTranslation _chunkTranslation;
        private readonly IInputService _inputService;
        private readonly BackgroundParallaxService _backgroundParallax;
        private readonly RunDetector _runDetector;
        private readonly IScoreService _scoreService;
        private readonly IUIService _uiService;
        private readonly IBiomService _biomService;

        public GameLoopState(
            GameStateMachine stateMachine,
            IPlayerPointer playerPointer,
            IBiomGenerationService biomGenerationService,
            ChunkTranslation chunkTranslation,
            IInputService inputService,
            BackgroundParallaxService backgroundParallax,
            RunDetector runDetector, 
            IScoreService scoreService,
            IUIService uiService,
            IBiomService biomService
        ) {
            _stateMachine = stateMachine;
            _playerPointer = playerPointer;
            _biomGenerationService = biomGenerationService;
            _chunkTranslation = chunkTranslation;
            _inputService = inputService;
            _backgroundParallax = backgroundParallax;
            _runDetector = runDetector;
            _scoreService = scoreService;
            _uiService = uiService;
            _biomService = biomService;
        }

        public override void Enter() {
            _uiService.SwitchScreen(ScreenId.HUD);
            _inputService.Enable();
            _chunkTranslation.Start();
            _biomGenerationService.Start();
            _backgroundParallax.Start();
            _runDetector.Start();
            _scoreService.Start();
            _playerPointer.Player.StateMachine.TranslateTo<RunState>();

            _playerPointer.Player.GetComponent<BumpDetector>().Bumped += Lose;
        }

        public override void Exit() {
            _chunkTranslation.Stop();
            _backgroundParallax.Stop();
            _runDetector.Stop();
            _biomGenerationService.Stop();
            _biomService.Reset();

            _playerPointer.Player.GetComponent<BumpDetector>().Bumped -= Lose;
        }

        private void Lose() {
            _stateMachine.TranslateTo<LoseState>();
        }
    }
}