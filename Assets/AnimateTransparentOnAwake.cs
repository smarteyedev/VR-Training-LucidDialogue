using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AnimateTransparentOnAwake : MonoBehaviour {
    public CanvasGroup[] canvasGroups;

    public float fadeDuration = 0.5f;
    public float scaleDuration = 0.3f;
    public float scaleUpFactor = 1.1f;
    public float delayBetweenAnimations = 0.15f;


    private void OnEnable() {
        if (canvasGroups.Length > 0) {
            SetAlphaToZero();
            StartCoroutine(AnimateWithDelay());
        } else {
            Debug.LogWarning("No CanvasGroup components found on " + gameObject.name);
        }
    }

    private IEnumerator AnimateWithDelay() {
        foreach (CanvasGroup canvasGroup in canvasGroups) {
            Vector3 canvasGroupOrigScale = canvasGroup.transform.localScale;
            if (canvasGroup != null) {
                canvasGroup.DOFade(1, fadeDuration);
                canvasGroup.transform.DOScale(canvasGroupOrigScale * scaleUpFactor, scaleDuration).SetEase(Ease.OutBack)
                         .OnComplete(() => canvasGroup.transform.DOScale(canvasGroupOrigScale, scaleDuration).SetEase(Ease.InBack));

                yield return new WaitForSeconds(delayBetweenAnimations);
            } else {
                Debug.LogWarning("CanvasGroup component not found on " + gameObject.name);
            }
        }
    }

    private void SetAlphaToZero() {
        foreach (CanvasGroup canvasGroup in canvasGroups) {
            if (canvasGroup != null) {
                canvasGroup.alpha = 0;
            } else {
                Debug.LogWarning("CanvasGroup component not found on " + gameObject.name);
            }
        }
    }

    public void FadeToZero() {
        if (canvasGroups.Length > 0) {
            foreach (CanvasGroup canvasGroup in canvasGroups) {
                if (canvasGroup != null) {
                    canvasGroup.DOFade(0, fadeDuration);
                }
            }
        }
    }

    public void FadeToOne() {
        if (canvasGroups.Length > 0) {
            foreach (CanvasGroup canvasGroup in canvasGroups) {
                if (canvasGroup != null) {
                    canvasGroup.DOFade(1, fadeDuration);
                }
            }
        }
    }
}
