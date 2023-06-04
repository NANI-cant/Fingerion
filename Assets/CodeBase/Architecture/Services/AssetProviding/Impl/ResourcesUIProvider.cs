using System.Collections.Generic;
using UI;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace Architecture.Services.AssetProviding.Impl {
    class ResourcesUIProvider : IUIProvider {
        private const string HUDPath = "UI/HUD";
        private const string StartScreenPath = "UI/StartScreen";
        private const string LoseScreenPath = "UI/LoseScreen";
        private const string UIFolderPath = "UI/";

        private readonly Dictionary<ScreenId, Screen> _screens;

        public GameObject HUD => Resources.Load<GameObject>(HUDPath);
        public GameObject StartScreen => Resources.Load<GameObject>(StartScreenPath);
        public GameObject LoseScreen => Resources.Load<GameObject>(LoseScreenPath);

        public ResourcesUIProvider() {
            _screens = new Dictionary<ScreenId, Screen>();
            CollectScreens();
        }

        private void CollectScreens() {
            foreach (var screen in Resources.LoadAll<Screen>(UIFolderPath)) {
                _screens.Add(screen.Id, screen);
            }
        }

        public Screen GetScreen(ScreenId screenId) {
            return _screens[screenId];
        }
    }
}