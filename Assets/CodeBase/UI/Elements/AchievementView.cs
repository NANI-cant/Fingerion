using Achievements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class AchievementView: MonoBehaviour {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _reward;
        [SerializeField] private Image _background;
        
        public IAchievement Achievement { get; private set; }

        public void Construct(IAchievement achievement) {
            Achievement = achievement;
            _label.text = Achievement.Label;
            _reward.text = Achievement.Reward.ShorterString();
        }

        public void SetAvailable() {
            _background.color = Color.white;   
        }

        public void SetCollected() {
            _background.color = Color.grey;
        }
    }
}