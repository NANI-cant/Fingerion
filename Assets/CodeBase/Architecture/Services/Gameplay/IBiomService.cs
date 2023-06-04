using Gameplay.Bioms;

namespace Architecture.Services.Gameplay {
    public interface IBiomService {
        IBiomPack Biom { get; }
        
        void Next();
        void Reset();
    }
}