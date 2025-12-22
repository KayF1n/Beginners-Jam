using UnityEngine;

public class UIPanel : MonoBehaviour {
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration = 0.2f;
   
    public virtual void Show() {
        gameObject.SetActive(true);
        if (_canvasGroup != null) {
            StartCoroutine(FadeIn());
        }
    }

    public virtual void Hide() {
        if (_canvasGroup != null) {
            StartCoroutine(FadeOut());
        } else {
            gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator FadeIn() {
        _canvasGroup.alpha = 0;
        float elapsed = 0;

        while (elapsed < _fadeDuration) {
            _canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / _fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 1;
    }

    private System.Collections.IEnumerator FadeOut() {
        float elapsed = 0;

        while (elapsed < _fadeDuration) {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / _fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }
}