using General.StateMachine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class DeathState: State {
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerAvatar _avatar;

        public DeathState(
            PlayerStateMachine stateMachine,
            PlayerAvatar avatar
        ) {
            _stateMachine = stateMachine;
            _avatar = avatar;
        }

        public override void Enter() {
            _avatar.Hit();
        }
    }
}