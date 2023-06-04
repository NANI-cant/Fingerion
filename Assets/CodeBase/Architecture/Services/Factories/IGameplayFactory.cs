using Gameplay.Environment.BackgroundGeneration;
using Gameplay.Environment.ChunkGeneration;
using Gameplay.Environment.ChunkGeneration.Impl;
using UnityEngine;

namespace Architecture.Services.Factories {
    public interface IGameplayFactory {
        GameObject CreatePlayer(Vector3 position, Quaternion rotation);
        Chunk CreateChunk(Chunk lastChunk);
        BackgroundTile CreateTile(Background background);
    }
}