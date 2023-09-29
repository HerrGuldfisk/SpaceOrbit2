using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public static CanvasGroup canvasGroup { get; private set; }

    void Awake()
    {
        if (canvasGroup != null && canvasGroup.gameObject != gameObject)
        {
            Debug.LogError("Duplicate DeathScreens in scene. Deleting one.");
            Destroy(gameObject);
            return;
        }
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
