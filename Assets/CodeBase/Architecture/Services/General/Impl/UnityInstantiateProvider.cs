using Architecture.Services.PersistentProgress;
using PersistentProgress;
using UnityEngine;

namespace Architecture.Services.General.Impl {
    public class UnityInstantiateProvider : IInstantiateProvider {
        private readonly IPersistentProgressService _persistentProgressService;

        public UnityInstantiateProvider(
            IPersistentProgressService persistentProgressService
        ) {
            _persistentProgressService = persistentProgressService;
        }

        public TObject Instantiate<TObject>(TObject template, Vector3 position, Quaternion rotation, Transform parent = null) where TObject : Object {
            var obj = Object.Instantiate(template, position, rotation, parent);
            return obj;
        }

        public TObject Instantiate<TObject>(TObject template, Transform parent = null) where TObject : Object {
            var obj = Object.Instantiate(template, parent);
            return obj;
        }
    }
}