using System;
using System.Collections.Generic;
using Architecture.Services.General;
using Gameplay.Utils;
using Zenject;

namespace Architecture.Services.Factories.Impl {
    public class SchedulerFactory : ISchedulerFactory, ITickable {
        private readonly ITimeProvider _timeProvider;
        private List<Timer> _timers = new List<Timer>();

        public SchedulerFactory(ITimeProvider timeProvider) {
            _timeProvider = timeProvider;
        }

        public Timer CreateTimer(float time, Action timesUpAction) {
            var timer = new Timer(time, timesUpAction);
            _timers.Add(timer);
            return timer;
        }

        public void Tick() {
            foreach (var timer in _timers) {
                timer.Tick(_timeProvider.DeltaTime);
            }
        }
    }
}