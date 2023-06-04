using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class GroundPlacer: MonoBehaviour {
        [SerializeField] private LayerMask _ground;
        [SerializeField] private float _accuracy;

        private void Update() {
            var hitResult = Physics2D.Raycast(transform.position, Vector2.down, _accuracy, _ground);
            if(hitResult.collider == null) return;

            transform.position = hitResult.point;
        }
    }
}