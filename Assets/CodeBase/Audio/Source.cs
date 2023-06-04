using Architecture.Services.General;
using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class Source: MonoBehaviour {
        [SerializeField] private AudioGroup _group;
        
        private IAudioService _audioService;
        private AudioSource _audioSource;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Construct(
            IAudioService audioService
        ) {
            _audioService = audioService;
            _audioSource = GetComponent<AudioSource>();
            UpdateVolume();
            _audioService.Modified += UpdateVolume;
        }

        private void UpdateVolume() {
            switch (_group) {
                case AudioGroup.Music: {
                    _audioSource.volume = _audioService.Music;
                    break;
                }
                case AudioGroup.Sounds: {
                    _audioSource.volume = _audioService.Sounds;
                    break;
                }
                default: {
                    Debug.LogError($"No realization for {_group}");
                    break;
                }
            }
        }

        public void Play(AudioClip audioClip) {
            _audioSource.PlayOneShot(audioClip);
        }

        private void OnDestroy() {
            _audioService.Modified -= UpdateVolume;
        }
    }
}