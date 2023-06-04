using System;
using UnityEngine;

namespace Gameplay.Utils {
    public class DestroyObserver3D: MonoBehaviour{
        private Collider _collider;
        
        public event Action<Collider> Destroyed;

        private void Awake() => _collider = GetComponent<Collider>();
        private void OnDestroy() => Destroyed?.Invoke(_collider);
    }
}