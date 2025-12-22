public class BootstrapState : State {
    private ISaveLoadService _saveLoadService;
    private IAudioService _audioService;
    public BootstrapState(IStateMachine stateMachine, ISaveLoadService saveLoadService, IAudioService audioService) : base(stateMachine) {
        _saveLoadService = saveLoadService;
        this._audioService = audioService;
    }

    public override void Enter() {
        RegisterSaveableServices();

        _saveLoadService.LoadAll();
        StateMachine.ChangeState<GameLoopState>();
    }

    private void RegisterSaveableServices() {
        var audioAdapter = new AudioServiceSaveAdapter(_audioService);
        _saveLoadService.Register<AudioSettings>(audioAdapter);
        // Тут можна додати інші адаптери
    }

    public override void Exit() {
    }
}
