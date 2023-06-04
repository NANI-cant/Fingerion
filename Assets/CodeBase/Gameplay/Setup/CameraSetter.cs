using System;
using UnityEngine;

namespace Gameplay.Setup {
    [RequireComponent(typeof(Camera))]
    public class CameraSetter: MonoBehaviour {
        [SerializeField] private Transform _anchor;
        [SerializeField] private float _xOffset;
        
        private Camera _camera;
        
        private float PixelsByUnit => _camera.pixelHeight / (_camera.orthographicSize * 2);

        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void Update() {
            transform.position = new Vector3(_anchor.position.x, transform.position.y, transform.position.z);
            transform.Translate(new Vector2(_camera.pixelWidth / 2f / PixelsByUnit, 0));
            transform.Translate(new Vector2(-_xOffset, 0));
        }

        [ContextMenu("Save Offset")]
        public void SaveOffset() {
            if(_anchor == null) return;
            if (_camera == null) _camera = GetComponent<Camera>();

            float leftSide = transform.position.x - _camera.pixelWidth / 2f / PixelsByUnit;
            _xOffset = _anchor.position.x - leftSide;
        }
    }
}