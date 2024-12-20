using UnityEngine;

public class FollowCameraRotation : MonoBehaviour {
    private Transform cameraTransform;

    private void Start() {
        cameraTransform = Camera.main.transform;
    }

    private void Update() {
        // Only follow the Y rotation of the camera
        Vector3 targetRotation = new Vector3(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(targetRotation);
    }
}
