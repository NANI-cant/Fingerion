using System;
using System.Collections.Generic;

namespace General.StateMachine {
    public abstract class StateMachine {
        private State _activeState;
        protected abstract Dictionary<Type, State> States { get; }

        public void TranslateTo<TState>() where TState : State {
            _activeState?.Exit();
            _activeState = States[typeof(TState)];
            _activeState?.Enter();
        }
    }
}