using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorTransition : MonoBehaviour
{
    public QuestionAnimator questionAnimator;

    [Header("Panel inicial (el primero visible)")]
    public CanvasGroup panelInicial;

    [Header("Duración transición")]
    public float duration = 1.2f;

    [Header("Overlay (opcional)")]
    public Image overlayImage;

    [Header("Audio (opcional)")]
    public AudioSource audioSource;
    public float audioDelay = 0.45f;
    [Range(0.5f, 1.5f)]
    public float audioPitch = 0.75f;

    // Panel actual
    private CanvasGroup currentPanel;

    // Historial de paneles
    private Stack<CanvasGroup> panelHistory = new Stack<CanvasGroup>();

    private Color startColor = new Color(0f, 1f, 1f, 0f);
    private Color endColor = new Color32(28, 40, 51, 255);

    // =====================================================
    // START
    // =====================================================
    void Start()
    {
        if (panelInicial != null)
        {
            PrepareInitialPanel(panelInicial);
            StartCoroutine(IntroTransition(panelInicial));
        }

        if (overlayImage != null)
            overlayImage.color = startColor;
    }

    // =====================================================
    // PREPARAR PANEL INICIAL (estado invisible)
    // =====================================================
    void PrepareInitialPanel(CanvasGroup panel)
    {
        currentPanel = panel;
        panelHistory.Clear();
        panelHistory.Push(panel);

        panel.alpha = 0f;
        panel.interactable = false;
        panel.blocksRaycasts = false;

        panel.transform.localScale = Vector3.one * 0.95f;
    }

    // =====================================================
    // ANIMACIÓN DE ENTRADA INICIAL ⭐
    // =====================================================
    IEnumerator IntroTransition(CanvasGroup panel)
    {
        float time = 0f;

        if (audioSource != null)
        {
            audioSource.pitch = audioPitch;
            Invoke(nameof(PlayAudio), audioDelay);
        }

        while (time < duration)
        {
            float t = Mathf.SmoothStep(0, 1, time / duration);

            panel.alpha = t;

            panel.transform.localScale =
                Vector3.Lerp(Vector3.one * 0.95f, Vector3.one, t);

            if (overlayImage != null)
                overlayImage.color = Color.Lerp(startColor, endColor, t);

            time += Time.deltaTime;
            yield return null;
        }

        panel.alpha = 1f;
        panel.interactable = true;
        panel.blocksRaycasts = true;
        panel.transform.localScale = Vector3.one;
    }

    // =====================================================
    // IR A OTRO PANEL
    // =====================================================
    public void GoToPanel(CanvasGroup newPanel)
    {
        if (newPanel == null || currentPanel == newPanel)
            return;

        panelHistory.Push(newPanel);
        StartCoroutine(SwitchPanel(currentPanel, newPanel));

        currentPanel = newPanel;
    }

    // =====================================================
    // VOLVER
    // =====================================================
    public void GoBack()
    {
        if (panelHistory.Count <= 1)
            return;

        panelHistory.Pop();
        CanvasGroup previousPanel = panelHistory.Peek();

        StartCoroutine(SwitchPanel(currentPanel, previousPanel));

        currentPanel = previousPanel;
    }

    // =====================================================
    // TRANSICIÓN ENTRE PANELES
    // =====================================================
    IEnumerator SwitchPanel(CanvasGroup from, CanvasGroup to)
    {
        float time = 0f;

        if (audioSource != null)
        {
            audioSource.pitch = audioPitch;
            Invoke(nameof(PlayAudio), audioDelay);
        }

        to.alpha = 0f;
        to.interactable = true;
        to.blocksRaycasts = true;
        to.transform.localScale = Vector3.one * 0.95f;

        while (time < duration)
        {
            float t = Mathf.SmoothStep(0, 1, time / duration);

            if (from != null)
                from.alpha = 1f - t;

            to.alpha = t;

            to.transform.localScale =
                Vector3.Lerp(Vector3.one * 0.95f, Vector3.one, t);

            if (overlayImage != null)
                overlayImage.color = Color.Lerp(startColor, endColor, t);

            time += Time.deltaTime;
            yield return null;
        }

        if (from != null)
        {
            from.alpha = 0f;
            from.interactable = false;
            from.blocksRaycasts = false;
        }

        to.alpha = 1f;
        to.transform.localScale = Vector3.one;

        // iniciar preguntas automáticamente
        if (questionAnimator != null &&
            to == questionAnimator.GetComponent<CanvasGroup>())
        {
            questionAnimator.IniciarPreguntas();
        }
    }

    void PlayAudio()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    // =====================================================
    // CAMBIO DE ESCENA
    // =====================================================
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}