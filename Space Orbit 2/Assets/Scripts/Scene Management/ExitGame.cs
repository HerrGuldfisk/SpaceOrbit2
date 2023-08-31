using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitGame : MonoBehaviour
{

    private InputAction exitAction;

    void Awake()
    {
        // Initialize the input action for exiting
        exitAction = new InputAction("Exit", binding: "<Keyboard>/escape");
        exitAction.performed += ctx => Exit();
        exitAction.Enable();
    }

    // Method to exit the game
    private void Exit()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    // Disable the input action when the object is destroyed
    void OnDestroy()
    {
        exitAction.Disable();
    }
}
