using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Audio SFX (puerta)")]
    public AudioSource sfxSource;
    public AudioClip abrirBien;
    public AudioClip abrirMal;

    [Header("Audio Voz (diálogo)")]
    public AudioSource voiceSource;
    public AudioClip correctVoice;
    public AudioClip incorrectVoice;

    [Header("Feedback")]
    public TextMeshProUGUI subtitleText;
    public GameObject subtitlePanel;

    [Header("Feedback Visual")]
    public RawImage correctRawImage;
    public RawImage incorrectRawImage;

    [TextArea] public string correctSubtitle;
    [TextArea] public string incorrectSubtitle;

    [Header("Animador preguntas")]
    public QuestionAnimator questionAnimator;

    [Header("Rotaciones")]
    public float badOpenAngle = 20f;
    public float goodOpenAngle = 120f;

    [Header("Duraciones")]
    public float badDuration = 1.2f;
    public float goodDuration = 2f;

    private bool validating = false;

    void ShowFeedbackVisual(bool correct)
{
    // Activar panel primero
    subtitlePanel.SetActive(true);

    // Mostrar imagen correspondiente
    correctRawImage.gameObject.SetActive(correct);
    incorrectRawImage.gameObject.SetActive(!correct);
}

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
            //  Mostrar feedback correcto
            ShowFeedbackVisual(true);

            sfxSource.PlayOneShot(abrirBien);

            voiceSource.Stop();
            voiceSource.clip = correctVoice;
            voiceSource.Play();

            subtitleText.text = correctSubtitle;

            yield return StartCoroutine(OpenDoorGood());
        }
        else
        {
            //  Mostrar feedback incorrecto
            ShowFeedbackVisual(false);

            sfxSource.PlayOneShot(abrirMal);

            voiceSource.Stop();
            voiceSource.clip = incorrectVoice;
            voiceSource.Play();

            subtitleText.text = incorrectSubtitle;

            yield return StartCoroutine(OpenDoorBad());
        }

        yield return new WaitForSeconds(voiceSource.clip.length);

        subtitleText.text = "";

        // Ocultar imágenes
        correctRawImage.gameObject.SetActive(false);
        incorrectRawImage.gameObject.SetActive(false);

        // Ocultar panel
        subtitlePanel.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        questionAnimator.MostrarSiguientePregunta();

        validating = false;
    }

    bool IsCorrectConfiguration()
    {
        int occupiedCount = 0;

        foreach (DropDoor zone in dropDoors)
        {
            if (zone.currentVector != null)
                occupiedCount++;
        }

        if (occupiedCount != 1) return false;
        if (dropDoorCorrecto.currentVector == null) return false;
        if (dropDoorCorrecto.currentVector.transform != vectorCorrecto) return false;

        return true;
    }

    IEnumerator OpenDoorBad()
    {
        Vector3 startRot = puerta.localEulerAngles;
        Vector3 badRot = startRot + new Vector3(0, 0, badOpenAngle);

        float half = badDuration / 2f;
        float time = 0f;

        while (time < half)
        {
            time += Time.deltaTime;
            puerta.localEulerAngles = Vector3.Lerp(startRot, badRot, time / half);
            yield return null;
        }

        time = 0f;

        while (time < half)
        {
            time += Time.deltaTime;
            puerta.localEulerAngles = Vector3.Lerp(badRot, startRot, time / half);
            yield return null;
        }

        puerta.localEulerAngles = startRot;
    }

    IEnumerator OpenDoorGood()
    {
        Vector3 startRot = puerta.localEulerAngles;
        Vector3 endRot = startRot + new Vector3(0, 0, goodOpenAngle);

        float time = 0f;

        while (time < goodDuration)
        {
            time += Time.deltaTime;
            puerta.localEulerAngles = Vector3.Lerp(startRot, endRot, time / goodDuration);
            yield return null;
        }

        puerta.localEulerAngles = endRot;
    }
}