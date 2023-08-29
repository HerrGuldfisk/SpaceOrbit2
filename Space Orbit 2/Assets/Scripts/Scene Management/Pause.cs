using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    private InputAction pauseAction;
    private bool isPaused = false;

    void Awake()
    {
        // Initialize the input action for pausing
        pauseAction = new InputAction("Pause", binding: "<Keyboard>/p");
        pauseAction.performed += ctx => TogglePause();
        pauseAction.Enable();
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            // Unpause the game
            Time.timeScale = 1f;
        }
        else
        {
            // Pause the game
            Time.timeScale = 0f;
        }
        isPaused = !isPaused;  // Toggle the pause state
    }

    void OnDestroy()
    {
        pauseAction.Disable();
    }
}
