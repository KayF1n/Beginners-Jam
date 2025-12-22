using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour {
    [Inject] IUiService uiService;
    [Inject] IGameManager gameManager;
    [SerializeField] UIPanel settingsPanel;

    public void StartGame() {
        gameManager.StartGame();
    }

    public void ToggleSettings() {
        uiService.TogglePanel(settingsPanel);
    }

    public void ExitGame() {
        gameManager.ExitGame();
    }
}