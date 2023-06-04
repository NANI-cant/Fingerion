using System;
using Gameplay.Utils;

namespace Architecture.Services.Factories {
    public interface ISchedulerFactory {
        Timer CreateTimer(float time, Action timesUpAction);
    }
}