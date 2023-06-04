using Gameplay.Bioms;
using UnityEngine;

namespace Architecture.Services.AssetProviding {
    public interface IPrefabProvider {
        GameObject Player { get; }
        int[] BiomsIds { get; }

        IBiomPack BiomPack(int id);
    }
}