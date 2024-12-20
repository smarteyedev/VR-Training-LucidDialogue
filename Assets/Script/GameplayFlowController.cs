using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // Make sure DOTween is imported

public class GameplayFlowController : MonoBehaviour {
    [SerializeField] CanvasGroup canvasRapportGroup;
    [SerializeField] RapportScore rapportScore;

    [SerializeField] VignetteActivator vignetteActivator;

    [SerializeField] GameObject[] gameObjectsToDeactivate;  // Array of GameObjects to deactivate

    public void SetActiveRapport() {
        if (canvasRapportGroup != null) {
            StartCoroutine(SetActiveCanvasDelay());
        }
    }

    public void DeactivateGameObjectsWithDelay(float delay) {
        if (gameObjectsToDeactivate.Length > 0) {
            StartCoroutine(DeactivateObjectsDelay(delay));
        }
    }

    IEnumerator SetActiveCanvasDelay() {
        yield return new WaitForSeconds(8);
        rapportScore.UpdateRapportUI();  // Call the method to update the rapport score
        // Set the CanvasGroup to active (enabled)
        canvasRapportGroup.gameObject.SetActive(true);
        vignetteActivator.AnimateVignetteEffect();  // Call the method to animate the vignette effect
        // Animate the alpha from 0 to 1 with DOTween
        canvasRapportGroup.DOFade(1f, 1f)  // Duration of 1 second, adjust as needed
            .SetEase(Ease.OutQuad);  // Optional: Use an easing function for smooth animation
    }

    IEnumerator DeactivateObjectsDelay(float delay) {
        // Wait for the specified delay before deactivating the objects
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in gameObjectsToDeactivate) {
            if (obj != null) {
                obj.SetActive(false);  // Deactivate the GameObject
            }
        }
    }
}
