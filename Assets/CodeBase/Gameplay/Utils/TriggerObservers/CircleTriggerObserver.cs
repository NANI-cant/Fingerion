using System;
using UnityEngine;

namespace Gameplay.Utils.TriggerObservers {
    [RequireComponent(typeof(CircleCollider2D))]
    public class CircleTriggerObserver: MonoBehaviour, ITriggerObserver2D {
        [SerializeField] private CircleCollider2D _collider;

        public event Action<Collider2D> Enter;
        public event Action<Collider2D> Exit;

        public float Radius {
            get => _collider.radius;
            set => _collider.radius = value;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<DestroyObserver2D>(out var destroyObserver)) {
                destroyObserver.Destroyed += OnTriggerExit2D;
            }
            Enter?.Invoke(other);   
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.TryGetComponent<DestroyObserver2D>(out var destroyObserver)) {
                destroyObserver.Destroyed -= OnTriggerExit2D;
            }
            Exit?.Invoke(other);   
        }

        public void Activate() => _collider.enabled = true;
        public void Deactivate() => _collider.enabled = false;

#if UNITY_EDITOR
        private void OnValidate() {
            if(_collider != null) return;
            _collider = GetComponent<CircleCollider2D>();
        }
#endif
    }
}