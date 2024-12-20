using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundMoverCheck : MonoBehaviour {
    [SerializeField] private GameObject groundToMove;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float position;
    [SerializeField] private Ease easeType;
    [SerializeField] private float scaleDuration = 1.0f;
    private Vector3 origPosition;
    private Vector3 halfScale;

    private void Awake() {
        origPosition = transform.parent.position;
        halfScale = groundToMove.transform.localScale * 0.5f; // Half of the original size
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            groundToMove.SetActive(true);
            groundToMove.transform.localScale = halfScale; // Start from half the size

            groundToMove.transform.DOMoveX(position, moveSpeed)
                .SetEase(easeType);

            // Scale from half size to original size with OutBack easing
            groundToMove.transform.DOScale(groundToMove.transform.localScale * 2, scaleDuration)
                .SetEase(Ease.OutBack);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            transform.parent.DOMoveX(origPosition.x, moveSpeed)
                .SetEase(easeType);
        }
    }
}

