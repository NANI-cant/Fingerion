using System;
using Architecture.Services.Gameplay;
using UnityEngine;

namespace Rings {
    [RequireComponent(typeof(SpriteRenderer))]
    public class RingGameplayView: MonoBehaviour {
        private SpriteRenderer _spriteRenderer;
        private IRingService _ringService;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
                _spriteRenderer.enabled = false;
                return;
            }

            _spriteRenderer.enabled = true;
            _spriteRenderer.sprite = _ringService.ActiveRing.Ring;
        }
    }
}