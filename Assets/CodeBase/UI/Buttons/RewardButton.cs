using Ads;
using Architecture.Services.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Button))]
    public class RewardButton: MonoBehaviour {
        [SerializeField] private Dialog _dialogTemplate;
        
        private Button _button;
        private IAdService _adService;
        private IUIFactory _uiFactory;
        private Canvas _uiRoot;
        private Dialog _openedDialog;

        private void Awake() => _button = GetComponent<Button>();

        public void Construct(
            IAdService adService,
            IUIFactory uiFactory,
            Canvas uiRoot
        ) {
            _adService = adService;
            _uiFactory = uiFactory;
            _uiRoot = uiRoot;
        }

        private void Start() => _button.onClick.AddListener(TryReward);
        private void OnDestroy() => _button.onClick.RemoveListener(TryReward);

        private void TryReward() {
            _openedDialog = _uiFactory.CreateDialog(_dialogTemplate, _uiRoot);
            _openedDialog.OnYes += Show;
        }

        private void Show() {
            _openedDialog.OnYes -= Show;
            _adService.ShowRewarded();
        }
    }
}