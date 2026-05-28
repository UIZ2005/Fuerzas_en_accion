using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip finalClip;

    [Header("Tiempo")]
    public float tiempoCambio = 58f;
    public float duracionFade = 2f;

    [Header("UI")]
    public CanvasGroup canvasFinal;
    public float duracionFadeCanvas = 1f;

    [Header("Panel a activar al finalizar")]
    public GameObject panelActivar;

    [Header("Velocidad")]
    public float tiempoRestanteParaAcelerar = 30f;
    public float velocidadAcelerada = 1.5f;

    private bool finalMostrado = false;

    void Start()
    {
        canvasFinal.alpha = 0;
        canvasFinal.gameObject.SetActive(true);

        if (panelActivar != null)
        {
            panelActivar.SetActive(false);
        }

        StartCoroutine(ControlAudio());
        StartCoroutine(AcelerarMusica());
        Time.timeScale = 1f;
    }

    IEnumerator ControlAudio()
    {
        yield return new WaitForSeconds(tiempoCambio - duracionFade);

        if (finalMostrado) yield break;

        float volumenInicial = audioSource.volume;
        float t = 0;

        while (t < duracionFade)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volumenInicial, 0, t / duracionFade);
            yield return null;
        }

        audioSource.volume = 0;

        MostrarFinal();
    }

    IEnumerator AcelerarMusica()
    {
        float tiempoParaAcelerar = tiempoCambio - tiempoRestanteParaAcelerar;

        yield return new WaitForSeconds(tiempoParaAcelerar);

        if (!finalMostrado)
        {
            audioSource.pitch = velocidadAcelerada;
        }
    }

    public void MostrarFinal()
    {
        if (finalMostrado) return;

        finalMostrado = true;

        audioSource.Stop();

        if (finalClip != null)
        {
            audioSource.clip = finalClip;
            audioSource.pitch = 1f;
            audioSource.volume = 1f;
            audioSource.Play();
        }

        StartCoroutine(FadeInCanvas());
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

        canvasFinal.alpha = 1;

        if (panelActivar != null)
        {
            panelActivar.SetActive(true);
        }
    }
}