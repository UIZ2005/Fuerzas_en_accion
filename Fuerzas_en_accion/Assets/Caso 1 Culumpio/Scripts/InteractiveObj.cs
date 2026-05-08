using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveObj : MonoBehaviour
{
    [Header("Para Los Puntos")]
    public Material material;
    public bool iscorrect;
    private selected scrpit;
    private InteractiveObj[] puntosInteractivos;
    private barraProgreso progreso;
    public TextMeshProUGUI textoPregunta;
    public GameObject buttons;

    [Header("Para los vectores")]
    public bool isVec = false;
    public float time;
    public GameObject DiagramaButton;

    private AudioManager audio;

    [Header("Audio de Narración (Subtítulos)")]
    public AudioSource vozSource;

    // AUDIOS ESPECÍFICOS (UNO POR TEXTO)
    [Header("Audios Vector")]
    public AudioClip audioVecCorrecto;
    public AudioClip audioVecIncorrecto;
    public AudioClip audioVecSiguiente;

    [Header("Audios Punto")]
    public AudioClip audioPuntoCorrecto;
    public AudioClip audioPuntoIncorrecto;
    public AudioClip audioPuntoSiguiente;
    public AudioClip audioPuntoPreguntaError;

    [Header("Audios Extra")]

    public AudioClip audioAnguloFuerza;

    void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
        scrpit = FindAnyObjectByType<selected>();
        puntosInteractivos = FindObjectsOfType<InteractiveObj>();
        progreso = FindAnyObjectByType<barraProgreso>();
    }

    public void click()
    {
        scrpit.enabled = false;
        GetComponent<MeshRenderer>().material = material;

        if (isVec)
            StartCoroutine(PrecionoVector());
        else
            StartCoroutine(precionoPunto());
    }

    // MÉTODO TEXTO + AUDIO
    IEnumerator MostrarLinea(string texto, AudioClip audioClip, float fallbackTime)
    {
        textoPregunta.text = texto;

        if (audioClip != null && vozSource != null)
        {
            vozSource.clip = audioClip;
            vozSource.Play();
            yield return new WaitForSeconds(audioClip.length);
        }
        else
        {
            yield return new WaitForSeconds(fallbackTime);
        }
    }

    IEnumerator PrecionoVector()
    {
        if (iscorrect)
        {
            yield return StartCoroutine(MostrarLinea(
                "¡Exacto!\nCuando la fuerza es perpendicular al brazo de palanca, el torque es máximo",
                audioVecCorrecto,
                4f
            ));

            audio.seleccionAudio(1);
        }
        else
        {
            yield return StartCoroutine(MostrarLinea(
                "Recuerda que el torque depende del seno del ángulo. A 90° se genera el máximo efecto",
                audioVecIncorrecto,
                3f
            ));

            audio.seleccionAudio(2);
        }

        if (iscorrect)
        {
            DiagramaButton.SetActive(true);

            yield return StartCoroutine(MostrarLinea(
                "Ahora, vamos a ver cuáles fuerzas son las que se aplican en un columpio, para eso abre el diagrama de fuerzas",
                audioVecSiguiente,
                4f
            ));

            progreso.Avanzar();

            foreach (InteractiveObj obj in puntosInteractivos)
            {
                obj.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            yield return StartCoroutine(MostrarLinea(
                "¿Qué ángulo de fuerza genera el mayor torque?",
                audioAnguloFuerza,
                2f
            ));
        }

        scrpit.enabled = true;
    }

    IEnumerator precionoPunto()
    {
        if (iscorrect)
        {
            yield return StartCoroutine(MostrarLinea(
                "¡Correcto!\nMientras más lejos del eje que apliques la fuerza, mayor torque y más fácil moverás el columpio",
                audioPuntoCorrecto,
                5f
            ));

            audio.seleccionAudio(1);
        }
        else
        {
            yield return StartCoroutine(MostrarLinea(
                "No te preocupes.\nRecuerda que: la distancia al punto de giro multiplica el efecto de la fuerza",
                audioPuntoIncorrecto,
                5f
            ));

            audio.seleccionAudio(2);
        }

        if (iscorrect)
        {
            buttons.SetActive(true);

            yield return StartCoroutine(MostrarLinea(
                "Si aplicas la misma fuerza en el asiento pero el columpio tuviera cuerdas más largas, ¿el torque aumentaría, disminuiría o se mantendría igual?",
                audioPuntoSiguiente,
                5f
            ));

            progreso.Avanzar();

            foreach (InteractiveObj obj in puntosInteractivos)
            {
                if (!obj.isVec)
                {
                    obj.gameObject.SetActive(false);
                };
            }
        }
        else
        {
            textoPregunta.text = "¿Dónde será más fácil que el columpio se mueva?";
        }

        scrpit.enabled = true;
    }
}