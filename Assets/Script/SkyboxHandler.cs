using UnityEngine;
using DG.Tweening; // Import DOTween namespace

public class SkyboxHandler : MonoBehaviour {
    public Material skyboxMaterial;

    private static readonly int TintColorID = Shader.PropertyToID("_TintColor");

    // Property to get/set the skybox color
    public Color SkyboxColor {
        get => skyboxMaterial.GetColor(TintColorID);
        set => skyboxMaterial.SetColor(TintColorID, value);
    }

    public Color originalSkyboxColor = Color.white; // Set the original skybox color
    public float animationTime = 1f; // Default time for animation
    public Ease animationEase = Ease.Linear; // Default easing

    private void Start() {
        // Set the skybox to the original color at the start
        if (skyboxMaterial != null) {
            SkyboxColor = originalSkyboxColor;
        }
    }

    // Method to animate skybox color change progressively
    public void AnimateSkyboxColor(Color targetColor, float duration, Ease easeType) {
        // Animate from the current skybox color to the target color over the given duration
        DOTween.To(() => SkyboxColor, x => SkyboxColor = x, targetColor, duration)
            .SetEase(easeType); // Use DOTween to animate color change with easing
    }
}
