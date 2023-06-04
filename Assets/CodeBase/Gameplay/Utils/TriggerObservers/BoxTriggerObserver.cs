using System;
using UnityEngine;

namespace Gameplay.Utils.TriggerObservers {
    [RequireComponent(typeof(BoxCollider))]
    public class BoxTriggerObserver: MonoBehaviour, ITriggerObserver {
        [SerializeField] private BoxCollider _collider;

        public event Action<Collider> Enter;
        public event Action<Collider> Exit;

        public Vector3 Size {
            get => _collider.size;
            set => _collider.size = value;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<DestroyObserver3D>(out var destroyObserver)) {
                destroyObserver.Destroyed += OnTriggerExit;
            }
            Enter?.Invoke(other);   
        }

        private void OnTriggerExit(Collider other) {
            if (other.TryGetComponent<DestroyObserver3D>(out var destroyObserver)) {
                destroyObserver.Destroyed -= OnTriggerExit;
            }
            Exit?.Invoke(other);   
        }

        public void Activate() => _collider.enabled = true;
        public void Deactivate() => _collider.enabled = false;

#if UNITY_EDITOR
        private void OnValidate() {
            if(_collider != null) return;
            _collider = GetComponent<BoxCollider>();
        }
#endif
    }
}