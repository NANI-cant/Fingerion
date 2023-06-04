using System;
using Architecture.Services.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Button))]
    public class NavigationButton: MonoBehaviour {
        [SerializeField] private ScreenId _toScreen;
        
        private Button _button;
        private IUIService _uiService;

        private void Awake() {
            _button = GetComponent<Button>();
        }

        public void Construct(IUIService uiService) {
            _uiService = uiService;
        }

        private void Start() {
            _button.onClick.AddListener(SwitchScreen);
        }

        private void OnDestroy() {
            _button.onClick.RemoveListener(SwitchScreen);
        }

        private void SwitchScreen() {
            _uiService.SwitchScreen(_toScreen);
        }
    }
}