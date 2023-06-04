using Architecture.Services.AssetProviding;
using Gameplay.Bioms;

namespace Architecture.Services.Gameplay.Impl {
    public class BiomService : IBiomService {
        private readonly IPrefabProvider _prefabProvider;

        public IBiomPack Biom { get; private set; }

        public BiomService(IPrefabProvider prefabProvider) {
            _prefabProvider = prefabProvider;

            Biom = _prefabProvider.BiomPack(0);
        }

        public void Next() {
            int chosenId = Biom.Id + 1;
            chosenId %= 4;
            Biom = _prefabProvider.BiomPack(chosenId);
        }

        public void Reset() {
            Biom = _prefabProvider.BiomPack(0);
        }
    }
}