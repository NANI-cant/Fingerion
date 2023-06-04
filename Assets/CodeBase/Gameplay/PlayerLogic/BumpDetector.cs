using System;
using Gameplay.Utils.TriggerObservers;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class BumpDetector: MonoBehaviour {
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private CircleTriggerObserver _trigger;

        public event Action Bumped;

        private void OnEnable() => _trigger.Enter += ReactToBump;
        private void OnDisable() => _trigger.Enter -= ReactToBump;

        private void ReactToBump(Collider2D collider) => Bumped?.Invoke();
    }
}