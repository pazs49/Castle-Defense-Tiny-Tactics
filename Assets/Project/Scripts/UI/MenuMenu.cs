using UnityEngine;
using DG.Tweening;

public class MenuMenu : MonoBehaviour
{
  public float fadeTime = 1f;
  public CanvasGroup canvasGroup;
  public RectTransform rectTransform;

  private void OnEnable()
  {
    canvasGroup.alpha = 0f;
    rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
    rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
    canvasGroup.DOFade(1, fadeTime);
  }
}
