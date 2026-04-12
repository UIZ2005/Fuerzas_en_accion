using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInteraction : MonoBehaviour
{
    [Header("Esferas")]
    public GameObject esferaRosada;
    public GameObject esferaMorada;
    public GameObject esferaAzul;

    [Header("Pivot de la llave (IMPORTANTE: usa un objeto vacío en la cabeza)")]
    public Transform llave;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip abrirBien;
    public AudioClip abrirMal;

    [Header("Cambio de panel")]
    public QuestionAnimator questionAnimator;

    private bool alreadyClicked = false;

    public void SelectSphere(string colorName)
    {
        if (alreadyClicked) return;
        alreadyClicked = true;

        StartCoroutine(ExecuteAction(colorName));
    }

    IEnumerator ExecuteAction(string colorName)
    {
        // Ocultar otras esferas
        if (colorName != "Rosada") esferaRosada.SetActive(false);
        if (colorName != "Morada") esferaMorada.SetActive(false);
        if (colorName != "Azul") esferaAzul.SetActive(false);

        // Ejecutar animación según esfera
        if (colorName == "Rosada")
        {
            audioSource.PlayOneShot(abrirBien);
            yield return StartCoroutine(RotateSmoothX(120f, 2f));
        }
        else if (colorName == "Morada")
        {
            audioSource.PlayOneShot(abrirMal);
            yield return StartCoroutine(RotateAndReturnX(60f, 1.3f));
        }
        else if (colorName == "Azul")
        {
            audioSource.PlayOneShot(abrirMal);
            yield return StartCoroutine(RotateAndShakeX(20f, 1.5f));
        }

        yield return new WaitForSeconds(0.3f);

        // Pasar al siguiente panel
        questionAnimator.MostrarSiguientePregunta();
    }

    // =====================================================
    // ROSADA → giro completo 120°
    // =====================================================
    IEnumerator RotateSmoothX(float angle, float duration)
    {
        Vector3 startRot = llave.localEulerAngles;
        Vector3 endRot = startRot + new Vector3(-angle, 0, 0);

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, time / duration);

            llave.localEulerAngles = Vector3.Lerp(startRot, endRot, t);

            yield return null;
        }

        llave.localEulerAngles = endRot;
    }

    // =====================================================
    // MORADA → gira 60° y vuelve
    // =====================================================
    IEnumerator RotateAndReturnX(float angle, float duration)
    {
        Vector3 startRot = llave.localEulerAngles;
        Vector3 midRot = startRot + new Vector3(-angle, 0, 0);

        float half = duration / 2f;
        float time = 0f;

        // Ida
        while (time < half)
        {
            time += Time.deltaTime;
            float t = time / half;
            llave.localEulerAngles = Vector3.Lerp(startRot, midRot, t);
            yield return null;
        }

        // Regreso
        time = 0f;
        while (time < half)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, time / half);
            llave.localEulerAngles = Vector3.Lerp(midRot, startRot, t);
            yield return null;
        }

        llave.localEulerAngles = startRot;
    }

    // =====================================================
    // AZUL → gira poco + vibra
    // =====================================================
    IEnumerator RotateAndShakeX(float angle, float duration)
    {
        Vector3 startRot = llave.localEulerAngles;
        Vector3 weakRot = startRot + new Vector3(-angle, 0, 0);

        float time = 0f;

        // Giro pequeño
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            llave.localEulerAngles = Vector3.Lerp(startRot, weakRot, t);
            yield return null;
        }

        // Vibración
        for (int i = 0; i < 5; i++)
        {
            llave.localEulerAngles += new Vector3(2f, 0, 0);
            yield return new WaitForSeconds(0.05f);

            llave.localEulerAngles += new Vector3(-2f, 0, 0);
            yield return new WaitForSeconds(0.05f);
        }

        llave.localEulerAngles = startRot;
    }
}