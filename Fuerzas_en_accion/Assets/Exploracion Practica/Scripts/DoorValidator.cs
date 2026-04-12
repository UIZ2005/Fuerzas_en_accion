using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorValidator : MonoBehaviour
{
    [Header("Vectores")]
    public Transform[] vectores;

    [Header("Vector correcto")]
    public Transform vectorCorrecto;

    [Header("Drop Doors")]
    public DropDoor[] dropDoors;

    [Header("Drop Door correcto")]
    public DropDoor dropDoorCorrecto;

    [Header("Puerta")]
    public Transform puerta;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip abrirBien;
    public AudioClip abrirMal;

    [Header("Animador preguntas")]
    public QuestionAnimator questionAnimator;

    [Header("Rotaciones")]
    public float badOpenAngle = 20f;
    public float goodOpenAngle = 120f;

    [Header("Duraciones")]
    public float badDuration = 1.2f;
    public float goodDuration = 2f;

    private bool validating = false;

    // =====================================================
    // BOTÓN VALIDAR
    // =====================================================
    public void ValidarRespuesta()
    {
        if (validating) return;

        validating = true;
        StartCoroutine(ValidateSequence());
    }

    IEnumerator ValidateSequence()
    {
        bool success = IsCorrectConfiguration();

        if (success)
        {
            audioSource.PlayOneShot(abrirBien);
            yield return StartCoroutine(OpenDoorGood());
        }
        else
        {
            audioSource.PlayOneShot(abrirMal);
            yield return StartCoroutine(OpenDoorBad());
        }

        yield return new WaitForSeconds(0.3f);

        questionAnimator.MostrarSiguientePregunta();

        validating = false;
    }

    // =====================================================
    // VALIDACIÓN CORRECTA
    // =====================================================
    bool IsCorrectConfiguration()
    {
        int occupiedCount = 0;
        SnapController detectedVector = null;

        foreach (DropDoor zone in dropDoors)
        {
            if (zone.currentVector != null)
            {
                occupiedCount++;
                detectedVector = zone.currentVector;
            }
        }

        // Si no hay ninguno colocado  malo
        if (occupiedCount == 0)
            return false;

        // Si hay más de uno  malo
        if (occupiedCount != 1)
            return false;

        // Si zona correcta vacía malo
        if (dropDoorCorrecto.currentVector == null)
            return false;

        // Si vector incorrecto malo
        if (dropDoorCorrecto.currentVector.transform != vectorCorrecto)
            return false;

        return true;
    }

    // =====================================================
    // ANIMACIÓN MALA
    // =====================================================
    IEnumerator OpenDoorBad()
    {
        Vector3 startRot = puerta.localEulerAngles;
        Vector3 badRot = startRot + new Vector3(0, 0, badOpenAngle);

        float half = badDuration / 2f;
        float time = 0f;

        // Abrir poco
        while (time < half)
        {
            time += Time.deltaTime;
            float t = time / half;

            puerta.localEulerAngles =
                Vector3.Lerp(startRot, badRot, t);

            yield return null;
        }

        // Cerrar
        time = 0f;

        while (time < half)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, time / half);

            puerta.localEulerAngles =
                Vector3.Lerp(badRot, startRot, t);

            yield return null;
        }

        puerta.localEulerAngles = startRot;
    }

    // =====================================================
    // ANIMACIÓN BUENA
    // =====================================================
    IEnumerator OpenDoorGood()
    {
        Vector3 startRot = puerta.localEulerAngles;
        Vector3 endRot = startRot + new Vector3(0, 0, goodOpenAngle);

        float time = 0f;

        while (time < goodDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, time / goodDuration);

            puerta.localEulerAngles =
                Vector3.Lerp(startRot, endRot, t);

            yield return null;
        }

        puerta.localEulerAngles = endRot;
    }
}