using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorValidator : MonoBehaviour
{
    [Header("Vectores originales (pregunta actual)")]
    public Transform vectorR;
    public Transform vectorF;

    [Header("Detectores")]
    public Transform detectorVectorR;
    public Transform detectorVectorF;

    [Header("Vectores en canvas retroalimentación")]
    public SpriteRenderer retroVectorR;
    public SpriteRenderer retroVectorF;

    [Header("Distancia válida")]
    public float tolerance = 15f;

    [Header("Animador preguntas")]
    public QuestionAnimator questionAnimator;

    private Vector3 savedPosR;
    private Vector3 savedPosF;

    private bool isCorrectR;
    private bool isCorrectF;

    public void ValidarVectores()
    {
        // Guardar posiciones actuales
        savedPosR = vectorR.position;
        savedPosF = vectorF.position;

        // Validar cercanía
        isCorrectR = Vector3.Distance(vectorR.position, detectorVectorR.position) <= tolerance;
        isCorrectF = Vector3.Distance(vectorF.position, detectorVectorF.position) <= tolerance;

        // Ir siguiente pregunta
        questionAnimator.MostrarSiguientePregunta();

        // Esperar transición y aplicar feedback
        Invoke(nameof(ApplyFeedback), 0.2f);
    }

    void ApplyFeedback()
    {
        // Mantener misma posición en retroalimentación
        retroVectorR.transform.position = new Vector3(
        savedPosR.x + 100f,
        savedPosR.y,
        savedPosR.z
        );

        retroVectorF.transform.position = new Vector3(
            savedPosF.x + 100f,
            savedPosF.y,
            savedPosF.z
        );

        // Pintar colores
        retroVectorR.color = isCorrectR ? Color.green : Color.red;
        retroVectorF.color = isCorrectF ? Color.green : Color.red;
    }
}