using System;
using Architecture.Services.Gameplay;
using Architecture.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens {
    public class LoseScreen: MonoBehaviour {
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameObject _newRecord;

        public event Action GameRestarted;

        public void Construct(IScoreService scoreService) {
            _newRecord.SetActive(scoreService.BestScore < scoreService.CurrentScore);
        }

        private void OnEnable() => _restartButton.onClick.AddListener(RestartGame);
        private void OnDisable() => _restartButton.onClick.RemoveListener(RestartGame);

        private void RestartGame() {
            GameRestarted?.Invoke();
        }
    }
}