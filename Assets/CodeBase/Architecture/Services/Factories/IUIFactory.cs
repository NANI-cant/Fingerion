using Achievements;
using Architecture.Services.UI;
using Shop;
using UI;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace Architecture.Services.Factories {
    public interface IUIFactory {
        Screen CreateScreen(ScreenId screenId, Canvas rootCanvas, IUIService uiService);
        ItemView CreateShopItem(ItemView template, Transform container, IItemData data);
        AchievementView CreateAchievement(AchievementView template, Transform container, IAchievement data);
        Dialog CreateDialog(Dialog dialogTemplate, Canvas uiRoot);
    }
}