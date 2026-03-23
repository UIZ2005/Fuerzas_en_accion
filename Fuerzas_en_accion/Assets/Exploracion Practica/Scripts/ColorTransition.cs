using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    [Header("Overlay de color (fondo)")]
    public Image overlayImage;

    [Header("UI de la prueba")]
    public CanvasGroup uiCanvasGroup;
    public Transform uiTransform;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Duración")]
    public float duration = 1.2f;

    [Header("Objetos a desvanecer (UI)")]
    public List<CanvasGroup> uiElements = new List<CanvasGroup>();


    [Header("Audio Settings")]
    public float audioDelay = 0.45f; // tiempo antes de reproducir
    [Range(0.5f, 1.5f)]
    public float audioPitch = 0.75f;

    // Colores
    private Color startColor = new Color(0f, 1f, 1f, 0f); // #00FFFF con alpha 0
    private Color endColor = new Color32(28, 40, 51, 255); // #1C2833

    void Start()
    {
        // Estado inicial overlay
        if (overlayImage != null)
            overlayImage.color = startColor;

        // Estado inicial UI
        if (uiCanvasGroup != null)
        {
            uiCanvasGroup.alpha = 0f;
            uiCanvasGroup.interactable = false;
            uiCanvasGroup.blocksRaycasts = false;
        }

        // Escala inicial
        if (uiTransform != null)
            uiTransform.localScale = Vector3.one * 0.95f;
        StartEvaluationTransition();
    }

    // Llamar esto cuando inicie la evaluación
    public void StartEvaluationTransition()
    {
        StartCoroutine(FullTransition());
    }

    IEnumerator FullTransition()
    {
        float time = 0f;

        // Sonido al iniciar la UI
        // Configurar velocidad del sonido
        if (audioSource != null)
            audioSource.pitch = audioPitch;

        // Reproducir con retraso
        if (audioSource != null)
            Invoke(nameof(PlayAudio), audioDelay);

        while (time < duration)
        {
            float t = Mathf.SmoothStep(0, 1, time / duration);

            // TRANSICIÓN DE FONDO
            if (overlayImage != null)
            {
                Color currentColor = Color.Lerp(startColor, endColor, t);
                overlayImage.color = currentColor;
            }

            // APARICIÓN DE UI
            if (uiCanvasGroup != null)
                uiCanvasGroup.alpha = t;

            // Efecto escala
            if (uiTransform != null)
                uiTransform.localScale = Vector3.Lerp(Vector3.one * 0.95f, Vector3.one, t);

            time += Time.deltaTime;
            yield return null;
        }

        // Valores finales
        if (overlayImage != null)
            overlayImage.color = endColor;

        if (uiCanvasGroup != null)
        {
            uiCanvasGroup.alpha = 1f;
            uiCanvasGroup.interactable = true;
            uiCanvasGroup.blocksRaycasts = true;
        }

        if (uiTransform != null)
            uiTransform.localScale = Vector3.one;
    }

    void PlayAudio()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    public void FadeOutAll()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float time = 0f;
        SetAlphaAll(1f);

        while (time < duration)
        {
            float t = Mathf.SmoothStep(0, 1, time / duration);
            float alpha = Mathf.Lerp(1f, 0f, t);

            SetAlphaAll(alpha);

            time += Time.deltaTime;
            yield return null;
        }

        SetAlphaAll(0f);
    }

    void SetAlphaAll(float alpha)
    {
        foreach (CanvasGroup cg in uiElements)
        {
            if (cg != null)
                cg.alpha = alpha;
        }
    }
}
