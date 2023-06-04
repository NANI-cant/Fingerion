using General.StateMachine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class IdleState : State {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerAvatar _avatar;

        public IdleState(
            PlayerStateMachine playerStateMachine,
            PlayerAvatar avatar
        ) {
            _playerStateMachine = playerStateMachine;
            _avatar = avatar;
        }

        public override void Enter() {
            _avatar.Idle();
        }
    }
}