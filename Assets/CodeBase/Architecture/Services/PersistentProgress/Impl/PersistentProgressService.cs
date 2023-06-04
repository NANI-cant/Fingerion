using System;
using PersistentProgress;

namespace Architecture.Services.PersistentProgress.Impl {
    public class PersistentProgressService: IPersistentProgressService {
        private readonly ISaveLoadService _saveLoadService;

        public PlayerProgress Progress { get; private set; }
        
        public event Action Loaded;

        public PersistentProgressService(
            ISaveLoadService saveLoadService
        ) {
            _saveLoadService = saveLoadService;
            Progress = null;
            Load();
        }

        public void Save() {
            _saveLoadService.Save(Progress);
        }

        public void Load() {
            _saveLoadService.LoadAsync((loadedProgress) => {
                Progress = loadedProgress;
                Loaded?.Invoke();
            });
        }
    }
}