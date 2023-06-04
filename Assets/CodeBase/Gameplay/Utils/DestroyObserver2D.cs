using System;
using UnityEngine;

namespace Gameplay.Utils {
    public class DestroyObserver2D: MonoBehaviour{
        private Collider2D _collider;
        
        public event Action<Collider2D> Destroyed;

        private void Awake() => _collider = GetComponent<Collider2D>();
        private void OnDestroy() => Destroyed?.Invoke(_collider);
    }
}