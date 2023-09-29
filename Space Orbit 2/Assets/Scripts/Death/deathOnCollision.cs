//shows the death screen and destroys the player game object when the player collides with this trigger object
//requires the "Player" tag on the player
//requires a rigidbody2d and a 2d collider on the player object
//requires a trigger collider on the hazard
//sets alpha to 1 on the canvasGroup of the "DeathScreen" object

using UnityEngine;

public class DeathOnCollision : MonoBehaviour
{
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
        if (DeathScreen.canvasGroup != null)
        {
            DeathScreen.canvasGroup.alpha = 1;
        }
        else
        {
            Debug.LogError("Can't show death screen. No static variable canvasGroup found");
        }
    }
}
