using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    private InputAction restartAction;

    void Awake()
    {
        // Initialize the input action for restarting
        restartAction = new InputAction("Restart", binding: "<Keyboard>/r");
        restartAction.performed += ctx => RestartScene();
        restartAction.Enable();
    }

    // Method to restart the current scene
    private void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Disable the input action when the object is destroyed
    void OnDestroy()
    {
        restartAction.Disable();
    }
}
