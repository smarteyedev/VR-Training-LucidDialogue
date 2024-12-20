using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management functions

public class RestartLevel : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Function to restart the current level
    public void RestartCurrentLevel() {
        // Get the active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
