using UnityEditor;
using UnityEngine;

namespace Gameplay.Environment.BackgroundGeneration {
    public class BackgroundTile: MonoBehaviour {
        [SerializeField] private SpriteRenderer[] _tiles;

        public float Length { get; private set; }

        private void Awake() {
            Length = 0;
            foreach (var spriteRenderer in _tiles) {
                Length += spriteRenderer.bounds.size.x;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("SetInRow")]
        public void SetInRow() {
            if(_tiles.Length == 0) return;

            _tiles[0].transform.position = transform.position;
            for (int i = 1; i < _tiles.Length; i++) {
                _tiles[i].transform.position =
                    _tiles[i - 1].transform.position + Vector3.right * _tiles[i - 1].bounds.size.x;
            }
            EditorUtility.SetDirty(gameObject);
        }
        
        private void OnDrawGizmos() {
            if(_tiles.Length == 0) return;
            
            Awake();
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position + Vector3.right * Length / 2, new Vector2(Length, _tiles[0].size.y));
            Gizmos.DrawWireSphere(transform.position, 0.1f);
            Gizmos.DrawWireSphere(transform.position + Vector3.right * Length, 0.1f);
            
        }
#endif
    }
}