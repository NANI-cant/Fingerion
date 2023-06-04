using Gameplay.PlayerLogic;

namespace Architecture.Services.Gameplay {
    public interface IPlayerPointer {
        public Player Player { get; set; }
    }
}