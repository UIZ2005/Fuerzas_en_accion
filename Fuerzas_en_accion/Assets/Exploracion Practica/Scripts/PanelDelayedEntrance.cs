using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelDelayedEntrance : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform rect;

    [Header("Delay antes de aparecer")]
    public float delay = 0.9f;

    [Header("Duración animación")]
    public float duration = 0.7f;

    [Header("Movimiento entrada")]
    public float startOffsetY = -80f; // desde abajo

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();

        // Estado inicial oculto
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void OnEnable()
    {
        StartCoroutine(PlayEntrance());
    }

    IEnumerator PlayEntrance()
    {
        yield return new WaitForSeconds(delay);

        Vector2 finalPos = rect.anchoredPosition;
        Vector2 startPos = finalPos + new Vector2(0, startOffsetY);

        rect.anchoredPosition = startPos;

        float time = 0f;

        while (time < duration)
        {
            float t = Mathf.SmoothStep(0, 1, time / duration);

            canvasGroup.alpha = t;
            rect.anchoredPosition = Vector2.Lerp(startPos, finalPos, t);

            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        rect.anchoredPosition = finalPos;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}