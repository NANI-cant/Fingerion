using System;
using PersistentProgress;

namespace Architecture.Services.PersistentProgress {
    public interface IPersistentProgressService {
        PlayerProgress Progress { get; }

        event Action Loaded;
        
        void Save();
        void Load();
    }
}