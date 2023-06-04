using UI;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace Architecture.Services.AssetProviding {
    public interface IUIProvider {
        GameObject HUD { get; }
        GameObject StartScreen { get; }
        GameObject LoseScreen { get; }
        Screen GetScreen(ScreenId screenId);
    }
}