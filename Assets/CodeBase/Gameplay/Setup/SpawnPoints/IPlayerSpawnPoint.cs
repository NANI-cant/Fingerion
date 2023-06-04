using UnityEngine;

namespace Gameplay.Setup.SpawnPoints {
    public interface IPlayerSpawnPoint {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}