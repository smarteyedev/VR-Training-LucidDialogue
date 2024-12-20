using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Make sure to import UnityEngine.UI namespace for Image component
using DG.Tweening;    // Make sure to import DOTween namespace

public class HandleAnimation : MonoBehaviour {
    // Reference to the Image component
    private Image imageComponent;

    // Start is called before the first frame update
    void Start() {
        // Get the Image component from the GameObject
        imageComponent = GetComponent<Image>();
    }

    // Function to animate the image color to red and scale to 1.2, then back to original
    public void AnimateToRed() {
        // Set the color to red and animate the scale
        imageComponent.DOColor(Color.red, 0.5f) // Change color to red over 0.5 seconds
            .OnKill(() => ResetColorAndScale()); // Call ResetColorAndScale when animation is done

        transform.DOScale(transform.localScale * 1.5f, 0.5f) // Animate scale to 1.2x
            .OnKill(() => ResetColorAndScale()); // Reset after animation completes
    }

    // Function to animate the image color to blue and scale to 1.2, then back to original
    public void AnimateToBlue() {
        // Set the color to blue and animate the scale
        imageComponent.DOColor(Color.blue, 0.5f) // Change color to blue over 0.5 seconds
            .OnKill(() => ResetColorAndScale()); // Call ResetColorAndScale when animation is done

        transform.DOScale(transform.localScale * 1.5f, 0.5f) // Animate scale to 1.2x
            .OnKill(() => ResetColorAndScale()); // Reset after animation completes
    }

    // Reset the color and scale back to original
    private void ResetColorAndScale() {
        // Reset color to original (white) and scale back to original
        imageComponent.DOColor(Color.white, 0.5f); // Reset color to white
        transform.DOScale(transform.localScale / 1.5f, 0.5f); // Reset scale back to original
    }

    // Debug method to log current color and scale (optional for debugging in the future)
    private void OnDrawGizmos() {
        // You can uncomment this if you want to log current color and scale in the editor
        // Debug.Log("Current Color: " + imageComponent.color + " | Current Scale: " + transform.localScale);
    }
}
