using System;
using Architecture.Services.Gameplay;
using TMPro;
using UnityEngine;

namespace UI {
    public class CurrentScoreView: MonoBehaviour {
        [SerializeField] private TMP_Text _value;
        
        private IScoreService _scoreService;

        public void Construct(IScoreService scoreService) {
            _scoreService = scoreService;
            UpdateUI();
            
            _scoreService.Modified += UpdateUI;
        }

        private void OnDestroy() {
            _scoreService.Modified -= UpdateUI;
        }

        private void UpdateUI() {
            _value.text = _scoreService.CurrentScore.ShorterString();
        }
    }
}