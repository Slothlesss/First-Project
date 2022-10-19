using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup CanvasGroup;
    public RectTransform RectTransform;

    private void Start() => PanelFadeIn();
    public void PanelFadeIn()
    {
        CanvasGroup.alpha = 0f;
        RectTransform.transform.localPosition = new Vector3(0f, -1000, 0f);
        RectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
        CanvasGroup.DOFade(1, fadeTime);
    }
    public void PanelFadeOut()
    {
        CanvasGroup.alpha = 1f;
        RectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        RectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.InOutQuint);
        CanvasGroup.DOFade(1, fadeTime);
    }
}
