using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControladorPreguntas : MonoBehaviour
{
    public GameObject panelPreguntas;
    public Text textoPregunta;

    public Button[] botones;
    public Text[] textosOpciones; // 
    public Image[] imagenesFondo;

    public Color colorDefault = Color.white;
    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;

    public GameObject botonReintentar;

    public AudioSource audioSource;
    public AudioClip acierto;
    public AudioClip error;

    private Pregunta preguntaActual;
    private VideoPlayer videoPlayer;

    private bool preguntaMostrada = false; //

    public void Inicializar(Pregunta p, VideoPlayer vp)
    {
        Debug.Log("Inicializar llamado");

        preguntaActual = p;
        videoPlayer = vp;
        preguntaMostrada = false;
    }

    void Update()
    {
        if (preguntaActual == null || videoPlayer == null) return;

        Debug.Log("Tiempo video: " + videoPlayer.time);
        Debug.Log("Tiempo pregunta: " + preguntaActual.tiempoEnVideo);

        if (!preguntaMostrada && videoPlayer.time >= preguntaActual.tiempoEnVideo)
        {
            MostrarPregunta();
            preguntaMostrada = true;
        }
    }

    void MostrarPregunta()
    {
        videoPlayer.Pause();

        panelPreguntas.SetActive(true);
        textoPregunta.text = preguntaActual.texto;

        for (int i = 0; i < botones.Length; i++)
        {
            if (i < preguntaActual.opciones.Length)
            {
                int index = i;

                botones[i].gameObject.SetActive(true);

                //
                textosOpciones[i].text = preguntaActual.opciones[i];

                botones[i].onClick.RemoveAllListeners();
                botones[i].onClick.AddListener(() => EvaluarRespuesta(index));
            }
            else
            {
                botones[i].gameObject.SetActive(false);
            }
        }
    }

    void EvaluarRespuesta(int seleccion)
    {
        // bloquear todos
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

        // resetear colores
        for (int i = 0; i < imagenesFondo.Length; i++)
        {
            imagenesFondo[i].color = colorDefault;
        }

        // volver a activar botones
        foreach (Button b in botones)
        {
            b.interactable = true;
        }
    }

    IEnumerator ContinuarVideo()
    {
        yield return new WaitForSeconds(1f);

        panelPreguntas.SetActive(false);
        videoPlayer.Play();
    }
}
