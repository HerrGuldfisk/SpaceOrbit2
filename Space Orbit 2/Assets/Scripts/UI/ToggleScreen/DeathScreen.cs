using UnityEngine;

public class DeathScreen : ToggleScreen
{
    public static DeathScreen Instance;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Duplicate DeathScreens in the scene. Deleting one.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
