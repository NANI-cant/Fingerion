using Architecture.Services.PersistentProgress;
using PersistentProgress;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Architecture.Services.General.Impl {
    public class UnityDestroyProvider : IDestroyProvider {
        private readonly IPersistentProgressService _persistentProgressService;

        public UnityDestroyProvider(IPersistentProgressService persistentProgressService) {
            _persistentProgressService = persistentProgressService;
        }
        
        public void Destroy(GameObject gameObject) {
            Object.Destroy(gameObject);   
        }
    }
}