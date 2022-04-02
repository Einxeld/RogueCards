using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    Image screenImg;
    [SerializeField] float fadeTime = 1f;
    float maxSize;

    [SerializeField] UnityEvent OnHide;

    void Awake()
    {
        instance = this;
        maxSize = transform.localScale.x;
        screenImg = GetComponent<Image>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(HideLoadingScreen());
    }

    public IEnumerator ShowLoadingScreen()
    {
        screenImg.raycastTarget = true;
        screenImg.DOFade(1f, fadeTime/3f);
        yield return transform.DOScale(maxSize, fadeTime).WaitForCompletion();
    }

    public IEnumerator HideLoadingScreen()
    {
        screenImg.DOFade(0f, fadeTime/3f).SetDelay(fadeTime/2f);
        yield return transform.DOScale(0f, fadeTime).WaitForCompletion();
        screenImg.raycastTarget = false;
        OnHide.Invoke();
    }
}
