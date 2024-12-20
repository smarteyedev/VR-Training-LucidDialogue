using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Make sure you have the DOTween plugin installed and imported
using UnityEngine.UI; // For handling UI elements like Image

public class BackgroundPlayButtonAnimation : MonoBehaviour {
    [SerializeField] private Image targetImage; // The UI Image component to animate
    [SerializeField] private float animationDuration = 1f; // Duration of the scale and transparency animation
    [SerializeField] private float scaleFactor = 1.5f; // Scale multiplier

    private Vector3 originalScale;

    void Start() {
        if (targetImage == null) {
            Debug.LogError("Target Image is not assigned.");
            return;
        }

        // Store the original scale of the image
        originalScale = targetImage.rectTransform.localScale;

        // Start the animation sequence
        PlayAnimation();
    }

    void PlayAnimation() {
        // Create a sequence for the animations
        Sequence animationSequence = DOTween.Sequence();

        // Scale up the image and fade in
        animationSequence.Append(targetImage.rectTransform.DOScale(originalScale * scaleFactor, animationDuration).SetEase(Ease.InOutSine));
        animationSequence.Join(targetImage.DOFade(0f, animationDuration).SetEase(Ease.InOutSine));

        // Immediately reset scale and alpha
        animationSequence.Append(targetImage.rectTransform.DOScale(originalScale, 0f));
        animationSequence.Append(targetImage.DOFade(1f, 0f));

        // Loop the sequence infinitely
        animationSequence.SetLoops(-1);
    }
}
