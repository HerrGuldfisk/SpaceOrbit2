using UnityEngine;

// WinScreen.cs
public class WinScreen : ToggleScreen
{
    public static WinScreen Instance;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Duplicate WinScreens in the scene. Deleting one.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}

