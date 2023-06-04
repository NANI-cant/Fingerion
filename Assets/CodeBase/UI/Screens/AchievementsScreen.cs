using System.Collections.Generic;
using Achievements;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using UnityEngine;

namespace UI.Screens {
    public class AchievementsScreen: MonoBehaviour {
        [SerializeField] private Transform _achievementsContainer;
        [SerializeField] private AchievementView _achievementTemplate;

        private readonly List<AchievementView> _achievements = new();

        public void Construct(
            IAchievementService achievementService, 
            IAchievement[] achievements,
            IUIFactory uiFactory
        ) {
            foreach (var achievementData in achievements) {
                var achievementView = uiFactory.CreateAchievement(_achievementTemplate, _achievementsContainer, achievementData);
                
                if (achievementService.Progress.Collected.Contains(achievementView.Achievement.Id)) {
                    achievementView.SetCollected();
                }
                else {
                    achievementView.SetAvailable();
                }
                
                _achievements.Add(achievementView);
            }
        }
    }
}