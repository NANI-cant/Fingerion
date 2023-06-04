using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class GroundDetector: MonoBehaviour {
        [SerializeField] private Vector2 _feetPoint = Vector2.zero;
        [SerializeField] [Min(0.1f)] private float _feetWidth = 0.1f;
        [SerializeField] private LayerMask _groundLayer;
        
        private float _bufferDistance;

        public event Action BufferGrounded;
        public event Action Grounded;
        public event Action GroundLost;

        public bool IsBufferGrounded { get; private set; }
        public bool IsGrounded { get; private set; }

        private Vector2 FeetPoint => (Vector2)transform.position + _feetPoint;

        public void Construct(float bufferDistance) {
            _bufferDistance = bufferDistance;
        }

        private void Update() {
            Detect();
        }

        private void Detect() {
            float distance = GetDistanceToGround();

            if (distance <= _bufferDistance && !IsBufferGrounded) BufferGrounded?.Invoke();
            if (distance <= 0.01f && !IsGrounded) Grounded?.Invoke();
            if (distance >= 0.01f && IsGrounded) GroundLost?.Invoke();

            IsBufferGrounded = distance <= _bufferDistance;
            IsGrounded = distance <= 0.01f;
        }

        private float GetDistanceToGround() {
            var hitResult = Physics2D.BoxCast(FeetPoint, new Vector2(_feetWidth, 0.01f), 0, Vector2.down, float.MaxValue, _groundLayer);
            return hitResult.collider == null 
                ? float.MaxValue 
                : hitResult.distance;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(FeetPoint, new Vector3(_feetWidth, 0.1f));
        }
#endif
    }
}