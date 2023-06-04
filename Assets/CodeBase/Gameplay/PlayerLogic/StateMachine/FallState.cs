using Architecture.Services.Gameplay;
using General.StateMachine;
using UnityEngine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class FallState : State {
        private readonly PlayerStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly GroundDetector _groundDetector;
        private readonly PlayerAvatar _avatar;
        private readonly Jumper _jumper;

        public FallState(
            PlayerStateMachine stateMachine,
            IInputService inputService,
            GroundDetector groundDetector,
            PlayerAvatar avatar,
            Jumper jumper
        ) {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _groundDetector = groundDetector;
            _avatar = avatar;
            _jumper = jumper;
        }
        
        public override void Enter() {
            Debug.Log("FALL");

            _inputService.Up += Jump;
            _inputService.Down += Slide;
            _groundDetector.Grounded += Run;
        }

        public override void Exit() {
            _avatar.Land();
            
            _inputService.Up -= Jump;
            _inputService.Down -= Slide;
            _groundDetector.Grounded -= Run;
        }

        private void Jump() {
            if(_groundDetector.IsBufferGrounded) _jumper.Jump();    
        }
        
        private void Slide() => _stateMachine.TranslateTo<SlideState>();
        private void Run() => _stateMachine.TranslateTo<RunState>();
    }
}