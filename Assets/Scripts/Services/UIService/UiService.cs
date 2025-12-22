public class UiService : IUiService {
    public void TogglePanel(UIPanel panel) {
        panel.gameObject.SetActive(!panel.gameObject.activeSelf);
    }
}