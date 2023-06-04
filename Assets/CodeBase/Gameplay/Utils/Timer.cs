using System;

namespace Gameplay.Utils {
    public class Timer {
        private readonly Action _timesUpAction;
        private float _remindedTime;
        private bool _canTick = true;

        public bool IsAlreadyStop { get; private set; }

        public Timer(float time, Action timesUpAction) {
            _remindedTime = time;
            _timesUpAction = timesUpAction;
            IsAlreadyStop = false;
        }
		
        public void Tick(float deltaTime) {
            if(!_canTick) return;
            if(IsAlreadyStop) return;
			
            _remindedTime -= deltaTime;
            if (_remindedTime > 0) return;
			
            _timesUpAction?.Invoke();
            IsAlreadyStop = true;
        }

        public void Pause() => _canTick = false;
        public void Resume() => _canTick = true;
        public void Stop() => IsAlreadyStop = true;

        public void Skip() {
            if(IsAlreadyStop) return;
            
            _timesUpAction?.Invoke();
            IsAlreadyStop = true;
        }
    }
}