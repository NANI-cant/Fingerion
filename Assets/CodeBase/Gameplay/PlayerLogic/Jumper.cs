using System;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class Jumper: MonoBehaviour {
        private float _jumpHeight;
        private float _jumpDuration;
        private Rigidbody2D _rigidbody;
        
        public void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Construct(float jumpHeight, float jumpDuration) {
            _jumpHeight = jumpHeight;
            _jumpDuration = jumpDuration;
        }

        public void Jump() {
            float speed = 2 * _jumpHeight / ( _jumpDuration / 2 );
            _rigidbody.velocity = Vector2.up * speed;
        }
    }
}