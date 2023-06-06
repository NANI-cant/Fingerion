using Achievements;
using Ads;
using Architecture.Services.AssetProviding;
using Architecture.Services.Gameplay;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Architecture.Services.UI;
using Audio;
using Rings;
using Shop;
using UI;
using UI.Screens;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace Architecture.Services.Factories.Impl {
    public class UIFactory : IUIFactory {
        private readonly IUIProvider _uiProvider;
        private readonly IInstantiateProvider _instantiateProvider;
        private readonly IMetricProvider _metricProvider;
        private readonly IBankService _bankService;
        private readonly IAchievementService _achievementService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly LocalizationService _localizationService;
        private readonly IScoreService _scoreService;
        private readonly IAudioService _audioService;
        private readonly IRingService _ringService;
        private readonly Source _soundsSource;
        private readonly IAdService _adService;
        private readonly AudioSource _audioSource;

        public UIFactory(
            IUIProvider uiProvider,
            IInstantiateProvider instantiateProvider,
            IMetricProvider metricProvider,
            IBankService bankService,
            IAchievementService achievementService,
            IPersistentProgressService persistentProgressService,
            LocalizationService localizationService,
            IScoreService scoreService,
            IAudioService audioService,
            IRingService ringService, 
            Source soundsSource,
            IAdService adService
        ) {
            _uiProvider = uiProvider;
            _instantiateProvider = instantiateProvider;
            _metricProvider = metricProvider;
            _bankService = bankService;
            _achievementService = achievementService;
            _persistentProgressService = persistentProgressService;
            _localizationService = localizationService;
            _scoreService = scoreService;
            _audioService = audioService;
            _ringService = ringService;
            _soundsSource = soundsSource;
            _adService = adService;
        }

        public Screen CreateScreen(ScreenId screenId, Canvas rootCanvas, IUIService uiService) {
            var screen = _instantiateProvider.Instantiate(_uiProvider.GetScreen(screenId), rootCanvas.transform);
            
            ConstructBase(screen, uiService, rootCanvas);
            ConstructShopScreen(screen);
            ConstructAchievementsScreen(screen);
            ConstructSettingsScreen(screen);
            ConstructLoseScreen(screen);

            return screen;
        }

        public ItemView CreateShopItem(ItemView template, Transform container, IItemData data) {
            var item = _instantiateProvider.Instantiate(template, container);
            item.Construct(data);
            item.DoForComponentsInChildren<LocalizedText>(lt => lt.Construct(_localizationService));

            return item;
        }

        public AchievementView CreateAchievement(AchievementView template, Transform container, IAchievement data) {
            var achievementScreen = _instantiateProvider.Instantiate(template, container);
            achievementScreen.Construct(data);
            achievementScreen.DoForComponentsInChildren<LocalizedText>(lt => {
                lt.Construct(_localizationService);
                lt.SetKey(data.Label);
            });

            return achievementScreen;
        }

        public Dialog CreateDialog(Dialog dialogTemplate, Canvas uiRoot) {
            var dialog = _instantiateProvider.Instantiate(dialogTemplate, uiRoot.transform);
            dialog.DoForComponentsInChildren<LocalizedText>(lt => lt.Construct(_localizationService));
            return dialog;
        }

        private void ConstructAchievementsScreen(Screen screen) {
            if (!screen.TryGetComponent<AchievementsScreen>(out var achievementsScreen)) return;
            achievementsScreen.Construct(_achievementService, _metricProvider.Achievements, this);
        }

        private void ConstructShopScreen(Screen screen) {
            if (!screen.TryGetComponent<ShopScreen>(out var shopScreen)) return;
            shopScreen.Construct(_metricProvider.Items, _bankService, _persistentProgressService, this, _ringService);
        }

        private void ConstructSettingsScreen(Screen screen) {
            if (!screen.TryGetComponent<SettingsScreen>(out var settingsScreen)) return;
            settingsScreen.Construct(_audioService, _localizationService);
        }

        private void ConstructLoseScreen(Screen screen) {
            if (!screen.TryGetComponent<LoseScreen>(out var loseScreen)) return;
            loseScreen.Construct(_scoreService);
        }

        private void ConstructBase(Screen screen, IUIService uiService, Canvas rootCanvas) {
            screen
                .DoForComponentsInChildren<NavigationButton>(nb => nb.Construct(uiService))
                .DoForComponentsInChildren<BankView>(bv => bv.Construct(_bankService))
                .DoForComponentsInChildren<LocalizedText>(lt => lt.Construct(_localizationService))
                .DoForComponentsInChildren<CurrentScoreView>(cs => cs.Construct(_scoreService))
                .DoForComponentsInChildren<BestScoreView>(bs => bs.Construct(_scoreService))
                .DoForComponentsInChildren<RingUIView>(rv => rv.Construct(_ringService))
                .DoForComponentsInChildren<ButtonSound>(ns => ns.Construct(_audioService, _soundsSource))
                .DoForComponentsInChildren<RewardButton>(rb => rb.Construct(_adService, this, rootCanvas));
        }
    }
}