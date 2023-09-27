//triggers the death screen and destroys the player game object when the player collides with this trigger object
//requires the "Player" tag on the player
//requires a rigidbody2d and a 2d collider on the player object
//requires a trigger collider on the hazard
//activates the death screen if it has the name DeathScreen

using UnityEngine;

public class deathOnCollision : MonoBehaviour
{
    GameObject _deathScreen; // Reference to the Death Screen UI object

    private void Awake()
    {
        _deathScreen = GameObject.Find("DeathScreen"); //Find the death screen object
        if (!_deathScreen) Debug.LogError("Could not find the DeathScreen object in the scene");
        else
        {
            _deathScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowDeathScreen();
            Destroy(collision.gameObject);
        }
    }

    private void ShowDeathScreen()
    {
        if (_deathScreen != null)
        {
            _deathScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("Death Screen not assigned in the inspector");
        }
    }
}
