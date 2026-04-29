using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    [Header("Objeto a evaluar")]
    public Transform target;

    [Header("UI")]
    public CanvasGroup panelVectores;
    public CanvasGroup panelVectores2;
    public CanvasGroup panelVectores3;
    public CanvasGroup panelCorrecto;

    [Header("Configuración")]
    public float fadeDuration = 0.5f;
    public float panelVisibleTime = 2f;

    private int currentQuestion = 0;
    private int score = 0;
    private bool isTransitioning = false;

    void Start()
    {
        // Inicializar paneles
        panelVectores.alpha = 1;
        panelVectores2.alpha = 0;
        panelVectores3.alpha = 0;
        panelCorrecto.alpha = 0;
    }

    void Update()
    {
        if (isTransitioning) return;

        if (CheckRotation())
        {
            StartCoroutine(HandleCorrectAnswer());
        }
    }

    // =========================
    // VALIDACIÓN PRINCIPAL
    // =========================
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

    // =========================
    // COMPARACIÓN EXACTA
    // =========================
    bool IsCloseToRotation(Quaternion current, Vector3 targetEuler, float tolerance)
    {
        Quaternion targetRot = Quaternion.Euler(targetEuler);
        float angle = Quaternion.Angle(current, targetRot);
        return angle <= tolerance;
    }

    // =========================
    // PREGUNTA 3 (RANGOS)
    // =========================
    bool CheckRangeRotation(Quaternion current)
    {
        Vector3 rot = NormalizeEuler(current.eulerAngles);

        bool xOk = Mathf.Abs(rot.x - 0) <= 5;
        bool yOk = rot.y >= 150 && rot.y <= 170;
        bool zOk = rot.z >= -40 && rot.z <= -20;

        return xOk && yOk && zOk;
    }

    // =========================
    // NORMALIZAR ÁNGULOS
    // =========================
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

    // =========================
    // MANEJO DE RESPUESTA
    // =========================
    IEnumerator HandleCorrectAnswer()
    {
        isTransitioning = true;
        score += 10;

        // Mostrar panel correcto
        yield return StartCoroutine(Fade(panelCorrecto, 0, 1));
        yield return new WaitForSeconds(panelVisibleTime);
        yield return StartCoroutine(Fade(panelCorrecto, 1, 0));

        // Ocultar panel actual
        yield return StartCoroutine(Fade(GetCurrentPanel(), 1, 0));

        currentQuestion++;

        if (currentQuestion >= 3)
        {
            Debug.Log("Juego terminado. Puntaje: " + score);
            yield break;
        }

        // Mostrar siguiente panel
        yield return StartCoroutine(Fade(GetCurrentPanel(), 0, 1));

        isTransitioning = false;
    }

    // =========================
    // PANEL ACTUAL
    // =========================
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

    // =========================
    // FADE
    // =========================
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