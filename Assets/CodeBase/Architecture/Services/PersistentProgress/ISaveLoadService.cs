using System;
using PersistentProgress;

namespace Architecture.Services.PersistentProgress {
    public interface ISaveLoadService {
        void Save(PlayerProgress playerProgress);
        void LoadAsync(Action<PlayerProgress> callback);
    }
}