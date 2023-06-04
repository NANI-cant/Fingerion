using Architecture.Services.Factories;
using Architecture.Services.General;
using UI;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace Architecture.Services.UI.Impl {
    public class UIService : IUIService {
        private readonly IUIFactory _factory;
        private readonly Canvas _rootCanvas;
        private readonly IDestroyProvider _destroyProvider;

        private Screen _currentScreen;

        public UIService(
            IUIFactory factory,
            Canvas rootCanvas,
            IDestroyProvider destroyProvider
        ) {
            _factory = factory;
            _rootCanvas = rootCanvas;
            _destroyProvider = destroyProvider;
        }

        public void CloseScreen(Screen screen) {
            if (screen == null) return;
            if (screen == _currentScreen) _currentScreen = null;
            _destroyProvider.Destroy(screen.gameObject);
        }

        public Screen OpenScreen(ScreenId screenId) {
            var screen = _factory.CreateScreen(screenId, _rootCanvas, this);
            _currentScreen = screen;
            return screen;
        }

        public Screen SwitchScreen(ScreenId toScreen) {
            if (_currentScreen != null) {
                CloseScreen(_currentScreen);
                _currentScreen = null;
            }
            
            return OpenScreen(toScreen);
        }

        public void CloseAllScreens() {
            if(_currentScreen == null) return;
            
            CloseScreen(_currentScreen);
        }
    }
}