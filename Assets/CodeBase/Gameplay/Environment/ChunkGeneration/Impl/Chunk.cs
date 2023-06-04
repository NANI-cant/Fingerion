using System;
using UnityEngine;

namespace Gameplay.Environment.ChunkGeneration.Impl {
    public class Chunk: MonoBehaviour {
        [SerializeField] private Transform _leftCorner;
        [SerializeField] private Transform _rightCorner;
        [SerializeField] private int[] _enterPoints;
        [SerializeField] private int[] _exitPoints;
        
        public float Length => Vector2.Distance(RightCorner, LeftCorner);

        private Vector2 RightCorner => _rightCorner.position;
        private Vector2 LeftCorner => _leftCorner.position;
        private int[] EnterPoints => _enterPoints;
        private int[] ExitPoints => _exitPoints;
        
        public void ConcatTo(Chunk chunk) {
            Vector2 offset = chunk.RightCorner - LeftCorner;
            transform.Translate(offset, Space.World);
        }

        public bool AbleToConcatWith(Chunk chunk) {
            if (chunk.EnterPoints.Length != ExitPoints.Length) return false;
            
            Array.Sort(ExitPoints);
            Array.Sort(chunk.EnterPoints);

            for (int i = 0; i < ExitPoints.Length; i++) {
                if (ExitPoints[i] != chunk.EnterPoints[i]) return false;
            }

            return true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.cyan;
            foreach (var point in EnterPoints) {
                Gizmos.DrawWireSphere(LeftCorner + Vector2.up * point, 0.1f);
            }
            
            Gizmos.color = Color.magenta;
            foreach (var point in ExitPoints) {
                Gizmos.DrawWireSphere(RightCorner + Vector2.up * point, 0.1f);
            }
        }

        private void OnValidate() {
            Array.Sort(_enterPoints);
            Array.Sort(_exitPoints);
        }
#endif
    }
}