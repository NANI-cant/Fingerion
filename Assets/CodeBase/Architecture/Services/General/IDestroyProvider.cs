using UnityEngine;

namespace Architecture.Services.General {
    public interface IDestroyProvider {
        void Destroy(GameObject gameObject);
    }
}