using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DoTween namespace

public class ScaleYAnimation : MonoBehaviour {
    [SerializeField] private float animationDuration = 1f; // Duration of the animation
    [SerializeField] private Ease animationEase = Ease.OutBack; // Ease type for the opening animation
    [SerializeField] private Ease closeAnimationEase = Ease.InBack; // Ease type for the closing animation

    private Vector3 originalScale;

    private void OnEnable() {
        // Store the original scale of the GameObject
        originalScale = transform.localScale;
    }

    public void AnimateYScaleOpen() {

        // Set the initial scale to 0 on the Y-axis
        Vector3 initialScale = new Vector3(originalScale.x, 0f, originalScale.z);
        transform.localScale = initialScale;
        // Animate the Y-scale using DoTween to open
        transform.DOScaleY(2, animationDuration)
                 .SetEase(animationEase); // Set easing for the animation
    }

    public void AnimateYScaleClose() {
        // Animate the Y-scale using DoTween to close
        transform.DOScaleY(0f, animationDuration)
                 .SetEase(closeAnimationEase); // Set easing for the closing animation
    }
}
