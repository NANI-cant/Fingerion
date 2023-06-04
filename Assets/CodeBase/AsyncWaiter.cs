using Architecture.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class AsyncWaiter: MonoBehaviour {
    private IPersistentProgressService _persistentProgressService;

    [Inject]
    public void Construct(IPersistentProgressService persistentProgressService) {
        _persistentProgressService = persistentProgressService;
    }

    private void Update() {
        if(_persistentProgressService.Progress == null) return;

        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}