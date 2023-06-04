using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Dialog: MonoBehaviour {
        [SerializeField] private Button _cancel;
        [SerializeField] private Button _yes;
        [SerializeField] private Button _no;

        public event Action OnYes;
        public event Action OnNo;

        private void Start() {
            _cancel.onClick.AddListener(No);
            _yes.onClick.AddListener(Yes);
            _no.onClick.AddListener(No);
        }

        private void OnDestroy() {
            _cancel.onClick.RemoveListener(No);
            _yes.onClick.RemoveListener(Yes);
            _no.onClick.RemoveListener(No);
        }

        private void Yes() {
            OnYes?.Invoke();
            Destroy(gameObject);
        }

        private void No() {
            OnNo?.Invoke();
            Destroy(gameObject);
        }
    }
}