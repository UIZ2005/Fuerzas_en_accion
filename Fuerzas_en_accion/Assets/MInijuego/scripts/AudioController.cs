using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip finalClip;

    [Header("Tiempo")]
    public float tiempoCambio = 57f;
    public float duracionFade = 2f;

    [Header("UI")]
    public CanvasGroup canvasFinal;
    public float duracionFadeCanvas = 1f;

    [Header("Panel a activar al finalizar")]
    public GameObject panelActivar;

    [Header("Velocidad")]
    public float tiempoRestanteParaAcelerar = 30f; // cuando falten 30s
    public float velocidadAcelerada = 1.5f;

    void Start()
    {
        canvasFinal.alpha = 0;
        canvasFinal.gameObject.SetActive(true);

        // Asegurar que el panel adicional comience desactivado
        if (panelActivar != null)
        {
            panelActivar.SetActive(false);
        }

        StartCoroutine(ControlAudio());
        StartCoroutine(AcelerarMusica());
    }

    IEnumerator ControlAudio()
    {
        yield return new WaitForSeconds(tiempoCambio - duracionFade);

        float volumenInicial = audioSource.volume;
        float t = 0;

        while (t < duracionFade)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volumenInicial, 0, t / duracionFade);
            yield return null;
        }

        audioSource.volume = 0;

        audioSource.Stop();
        audioSource.clip = finalClip;
        audioSource.pitch = 1f;
        audioSource.volume = 1f;
        audioSource.Play();

        StartCoroutine(FadeInCanvas());
    }

    IEnumerator AcelerarMusica()
    {
        // Espera hasta que falten 30 segundos
        float tiempoParaAcelerar = tiempoCambio - tiempoRestanteParaAcelerar;

        yield return new WaitForSeconds(tiempoParaAcelerar);

        audioSource.pitch = velocidadAcelerada;
    }

    IEnumerator FadeInCanvas()
    {
        float t = 0;

        while (t < duracionFadeCanvas)
        {
            t += Time.deltaTime;
            canvasFinal.alpha = Mathf.Lerp(0, 1, t / duracionFadeCanvas);
            yield return null;
        }

        // Asegurar alpha final
        canvasFinal.alpha = 1;

        // ACTIVAR EL PANEL ASIGNADO
        if (panelActivar != null)
        {
            panelActivar.SetActive(true);
        }
    }
}