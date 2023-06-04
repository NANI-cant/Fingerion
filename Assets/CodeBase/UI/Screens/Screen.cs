using UnityEngine;

namespace UI.Screens {
    public class Screen: MonoBehaviour {
        [SerializeField] private ScreenId _screenId;

        public ScreenId Id => _screenId;
    }
}