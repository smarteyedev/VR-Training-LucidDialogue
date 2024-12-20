using UnityEngine;
using DG.Tweening;

public class PlayButtonAnimation : MonoBehaviour {
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject button;
    [SerializeField] private CanvasGroup stick;
    [SerializeField] private CanvasGroup parent;

    [SerializeField] private float buttonScale = 1.5f;
    [SerializeField] private float backgroundScale = 2f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease easing = Ease.OutBack; // Ease type for animations

    private Vector3 buttonOrigScale;
    private Vector3 backgroundOrigScale;

    private void Awake() {
        buttonOrigScale = button.transform.localScale;
        backgroundOrigScale = background.transform.localScale;
    }

    public void AnimateSizeUp() {
        // Animate button and background scale
        AnimateTransform(button.transform, buttonOrigScale * buttonScale);
        AnimateTransform(background.transform, backgroundOrigScale * backgroundScale);

        // Animate stick (CanvasGroup) fade to 0 (transparent)
        stick.DOFade(0f, duration).SetEase(easing);
    }

    public void AnimateSizeBack() {
        // Animate button and background scale back to original size
        AnimateTransform(button.transform, buttonOrigScale);
        AnimateTransform(background.transform, backgroundOrigScale);

        // Animate stick (CanvasGroup) fade back to 1 (opaque)
        stick.DOFade(1f, duration).SetEase(easing);
    }

    public void OnClickAnimation() {
        // Create a sequence to play both animations simultaneously
        Sequence sequence = DOTween.Sequence();

        // Scale button and background up by 1.5x
        sequence.Join(button.transform.DOScale(buttonOrigScale * 1.5f, duration + 0.2f).SetEase(easing));
        sequence.Join(background.transform.DOScale(backgroundOrigScale * 1.5f, duration + 0.2f).SetEase(easing));

        // Fade out parent group (which will affect both button and background)
        sequence.Join(parent.DOFade(0f, duration).SetEase(easing));

        // Disable the parent GameObject after the sequence finishes
        sequence.OnKill(() => {
            parent.gameObject.SetActive(false); // Disable the parent GameObject
        });
    }


    private void AnimateTransform(Transform target, Vector3 scale) {
        target.DOScale(scale, duration).SetEase(easing);
    }
}
