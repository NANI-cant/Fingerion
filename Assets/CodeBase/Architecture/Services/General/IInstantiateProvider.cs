using UnityEngine;

namespace Architecture.Services.General {
    public interface IInstantiateProvider {
        TObject Instantiate<TObject>(TObject template, Vector3 position, Quaternion rotation, Transform parent = null) where TObject : Object;
        TObject Instantiate<TObject>(TObject template, Transform parent = null) where TObject : Object;
    }
}