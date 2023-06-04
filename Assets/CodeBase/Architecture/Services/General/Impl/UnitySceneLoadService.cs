using UnityEngine.SceneManagement;

namespace Architecture.Services.General.Impl {
    public class UnitySceneLoadService: ISceneLoadService {
        private const string GameplayName = "Gameplay";
        
        public void LoadGameplay() => SceneManager.LoadSceneAsync(GameplayName);
        public void Reload() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}