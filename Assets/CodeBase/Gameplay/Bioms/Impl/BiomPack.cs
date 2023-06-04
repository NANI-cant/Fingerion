using Gameplay.Environment.BackgroundGeneration;
using Gameplay.Environment.ChunkGeneration.Impl;
using UnityEngine;

namespace Gameplay.Bioms.Impl {
    [CreateAssetMenu(fileName = "BiomPack", menuName = "BiomPack")]
    public class BiomPack : ScriptableObject, IBiomPack {
        [SerializeField] private int _id;
        [SerializeField] private Chunk[] _chunks;
        [SerializeField] private BackgroundTile _tile;

        public int Id => _id;
        public Chunk[] Chunks => _chunks;
        public BackgroundTile Tile => _tile;
    }
}