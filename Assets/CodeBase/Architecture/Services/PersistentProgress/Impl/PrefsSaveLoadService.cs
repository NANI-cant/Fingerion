using System;
using PersistentProgress;
using UnityEngine;

namespace Architecture.Services.PersistentProgress.Impl {
    public class PrefsSaveLoadService : ISaveLoadService {
        private const string PlayerProgressKey = "PlayerProgress";

        public void Save(PlayerProgress playerProgress) {
            string json = JsonUtility.ToJson(playerProgress);
            PlayerPrefs.SetString(PlayerProgressKey, json);
        }

        public void Load(out PlayerProgress playerProgress) {
            string json = PlayerPrefs.GetString(PlayerProgressKey, "");
            playerProgress = JsonUtility.FromJson<PlayerProgress>(json) ?? new PlayerProgress();
        }

        public void LoadAsync(Action<PlayerProgress> callback) {
            string json = PlayerPrefs.GetString(PlayerProgressKey, "");
            var progress = JsonUtility.FromJson<PlayerProgress>(json) ?? new PlayerProgress();
            callback?.Invoke(progress);
        }
    }
}