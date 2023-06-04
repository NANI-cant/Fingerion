using System;
using System.Collections.Generic;
using Architecture.Services.Gameplay;
using General.StateMachine;
using UnityEngine;

namespace Gameplay.PlayerLogic.StateMachine {
    public class PlayerStateMachine: General.StateMachine.StateMachine {
        protected override Dictionary<Type, State> States { get; }

        public PlayerStateMachine(
            IInputService inputService,
            Jumper jumper,
            GroundDetector groundDetector,
            Slider slider,
            PlayerAvatar avatar, 
            GroundPlacer placer
        ) {
            States = new Dictionary<Type, State>() {
                [typeof(IdleState)] = new IdleState(this, avatar),
                [typeof(RunState)] = new RunState(this, inputService, jumper, groundDetector, avatar, placer),
                [typeof(FallState)] = new FallState(this, inputService, groundDetector, avatar, jumper),
                [typeof(SlideState)] = new SlideState(this, inputService, slider, groundDetector, jumper, avatar),
                [typeof(DeathState)] = new DeathState(this, avatar),
            };
        }
    }
}