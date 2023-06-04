using System;
using Architecture.Services.PersistentProgress;
using Audio;

namespace Architecture.Services.General.Impl {
    public class AudioService : IAudioService {
        private readonly IPersistentProgressService _persistentProgressService;
        
        private float _music = 1;
        private float _sounds = 1;

        public event Action Modified;

        public float Music {
            get => _music;
            set {
                _music = Math.Clamp(value, 0, 1);
                _persistentProgressService.Progress.MusicVolume = _music;
                _persistentProgressService.Save();
                Modified?.Invoke();
            }
        }

        public float Sounds {
            get => _sounds;
            set {
                _sounds = Math.Clamp(value, 0, 1);
                _persistentProgressService.Progress.SoundsVolume = _sounds;
                _persistentProgressService.Save();
                Modified?.Invoke();
            }
        }

        public AudioService(
            IPersistentProgressService persistentProgressService,
            Source[] persistentSources
        ) {
            _persistentProgressService = persistentProgressService;
            foreach (var source in persistentSources) {
                source.Construct(this);    
            }

            if (_persistentProgressService.Progress == null) {
                _persistentProgressService.Loaded += InitializeDelayed;
            }
            else {
                Music = _persistentProgressService.Progress.MusicVolume;
                Sounds = _persistentProgressService.Progress.SoundsVolume;   
            }
        }

        private void InitializeDelayed() {
            Music = _persistentProgressService.Progress.MusicVolume;
            Sounds = _persistentProgressService.Progress.SoundsVolume;
            _persistentProgressService.Loaded -= InitializeDelayed;
        }
    }
}