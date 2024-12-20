using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DoTween namespace

public class ModularSpinningAnimation : MonoBehaviour {
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = new Vector3(0, 360, 0); // Axis to rotate around
    public float duration = 5f; // Duration of one full rotation
    public bool loopForever = true; // Loop infinitely
    public int loopCount = 1; // Loop count if not looping forever
    public LoopType loopType = LoopType.Restart; // Loop behavior: Restart, Yoyo, Incremental

    [Header("Ease Settings")]
    public Ease rotationEase = Ease.Linear; // Easing for the rotation

    [Header("Control Settings")]
    public bool playOnStart = true; // Should it play as soon as the game starts?

    private Tween rotationTween; // Reference to the tween
    private bool isPlaying = false; // State of the animation

    void Awake() {
        // Set a random rotation axis
        rotationAxis = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
    }

    void Start() {
        if (playOnStart) {
            StartSpinning();
        }
    }

    /// <summary>
    /// Starts the spinning animation.
    /// </summary>
    public void StartSpinning() {
        if (rotationTween != null && rotationTween.IsActive()) {
            rotationTween.Kill(); // Kill any existing tween
        }

        // Calculate loops
        int loops = loopForever ? -1 : loopCount;

        // Create rotation tween
        rotationTween = transform.DORotate(rotationAxis, duration, RotateMode.FastBeyond360)
            .SetEase(rotationEase)
            .SetLoops(loops, loopType)
            .OnStart(() => isPlaying = true)
            .OnComplete(() => isPlaying = false);

        isPlaying = true;
    }

    /// <summary>
    /// Stops the spinning animation.
    /// </summary>
    public void StopSpinning() {
        if (rotationTween != null) {
            rotationTween.Kill();
            rotationTween = null;
        }

        isPlaying = false;
    }

    /// <summary>
    /// Toggles the spinning animation on/off.
    /// </summary>
    public void ToggleSpinning() {
        if (isPlaying) {
            StopSpinning();
        } else {
            StartSpinning();
        }
    }

    /// <summary>
    /// Updates the rotation speed (duration of one full rotation).
    /// </summary>
    /// <param name="newDuration">New duration for one rotation.</param>
    public void UpdateRotationSpeed(float newDuration) {
        duration = newDuration;
        if (isPlaying) {
            StartSpinning();
        }
    }

    /// <summary>
    /// Updates the rotation axis dynamically.
    /// </summary>
    /// <param name="newAxis">New rotation axis.</param>
    public void UpdateRotationAxis(Vector3 newAxis) {
        rotationAxis = newAxis;
        if (isPlaying) {
            StartSpinning();
        }
    }
}
