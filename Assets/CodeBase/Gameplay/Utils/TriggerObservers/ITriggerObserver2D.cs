using System;
using UnityEngine;

namespace Gameplay.Utils.TriggerObservers {
    public interface ITriggerObserver2D {
        event Action<Collider2D> Enter;
        event Action<Collider2D> Exit;
    }
}