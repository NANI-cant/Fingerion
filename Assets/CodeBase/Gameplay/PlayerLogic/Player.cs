using System;
using Architecture.Services.Gameplay;
using Gameplay.PlayerLogic.StateMachine;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(GroundDetector))]
    [RequireComponent(typeof(BumpDetector))]
    [RequireComponent(typeof(Jumper))]
    [RequireComponent(typeof(PlayerAvatar))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GroundPlacer))]
    public class Player: MonoBehaviour {
        private GroundDetector _groundDetector;
        private Jumper _jumper;
        private Slider _slider;
        private IInputService _input;
        private PlayerAvatar _avatar;
        private GroundPlacer _placer;

        public PlayerStateMachine StateMachine { get; private set; }

        private void Awake() {
            _groundDetector = GetComponent<GroundDetector>();
            _jumper = GetComponent<Jumper>();
            _slider = GetComponent<Slider>();
            _avatar = GetComponent<PlayerAvatar>();
            _placer = GetComponent<GroundPlacer>();
        }

        public void Construct(IInputService inputService) {
            StateMachine = new PlayerStateMachine(inputService, _jumper, _groundDetector, _slider, _avatar, _placer);
        }

        private void Start() {
            StateMachine.TranslateTo<IdleState>();
        }
    }
}