using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionController : MonoBehaviour
{
    [Header("Objeto a evaluar")]
    public Transform target;

    [Header("UI")]
    public CanvasGroup panelVectores;
    public CanvasGroup panelVectores2;
    public CanvasGroup panelVectores3;
    public CanvasGroup panelCorrecto;

    [Header("Panel adicional al acertar")]
    public GameObject panelAcierto;

    [Header("Tiempo del panel adicional")]
    public float tiempoPanelAcierto = 7f;

    [Header("Texto Puntaje")]
    public TextMeshProUGUI scoreText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;

    [Header("Controlador Final")]
    public AudioController audioController;

    [Header("Configuración")]
    public float fadeDuration = 0.5f;
    public float panelVisibleTime = 2f;

    private int currentQuestion = 0;
    private int score = 0;
    private bool isTransitioning = false;
    private bool juegoTerminado = false;

    void Start()
    {
        panelVectores.alpha = 1;
        panelVectores2.alpha = 0;
        panelVectores3.alpha = 0;
        panelCorrecto.alpha = 0;
        Time.timeScale = 1f;

        if (panelAcierto != null)
        {
            panelAcierto.SetActive(false);
        }

        UpdateScoreUI();
    }

    void Update()
    {
        if (isTransitioning || juegoTerminado) return;

        if (CheckRotation())
        {
            StartCoroutine(HandleCorrectAnswer());
        }
    }

    bool CheckRotation()
    {
        Quaternion current = target.rotation;

        switch (currentQuestion)
        {
            case 0:
                return IsCloseToRotation(current, new Vector3(180, 80, 0), 10f);

            case 1:
                return IsCloseToRotation(current, new Vector3(0, 50, 0), 5f);

            case 2:
                return CheckRangeRotation(current);
        }

        return false;
    }

    bool IsCloseToRotation(Quaternion current, Vector3 targetEuler, float tolerance)
    {
        Quaternion targetRot = Quaternion.Euler(targetEuler);
        float angle = Quaternion.Angle(current, targetRot);
        return angle <= tolerance;
    }

    bool CheckRangeRotation(Quaternion current)
    {
        Vector3 rot = NormalizeEuler(current.eulerAngles);

        bool xOk = Mathf.Abs(rot.x - 0) <= 20;
        bool yOk = rot.y >= 150 && rot.y <= 170;
        bool zOk = rot.z >= -40 && rot.z <= -20;

        return xOk && yOk && zOk;
    }

    Vector3 NormalizeEuler(Vector3 euler)
    {
        return new Vector3(
            NormalizeAngle(euler.x),
            NormalizeAngle(euler.y),
            NormalizeAngle(euler.z)
        );
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return angle;
    }

    IEnumerator HandleCorrectAnswer()
    {
        isTransitioning = true;

        // SUMAR PUNTOS
        score += 10;
        UpdateScoreUI();

        // SONIDO
        if (audioSource != null && correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }

        // PANEL ACIERTO
        if (panelAcierto != null)
        {
            panelAcierto.SetActive(true);
            StartCoroutine(DesactivarPanelAcierto());
        }

        // PANEL CORRECTO
        yield return StartCoroutine(Fade(panelCorrecto, 0, 1));
        yield return new WaitForSeconds(panelVisibleTime);
        yield return StartCoroutine(Fade(panelCorrecto, 1, 0));

        // OCULTAR PANEL ACTUAL
        yield return StartCoroutine(Fade(GetCurrentPanel(), 1, 0));

        currentQuestion++;

        // TERMINÓ EL JUEGO
        if (currentQuestion >= 3)
        {
            juegoTerminado = true;

            Debug.Log("Juego terminado. Puntaje: " + score);

            // ACTIVAR FINAL INMEDIATAMENTE
            if (audioController != null)
            {
                audioController.MostrarFinal();
            }

            yield break;
        }

        // MOSTRAR SIGUIENTE PANEL
        yield return StartCoroutine(Fade(GetCurrentPanel(), 0, 1));

        isTransitioning = false;
    }

    IEnumerator DesactivarPanelAcierto()
    {
        yield return new WaitForSeconds(tiempoPanelAcierto);

        if (panelAcierto != null)
        {
            panelAcierto.SetActive(false);
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }

    CanvasGroup GetCurrentPanel()
    {
        switch (currentQuestion)
        {
            case 0: return panelVectores;
            case 1: return panelVectores2;
            case 2: return panelVectores3;
        }

        return null;
    }

    IEnumerator Fade(CanvasGroup cg, float start, float end)
    {
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, time / fadeDuration);
            yield return null;
        }

        cg.alpha = end;
    }
}