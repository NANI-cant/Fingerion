using UnityEngine;

namespace Gameplay.Utils {
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class StaticFOV : MonoBehaviour {
        [SerializeField] [Range(0,360)] private float _fieldOfView;
        
        private Camera _camera;

        private void Awake() => _camera = GetComponent<Camera>();

        private void Update() {
            float fov = Camera.HorizontalToVerticalFieldOfView(_fieldOfView, _camera.aspect);
            _camera.fieldOfView = fov;
        }
    }
}