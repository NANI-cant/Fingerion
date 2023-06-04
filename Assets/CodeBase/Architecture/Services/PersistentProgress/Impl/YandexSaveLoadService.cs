using System;
using System.Runtime.InteropServices;
using PersistentProgress;
using UnityEngine;

namespace Architecture.Services.PersistentProgress.Impl {
    public class YandexSaveLoadService : ISaveLoadService {
        private Action<PlayerProgress> _asyncLoadAction;

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void SaveExtern(string progress);
        [DllImport("__Internal")]
        private static extern void LoadExtern();
#endif

        public void Save(PlayerProgress playerProgress) {
            var json = JsonUtility.ToJson(playerProgress ?? new PlayerProgress());
#if UNITY_WEBGL
            SaveExtern(json);
#endif
        }

        public void LoadAsync(Action<PlayerProgress> callback) {
#if UNITY_WEBGL
            LoadExtern();
#endif
            _asyncLoadAction = callback;
        }

        public void SetProgress(string progress) {
            var playerProgress = JsonUtility.FromJson<PlayerProgress>(progress) ?? new PlayerProgress();
            _asyncLoadAction.Invoke(playerProgress);
        }
    }
}