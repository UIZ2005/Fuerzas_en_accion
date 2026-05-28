using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControladorPreguntas : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelPreguntas;
    public Text textoPregunta;

    public Button[] botones;
    public Text[] textosOpciones;
    public Image[] imagenesFondo;

    [Header("Colores")]
    public Color colorDefault = Color.white;
    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;

    [Header("Reintentar")]
    public GameObject botonReintentar;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip acierto;
    public AudioClip error;

    [Header("Cursor")]
    public Texture2D cursorMano;
    public Texture2D cursorNormal;

    private Pregunta preguntaActual;
    private VideoPlayer videoPlayer;

    private bool preguntaMostrada = false;

    public void Inicializar(Pregunta p, VideoPlayer vp)
    {
        Debug.Log("Inicializar llamado");

        preguntaActual = p;
        videoPlayer = vp;

        // IMPORTANTE:
        // Solo reiniciar cuando cambia de video
        preguntaMostrada = false;

        // Resetear UI
        panelPreguntas.SetActive(false);
        botonReintentar.SetActive(false);

        // Reactivar botones
        foreach (Button b in botones)
        {
            b.interactable = true;
        }

        // Resetear colores
        for (int i = 0; i < imagenesFondo.Length; i++)
        {
            imagenesFondo[i].color = colorDefault;
        }
    }

    void Update()
    {
        if (preguntaActual == null || videoPlayer == null)
            return;

        Debug.Log("Tiempo video: " + videoPlayer.time);
        Debug.Log("Tiempo pregunta: " + preguntaActual.tiempoEnVideo);

        // Mostrar solo una vez
        if (!preguntaMostrada &&
            !panelPreguntas.activeSelf &&
            videoPlayer.time >= preguntaActual.tiempoEnVideo)
        {
            MostrarPregunta();
            preguntaMostrada = true;
        }
    }

    void CambiarCursorMano()
    {
        Cursor.SetCursor(cursorMano, Vector2.zero, CursorMode.Auto);
    }

    void RestaurarCursor()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }

    void MostrarPregunta()
    {
        if (preguntaActual == null || videoPlayer == null)
            return;

        videoPlayer.Pause();

        panelPreguntas.SetActive(true);
        botonReintentar.SetActive(false);

        textoPregunta.text = preguntaActual.texto;

        // Resetear colores
        for (int i = 0; i < imagenesFondo.Length; i++)
        {
            imagenesFondo[i].color = colorDefault;
        }

        // Configurar botones
        for (int i = 0; i < botones.Length; i++)
        {
            if (i < preguntaActual.opciones.Length)
            {
                int index = i;

                botones[i].gameObject.SetActive(true);
                botones[i].interactable = true;

                textosOpciones[i].text = preguntaActual.opciones[i];

                botones[i].onClick.RemoveAllListeners();
                botones[i].onClick.AddListener(() => EvaluarRespuesta(index));

                // Configurar hover cursor
                EventTrigger trigger = botones[i].GetComponent<EventTrigger>();

                if (trigger == null)
                    trigger = botones[i].gameObject.AddComponent<EventTrigger>();

                trigger.triggers.Clear();

                // Hover entrar
                EventTrigger.Entry entryEnter = new EventTrigger.Entry();
                entryEnter.eventID = EventTriggerType.PointerEnter;
                entryEnter.callback.AddListener((data) => { CambiarCursorMano(); });
                trigger.triggers.Add(entryEnter);

                // Hover salir
                EventTrigger.Entry entryExit = new EventTrigger.Entry();
                entryExit.eventID = EventTriggerType.PointerExit;
                entryExit.callback.AddListener((data) => { RestaurarCursor(); });
                trigger.triggers.Add(entryExit);
            }
            else
            {
                botones[i].gameObject.SetActive(false);
            }
        }
    }

    void EvaluarRespuesta(int seleccion)
    {
        // Bloquear botones
        foreach (Button b in botones)
        {
            b.interactable = false;
        }

        if (seleccion == preguntaActual.respuestaCorrecta)
        {
            audioSource.PlayOneShot(acierto);

            imagenesFondo[seleccion].color = colorCorrecto;

            StartCoroutine(ContinuarVideo());
        }
        else
        {
            audioSource.PlayOneShot(error);

            imagenesFondo[seleccion].color = colorIncorrecto;

            botonReintentar.SetActive(true);
        }
    }

    public void Reintentar()
    {
        botonReintentar.SetActive(false);

        // Resetear colores
        for (int i = 0; i < imagenesFondo.Length; i++)
        {
            imagenesFondo[i].color = colorDefault;
        }

        // Reactivar botones
        foreach (Button b in botones)
        {
            b.interactable = true;
        }
    }

    IEnumerator ContinuarVideo()
    {
        yield return new WaitForSeconds(1f);

        // Ya fue respondida
        preguntaMostrada = true;

        // Resetear colores
        for (int i = 0; i < imagenesFondo.Length; i++)
        {
            imagenesFondo[i].color = colorDefault;
        }

        // Reactivar botones
        foreach (Button b in botones)
        {
            b.interactable = true;
        }

        botonReintentar.SetActive(false);

        panelPreguntas.SetActive(false);

        videoPlayer.Play();
    }
}