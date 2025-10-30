using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class UIFade : MonoBehaviour
{
    [SerializeField]
    float delay;

    [SerializeField]
    float fadeDuration;

    CanvasGroup canvasGroup;

    [SerializeField]
    UnityEvent OnEnabled;

    void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup.alpha != 1)
        {
            FadeToOpaque();
        }
        else
        {
            FadeToTransparent();
        }
        OnEnabled.Invoke();
    }

    public void FadeToTransparent()
    {
        StartCoroutine(FadeToTransparentCor());
    }

    IEnumerator FadeToTransparentCor()
    {
        yield return new WaitForSecondsRealtime(delay);
        while (canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= 1 / fadeDuration * Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void FadeToOpaque()
    {
        StartCoroutine(FadeToOpaqueCor());
    }

    IEnumerator FadeToOpaqueCor()
    {
        yield return new WaitForSecondsRealtime(delay);

        while (canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += 1 / fadeDuration * Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
