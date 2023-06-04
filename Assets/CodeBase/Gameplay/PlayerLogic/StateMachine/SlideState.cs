using Architecture.Services.Gameplay;
using General.StateMachine;
using UnityEngine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class SlideState : State {
        private readonly PlayerStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly Slider _slider;
        private readonly GroundDetector _groundDetector;
        private readonly  Jumper _jumper;
        private readonly PlayerAvatar _avatar;

        public SlideState(
            PlayerStateMachine stateMachine,
            IInputService inputService,
            Slider slider,
            GroundDetector groundDetector,
            Jumper jumper,
            PlayerAvatar avatar
        ) {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _slider = slider;
            _groundDetector = groundDetector;
            _jumper = jumper;
            _avatar = avatar;
        }

        public override void Enter() {
            Debug.Log("SLIDE");
            _avatar.Slide();

            if (_groundDetector.IsGrounded) {
                DoSlide();
            }
            else {
                _slider.Fall();
            }

            //_groundDetector.GroundLost += Fall;
            _groundDetector.Grounded += DoSlide;
            _inputService.Up += Jump;
        }

        public override void Exit() {
            _avatar.Stand();
            _slider.Interrupt();

            //_groundDetector.GroundLost -= Fall;
            _groundDetector.Grounded -= DoSlide;
            _inputService.Up -= Jump;
        }

        private void Jump() {
            if (!_groundDetector.IsBufferGrounded) return;
            
            _avatar.Jump();
            _jumper.Jump();
            _stateMachine.TranslateTo<FallState>();
        }

        //private void Fall() => _stateMachine.TranslateTo<FallState>();
        private void DoSlide() => _slider.Slide(_stateMachine.TranslateTo<RunState>);
    }
}