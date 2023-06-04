using Architecture.Services.Gameplay;
using General.StateMachine;
using UnityEngine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class RunState : State {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly IInputService _inputService;
        private readonly Jumper _jumper;
        private readonly GroundDetector _groundDetector;
        private readonly PlayerAvatar _avatar;
        private readonly GroundPlacer _placer;

        public RunState(
            PlayerStateMachine playerStateMachine,
            IInputService inputService,
            Jumper jumper,
            GroundDetector groundDetector,
            PlayerAvatar avatar,
            GroundPlacer placer
        ) {
            _playerStateMachine = playerStateMachine;
            _inputService = inputService;
            _jumper = jumper;
            _groundDetector = groundDetector;
            _avatar = avatar;
            _placer = placer;
        }
        
        public override void Enter() {
            Debug.Log("RUN");

            _placer.enabled = true;
            _avatar.Run();
            
            _groundDetector.GroundLost += Fall;
            _inputService.Up += Jump;
            _inputService.Down += Slide;
        }

        public override void Exit() {
            _placer.enabled = false;
            
            _groundDetector.GroundLost -= Fall;
            _inputService.Up -= Jump;
            _inputService.Down -= Slide;
        }

        private void Jump() {
            _avatar.Jump();
            Fall();
            _jumper.Jump();
        }

        private void Fall() => _playerStateMachine.TranslateTo<FallState>();
        private void Slide() => _playerStateMachine.TranslateTo<SlideState>();
    }
}