using System;
using UnityEngine;
using Zenject;

namespace Architecture.Services.Gameplay.Impl {
    public class InputService : IInputService, ITickable {
        private const float SwipeTargetDelta = 50f;
        
        public event Action<Vector2> Swipe;
        public event Action Up;
        public event Action Down;

        private Vector2 _savedMousePosition;
        private bool _enabled;
        private bool _swipeAlreadyDetected;

        public void Tick() {
            if(Input.GetKeyDown(KeyCode.UpArrow)) Up?.Invoke();
            if(Input.GetKeyDown(KeyCode.DownArrow)) Down?.Invoke();
            
            if (Input.GetMouseButtonDown(0)) HandlePointerDown();
            if (Input.GetMouseButton(0)) HandlePointerHold();
            if (Input.GetMouseButtonUp(0)) HandlePointerUp();
        }
        
        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;

        private void HandlePointerHold() {
            if(!_enabled) return;
            HandlePointerUp();
        }

        private void HandlePointerDown() {
            if(!_enabled) return;

            _swipeAlreadyDetected = false;
            _savedMousePosition = Input.mousePosition;
        }

        private void HandlePointerUp() {
            if(!_enabled) return;
            if(_swipeAlreadyDetected) return;
            
            if (Vector2.Distance(_savedMousePosition, Input.mousePosition) < SwipeTargetDelta) return;

            Vector2 direction = ((Vector2) Input.mousePosition - _savedMousePosition).normalized;
            Swipe?.Invoke(direction);

            if (direction.y > 0.5f) Up?.Invoke();
            if (direction.y < -0.5f) Down?.Invoke();
            _swipeAlreadyDetected = true;
        }
    }
}