using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    private InputAction pauseAction;
    private bool isPaused = false;
    private GameObject pauseCanvas;

    void Awake()
    {
        // Initialize the input action for pausing
        pauseAction = new InputAction("Pause", binding: "<Keyboard>/p");
        pauseAction.performed += ctx => TogglePause();
        pauseAction.Enable();

        // Get the pause canvas
        pauseCanvas = GameObject.Find("PauseCanvas");
        pauseCanvas.SetActive(false);
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            // Unpause the game
            Time.timeScale = 1f;
            if(pauseCanvas) pauseCanvas.SetActive(false);
        }
        else
        {
            // Pause the game
            Time.timeScale = 0f;
            if(pauseCanvas) pauseCanvas.SetActive(true);
        }
        isPaused = !isPaused;  // Toggle the pause state
    }

    void OnDestroy()
    {
        pauseAction.Disable();
    }
}
