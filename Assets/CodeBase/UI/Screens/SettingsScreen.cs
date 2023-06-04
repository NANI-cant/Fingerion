using Architecture.Services.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens {
    public class SettingsScreen: MonoBehaviour {
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _sounds;
        [SerializeField] private TMP_Dropdown _language;
        
        private LocalizationService _localizationService;
        private IAudioService _audioService;

        public void Construct(
            IAudioService audioService,
            LocalizationService localizationService
        ) {
            _audioService = audioService;
            _localizationService = localizationService;
            
            _music.value = _audioService.Music;
            _sounds.value = _audioService.Sounds;
            _language.value = _localizationService.SelectedLanguage;
        }

        private void Start() {
            _music.onValueChanged.AddListener(UpdateMusic);
            _sounds.onValueChanged.AddListener(UpdateSounds);
            _language.onValueChanged.AddListener(UpdateLanguage);
        }

        private void OnDestroy() {
            _music.onValueChanged.RemoveListener(UpdateMusic);
            _sounds.onValueChanged.RemoveListener(UpdateSounds);
            _language.onValueChanged.RemoveListener(UpdateLanguage);
        }

        private void UpdateLanguage(int language) => _localizationService.ChangeLanguage(language);
        private void UpdateSounds(float value) => _audioService.Sounds = value;
        private void UpdateMusic(float value) => _audioService.Music = value;
    }
}