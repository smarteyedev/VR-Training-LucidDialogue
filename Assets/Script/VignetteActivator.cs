using UnityEngine;
using DG.Tweening; // Make sure you have DOTween installed

public class VignetteActivator : MonoBehaviour {
    // Reference to the material that uses the Vignette shader
    public Material vignetteMaterial;

    // Parameters to control the vignette effect
    public float targetApertureSize = 0.7f;
    public float targetFeatheringEffect = 0.2f;
    public Color vignetteColor = Color.black;
    public Color vignetteColorBlend = Color.black;

    // Duration for the animation
    public float animationDuration = 1f;

    private void Start() {
        // Ensure the material is set (could be assigned in Inspector)
        if (vignetteMaterial == null) {
            Debug.LogError("Vignette material not assigned!");
            return;
        }

        // Initially set aperture size and feathering to 0
        SetVignetteEffect(0f, 0f, vignetteColor, vignetteColorBlend);

        // Animate the aperture and feathering values to their target values using DOTween
        AnimateVignetteEffect();
    }

    // Method to activate and update the vignette effect
    public void SetVignetteEffect(float aperture, float feathering, Color color, Color colorBlend) {
        if (vignetteMaterial != null) {
            // Set the shader properties on the material
            vignetteMaterial.SetFloat("_ApertureSize", aperture);
            vignetteMaterial.SetFloat("_FeatheringEffect", feathering);
            vignetteMaterial.SetColor("_VignetteColor", color);
            vignetteMaterial.SetColor("_VignetteColorBlend", colorBlend);
        }
    }

    // Method to animate the vignette effect over time
    public void AnimateVignetteEffect() {
        if (vignetteMaterial != null) {
            // Animate aperture size and feathering effect using DOTween with Ease.OutBack
            DOTween.To(() => vignetteMaterial.GetFloat("_ApertureSize"),
                       x => vignetteMaterial.SetFloat("_ApertureSize", x),
                       targetApertureSize,
                       animationDuration).SetEase(Ease.OutBack);  // Adding Ease.OutBack

            DOTween.To(() => vignetteMaterial.GetFloat("_FeatheringEffect"),
                       x => vignetteMaterial.SetFloat("_FeatheringEffect", x),
                       targetFeatheringEffect,
                       animationDuration).SetEase(Ease.OutBack);  // Adding Ease.OutBack
        }
    }

    // Optionally: You can disable the vignette effect or reset it
    public void ResetVignetteEffect() {
        if (vignetteMaterial != null) {
            // Reset properties to default
            vignetteMaterial.SetFloat("_ApertureSize", targetApertureSize);
            vignetteMaterial.SetFloat("_FeatheringEffect", targetFeatheringEffect);
            vignetteMaterial.SetColor("_VignetteColor", Color.black);
            vignetteMaterial.SetColor("_VignetteColorBlend", Color.black);
        }
    }

    // Public method to smoothly animate aperture and feathering effects to zero
    public void AnimateVignetteToZero(float speed) {
        if (vignetteMaterial != null) {
            // Adjust the animation duration based on the speed
            float adjustedDuration = speed;

            // Animate aperture size and feathering effect to zero using DOTween
            DOTween.To(() => vignetteMaterial.GetFloat("_ApertureSize"),
                       x => vignetteMaterial.SetFloat("_ApertureSize", x),
                       0f,
                       adjustedDuration).SetEase(Ease.InCubic);

            DOTween.To(() => vignetteMaterial.GetFloat("_FeatheringEffect"),
                       x => vignetteMaterial.SetFloat("_FeatheringEffect", x),
                       0f,
                       adjustedDuration).SetEase(Ease.InCubic);
        }
    }
    private void OnApplicationQuit() {
        ResetVignetteEffect();
    }

}
