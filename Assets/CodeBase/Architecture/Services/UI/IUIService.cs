using UI;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace Architecture.Services.UI {
    public interface IUIService {
        void CloseScreen(Screen screen);
        Screen OpenScreen(ScreenId screenId);
        Screen SwitchScreen(ScreenId toScreen);
        void CloseAllScreens();
    }
}