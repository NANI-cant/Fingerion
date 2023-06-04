using System;

namespace Architecture.Services.General {
    public interface IAudioService {
        event Action Modified;
        
        float Music { get; set; }
        float Sounds { get; set; }
    }
}