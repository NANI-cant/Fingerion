using System;
using Architecture.Services.Factories;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    public class Slider: MonoBehaviour {
        private float _fallSpeed;
        private float _slideDuration;
        private ISchedulerFactory _schedulerFactory;
        private Timer _slidingTimer;
        private bool _alreadySliding;
        private Rigidbody2D _rigidbody;
        
        public void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Construct(float fallSpeed, float slideDuration, ISchedulerFactory schedulerFactory) {
            _fallSpeed = fallSpeed;
            _slideDuration = slideDuration;
            _schedulerFactory = schedulerFactory;
        }
        
        public void Slide(Action onSlideEnd) {
            _slidingTimer = _schedulerFactory.CreateTimer(_slideDuration, onSlideEnd);
        }

        public void Interrupt() {
            _slidingTimer?.Stop();
        }

        public void Fall() {
            _rigidbody.velocity = Vector2.down * _fallSpeed;
        }
    }
}