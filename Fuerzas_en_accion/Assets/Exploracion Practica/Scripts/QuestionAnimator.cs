using System.Collections;
using UnityEngine;

public class QuestionAnimator : MonoBehaviour
{
    [Header("Preguntas (paneles internos)")]
    public RectTransform[] preguntas;

    [Header("Animacion")]
    public float entradaDuracion = 0.7f;
    public float salidaDuracion = 0.6f;
    public float distanciaSlide = 200f;

    [Header("Audio")]
    public AudioSource slideAudio;

    private int preguntaActual = -1;
    private bool animando = false;

    void Awake()
    {
        // Todas ocultas al iniciar
        foreach (RectTransform p in preguntas)
        {
            p.gameObject.SetActive(false);
            p.anchoredPosition = new Vector2(distanciaSlide, 0);
        }
    }

    // Se llama cuando aparece PanelPreguntas
    public void IniciarPreguntas()
    {
        MostrarSiguientePregunta();
    }

    // Botón Aplicar
    public void MostrarSiguientePregunta()
    {
        if (!animando)
            StartCoroutine(AnimarCambio());
    }

    IEnumerator AnimarCambio()
    {
        animando = true;

        RectTransform anteriorRect = null;
        CanvasGroup anteriorGroup = null;

        if (preguntaActual >= 0)
        {
            anteriorRect = preguntas[preguntaActual];
            anteriorGroup = anteriorRect.GetComponent<CanvasGroup>();
        }

        preguntaActual++;

        if (preguntaActual >= preguntas.Length)
        {
            animando = false;
            yield break;
        }

        RectTransform nuevaRect = preguntas[preguntaActual];
        CanvasGroup nuevaGroup = nuevaRect.GetComponent<CanvasGroup>();

        nuevaRect.gameObject.SetActive(true);
        nuevaGroup.alpha = 0f;

        if (slideAudio != null)
            slideAudio.Play();

        float t = 0f;

        Vector2 nuevaInicio = new Vector2(distanciaSlide, 0);
        Vector2 nuevaFin = Vector2.zero;

        Vector2 anteriorInicio = Vector2.zero;
        Vector2 anteriorFin = new Vector2(-distanciaSlide, 0);

        while (t < entradaDuracion)
        {
            t += Time.deltaTime;
            float lerp = t / entradaDuracion;

            // NUEVA entra
            nuevaRect.anchoredPosition =
                Vector2.Lerp(nuevaInicio, nuevaFin, lerp);

            nuevaGroup.alpha = lerp;

            // ANTERIOR sale
            if (anteriorRect != null)
            {
                anteriorRect.anchoredPosition =
                    Vector2.Lerp(anteriorInicio, anteriorFin, lerp);

                anteriorGroup.alpha = 1f - lerp;
            }

            yield return null;
        }

        // apagar anterior inmediatamente
        if (anteriorRect != null)
            anteriorRect.gameObject.SetActive(false);

        nuevaRect.anchoredPosition = Vector2.zero;
        nuevaGroup.alpha = 1f;

        animando = false;
    }
}