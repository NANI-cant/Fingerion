using Gameplay.Environment.BackgroundGeneration;
using Gameplay.Environment.ChunkGeneration.Impl;

namespace Gameplay.Bioms {
    public interface IBiomPack {
        public int Id { get; }
        public Chunk[] Chunks { get; }
        public BackgroundTile Tile { get; }
    }
}