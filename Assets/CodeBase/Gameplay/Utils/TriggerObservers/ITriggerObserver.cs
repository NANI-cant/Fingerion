using System;
using UnityEngine;

namespace Gameplay.Utils.TriggerObservers {
    public interface ITriggerObserver {
        event Action<Collider> Enter;
        event Action<Collider> Exit;
    }
}