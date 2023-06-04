using Architecture.Services.General;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Button))]
    public class ButtonSound: MonoBehaviour {
        [SerializeField] private AudioClip _sound;

        private Source _audioSource;
        private Button _button;
        private IAudioService _audioService;

        private void Awake() => _button = GetComponent<Button>();
        
        public void Construct(
            IAudioService audioService,
            Source source
        ) {
            _audioService = audioService;
            _audioSource = source;
        }

        private void Start() {
            _button.onClick.AddListener(PlaySound);
        }

        private void OnDestroy() {
            _button.onClick.RemoveListener(PlaySound);
        }

        private void PlaySound() => _audioSource.Play(_sound);
        
    }
}