//shows the death screen and destroys the player game object when the player collides with this trigger object
//requires the "Player" tag on the player
//requires a rigidbody2d and a 2d collider on the player object
//requires a trigger collider on the hazard
//activates the death screen if it has the name DeathScreen

using UnityEngine;

public class DeathOnCollision : MonoBehaviour
{
    CanvasGroup _deathScreenRenderer; // Reference to the Death Screen renderer

    private void Awake()
    {
        //finds the death screen canvas renderer
        if (_deathScreenRenderer == null)
        {
            GameObject deathScreenObject = GameObject.Find("DeathScreen");
            if (!deathScreenObject)
            {
                Debug.LogError("Could not find the DeathScreen object in the scene");
                return;
            }

            _deathScreenRenderer = deathScreenObject.GetComponent<CanvasGroup>();
            if (!_deathScreenRenderer)
            {
                Debug.LogError("Could not find the canvas renderer in the death screen");
                return;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //loose if collision obj is player
        if (collision.CompareTag("Player"))
        {
            ShowDeathScreen();
            if (!collision.gameObject) Debug.LogError("No gameObject found on the collider. Cant destroy it.");
            else Destroy(collision.gameObject);
        }
    }

    private void ShowDeathScreen()
    {
        //show the death screen
        if (_deathScreenRenderer != null)
        {
            _deathScreenRenderer.alpha = 1;
        }
        else
        {
            Debug.LogError("Can't show death screen. Missing reference to renderer component");
        }
    }
}
