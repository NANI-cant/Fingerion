using System;
using UnityEngine;

namespace Architecture.Services.Gameplay {
    public interface IInputService {
        event Action<Vector2> Swipe;
        event Action Up;
        event Action Down;

        public void Enable();
        public void Disable();
    }
}