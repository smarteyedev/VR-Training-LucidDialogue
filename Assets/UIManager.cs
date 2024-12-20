using System.Collections;
using UnityEngine;
using DG.Tweening;
using Tproject.AudioManager;
public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject dialogueCanvas;

    public void ActivateCanvas(float delayTime) {
        StartCoroutine(ActivateCanvasWithDelay(delayTime));
    }

    private IEnumerator ActivateCanvasWithDelay(float delayTime) {
        AudioManager.Instance.PlaySFX("dialogueOpen");
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        // Set the canvas to active
        dialogueCanvas.SetActive(true);

        // Get the CanvasGroup component
        CanvasGroup canvasGroup = dialogueCanvas.GetComponent<CanvasGroup>();

        if (canvasGroup != null) {
            // Use DOTween to fade in the canvas
            canvasGroup.alpha = 0f; // Ensure it's initially transparent
            canvasGroup.DOFade(1f, 1f); // Fade in over 1 second
        }
    }
}
