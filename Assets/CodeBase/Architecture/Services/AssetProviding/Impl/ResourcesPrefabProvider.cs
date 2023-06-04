using System.Collections.Generic;
using System.Linq;
using Gameplay.Bioms;
using Gameplay.Bioms.Impl;
using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    public class ResourcesPrefabProvider : IPrefabProvider {
        private const string PlayerPath = "Gameplay/Player";
        private const string BiomsFolder = "BiomPacks/";

        private readonly Dictionary<int, BiomPack> _bioms = new ();

        public GameObject Player => Resources.Load<GameObject>(PlayerPath);
        public int[] BiomsIds => _bioms.Keys.ToArray();

        public ResourcesPrefabProvider() {
            foreach (var biomPack in Resources.LoadAll<BiomPack>(BiomsFolder)) {
                _bioms.Add(biomPack.Id, biomPack);   
            }
        }

        public IBiomPack BiomPack(int biomId) => _bioms[biomId];
    }
}