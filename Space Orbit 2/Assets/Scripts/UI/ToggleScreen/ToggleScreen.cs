using UnityEngine;

public class ToggleScreen : MonoBehaviour
{
  protected CanvasGroup canvasGroup;

  protected virtual void Awake()
  {
    canvasGroup = GetComponent<CanvasGroup>();
  }

  public void Show()
  {
    canvasGroup.alpha = 1;
  }

  public void Hide()
  {
    canvasGroup.alpha = 0;
  }
}

