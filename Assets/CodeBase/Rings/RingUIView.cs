using Architecture.Services.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Rings {
    [RequireComponent(typeof(Image))]
    public class RingUIView : MonoBehaviour {
        private Image _image;
        private IRingService _ringService;

        private void Awake() {
            _image = GetComponent<Image>();
        }

        public void Construct(IRingService ringService) {
            _ringService = ringService;
            UpdateRing();
        }

        private void Start() {
            _ringService.Modified += UpdateRing;
        }

        private void OnDestroy() {
            _ringService.Modified -= UpdateRing;
        }

        private void UpdateRing() {
            if (_ringService.ActiveRing == null) {
                _image.enabled = false;
                return;
            }

            _image.enabled = true;
            _image.sprite = _ringService.ActiveRing.Icon;
        }
    }
}