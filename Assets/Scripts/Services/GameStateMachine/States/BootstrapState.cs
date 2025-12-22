using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class BootstrapState : State {
    private ISaveLoadService _saveLoadService;
    private IAudioService _audioService;
    private ILevelProgressService _levelProgress;
    public BootstrapState(IStateMachine stateMachine, ISaveLoadService saveLoadService, IAudioService audioService, ILevelProgressService levelProgressService) : base(stateMachine) {
        _saveLoadService = saveLoadService;
        _audioService = audioService;
        _levelProgress = levelProgressService;
    }

    public override void Enter() {
        _saveLoadService.LoadAll();
        StateMachine.ChangeState<GameLoopState>();
    }

    public override void Exit() {
    }
}

public interface ISceneLoader {
    UniTask LoadAsync(string sceneName, IProgress<float> progress = null, CancellationToken cancellationToken = default);
    void Load(string sceneName, Action onLoaded = null);
}

public class SceneLoader : ISceneLoader {
    public async UniTask LoadAsync(string sceneName, IProgress<float> progress = null, CancellationToken cancellationToken = default) {
        if (string.IsNullOrEmpty(sceneName))
            throw new ArgumentException("Scene name is empty", nameof(sceneName));

        // UniTask має вбудований метод ToUniTask, який приймає progress та cancellationToken
        await SceneManager.LoadSceneAsync(sceneName)
            .ToUniTask(progress: progress, cancellationToken: cancellationToken);
    }

    public void Load(string sceneName, Action onLoaded = null) {
        // Запускаємо асинхронний метод у стилі "fire and forget"
        LoadAndForget(sceneName, onLoaded).Forget();
    }

    private async UniTaskVoid LoadAndForget(string sceneName, Action onLoaded) {
        await LoadAsync(sceneName);
        onLoaded?.Invoke();
    }
}