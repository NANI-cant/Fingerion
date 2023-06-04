using Ads;
using Ads.Impl;
using Architecture.Services.General;
using Architecture.Services.PersistentProgress;
using Architecture.Services.PersistentProgress.Impl;
using UnityEngine;
using Zenject;

namespace Yandex{
    public class Yandex : MonoBehaviour{
        public static Yandex Instance { get; private set; }
        
        private IAudioService _audioService;
        private YandexSaveLoadService _saveLoadService;
        private YandexAdService _adService;
        private float _savedMusicVolume;
        private float _savedSoundsVolume;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else {
                Destroy(gameObject);
                return;
            }
        }

        [Inject]
        public void Construct(
            IAudioService audioService,
            ISaveLoadService saveLoadService,
            IAdService adService
        ) {
            _audioService = audioService;
            if (saveLoadService is YandexSaveLoadService yandexSaveLoad) _saveLoadService = yandexSaveLoad;
            if (adService is YandexAdService yandexAd) _adService = yandexAd;
        }

        public void AdOpened() {
            _savedMusicVolume = _audioService.Music;
            _savedSoundsVolume = _audioService.Sounds;
            _audioService.Music = 0;
            _audioService.Sounds = 0;
        }

        public void AdClosed() {
            _audioService.Music = _savedMusicVolume;
            _audioService.Sounds = _savedSoundsVolume;
        }

        public void GiveReward() {
            _adService.GiveReward();
        }

        public void SetProgress(string progress) {
            _saveLoadService.SetProgress(progress);
        }
    }
}
