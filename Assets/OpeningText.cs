using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public class TextAnimationData {
    public string text; // The text to display
    public float midDelay; // Delay after text appears but before fade out
    public UnityEvent onTextAnimationFinish; // UnityEvent to be triggered when the animation finishes for this specific text
}

public class OpeningText : MonoBehaviour {
    [Header("Text Settings")]
    public TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component
    public TextAnimationData[] textDataArray; // Array of text and delay pairs

    [Header("Animation Settings")]
    public float initialDelay = 2f; // Delay before the animation starts for each text
    public Vector3 startOffset = new Vector3(0, -50, 0); // Start position offset
    public Vector3 endOffset = new Vector3(0, 50, 0); // End position offset
    public float animationDuration = 1f; // Duration of the animation

    private int currentIndex = 0;

    void Start() {
        if (textDataArray.Length > 0) {
            StartCoroutine(AnimateText());
        }
    }

    IEnumerator AnimateText() {
        while (true) {
            // Get the current text and mid-delay
            var currentData = textDataArray[currentIndex];

            // Wait for the initial delay before the text appears
            yield return new WaitForSeconds(initialDelay);

            // Set the text
            textMeshPro.text = currentData.text;

            // Set starting position and set alpha to 0 (invisible)
            textMeshPro.rectTransform.anchoredPosition = startOffset;
            textMeshPro.alpha = 0f;

            // Animate to the desired position and fade in
            textMeshPro.rectTransform.DOAnchorPos(Vector3.zero, animationDuration).SetEase(Ease.OutCubic);
            textMeshPro.DOFade(1f, animationDuration).SetEase(Ease.OutCubic);

            // Wait for animation to complete (before mid-delay)
            yield return new WaitForSeconds(animationDuration);

            // Wait for mid delay (after the text has moved to its target position)
            yield return new WaitForSeconds(currentData.midDelay);

            // Fade out and move further up
            Sequence fadeOutSequence = DOTween.Sequence();
            fadeOutSequence
                .Append(textMeshPro.DOFade(0, animationDuration).SetEase(Ease.InCubic))
                .Join(textMeshPro.rectTransform.DOAnchorPos(endOffset, animationDuration).SetEase(Ease.InCubic));

            // Wait for the fade-out animation to complete
            yield return fadeOutSequence.WaitForCompletion();

            // Check if the UnityEvent is not null and has listeners
            if (currentData.onTextAnimationFinish != null && currentData.onTextAnimationFinish.GetPersistentEventCount() > 0) {
                // Trigger the UnityEvent specific to the current text animation
                currentData.onTextAnimationFinish.Invoke();
            }

            // Move to the next index, loop back if necessary
            currentIndex = (currentIndex + 1) % textDataArray.Length;
        }
    }
}
