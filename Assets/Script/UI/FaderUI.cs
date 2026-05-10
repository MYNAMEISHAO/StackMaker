using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;
using System.Collections;
using System;

public class FaderUI : MonoBehaviour
{
    public Image fadeImage;

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void FadeIn(float duration)
    {
        fadeImage.DOFade(0f, duration).SetEase(Ease.Linear).OnComplete(() => { 
            fadeImage.raycastTarget = false;
            InputManager.Instance.ActiveInput();
        });
    }

    public void Transition(Action action, float duration)
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(WaitToFadeIn(action, duration));
            InputManager.Instance.DeActiveInput();
        });

    }

    public IEnumerator WaitToFadeIn(Action action,float duration)
    {
        action?.Invoke();
        yield return new WaitForSeconds(duration);
        FadeIn(1f);
    }
}