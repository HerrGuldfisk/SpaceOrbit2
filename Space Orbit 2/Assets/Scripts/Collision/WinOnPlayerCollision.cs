using UnityEngine;

public class WinOnPlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Debug.Log("'player collided with win object'");
        WinScreen.Instance.Show();
    }
}
