using UnityEngine;

public class GameManager : IGameManager {

    private IStateMachine gameStateMachine;

    public GameManager(IStateMachine stateMachine) {

    }
    public void StartGame() {
        Debug.Log("Game Started!");
        // Load current level or initialize game state here
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

