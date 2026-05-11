using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValidacionPreguntasCaso2 : MonoBehaviour
{
    [Header("Diagrama")]
    public GameObject diagrama;
    public GameObject diagrama1;
    public GameObject diagrama2;
    public GameObject diagrama3;

    [Header("Pregunta 1")]
    public GameObject pregunta1;
    public TMP_InputField Torque1;
    public TMP_InputField AC1;

    [Header("Pregunta 2")]
    public GameObject pregunta2;
    public TMP_InputField Torque2;

    [Header("Pregunta 3")]
    public GameObject pregunta3;
    public TMP_InputField Balon;
    public TMP_InputField Cilindro;

    [Header("Pregunta 3.2")]
    public GameObject pregunta3_2;
    public TMP_InputField Balon2;
    public TMP_InputField Cilindro2;

    [Header("Pregunta 4")]
    public GameObject pregunta4;
    public TMP_InputField Balon4;
    public TMP_InputField Balon4_1;

    [Header("Pregunta 4.2")]
    public GameObject pregunta4_2;
    public TMP_InputField Balon4_2;

    [Header("Sistema de Subtítulos")]
    public GameObject panelSubtitulos;          // Panel que contiene el subtítulo
    public TextMeshProUGUI textoSubtitulos;     // Texto donde se mostrará el subtítulo

    [Header("Audios de Retroalimentación")]
    public AudioClip audioCorrecto;             // Audio para respuesta correcta
    public AudioClip audioIncorrecto;           // Audio para respuesta incorrecta

    // Textos fijos solicitados
    private string dialogoCorrecto =
        "Correcto muy bien! Hiciste el cálculo adecuadamente.";

    private string dialogoIncorrecto =
        "¿Seguro hiciste bien el cálculo?";

    private AudioManager audio;
    private AudioSource audioSourceSubtitulos;

    void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();

        // Obtener o crear AudioSource para reproducir los audios de subtítulos
        audioSourceSubtitulos = GetComponent<AudioSource>();
        if (audioSourceSubtitulos == null)
        {
            audioSourceSubtitulos = gameObject.AddComponent<AudioSource>();
        }

        // Ocultar panel al iniciar
        if (panelSubtitulos != null)
        {
            panelSubtitulos.SetActive(false);
        }
    }

    // ==========================================================
    // SISTEMA DE SUBTÍTULOS
    // ==========================================================

    private IEnumerator MostrarSubtituloConAudio(
        string texto,
        AudioClip clip,
        System.Action accionAlFinal = null)
    {
        // Mostrar panel
        if (panelSubtitulos != null)
            panelSubtitulos.SetActive(true);

        // Asignar texto
        if (textoSubtitulos != null)
            textoSubtitulos.text = texto;

        // Duración por defecto si no hay audio
        float duracion = 2f;

        // Reproducir audio
        if (clip != null && audioSourceSubtitulos != null)
        {
            audioSourceSubtitulos.clip = clip;
            audioSourceSubtitulos.Play();
            duracion = clip.length;
        }

        // Esperar hasta que termine el audio
        yield return new WaitForSeconds(duracion);

        // Ocultar panel
        if (panelSubtitulos != null)
            panelSubtitulos.SetActive(false);

        // Ejecutar acción posterior (cambiar de pregunta, ocultar paneles, etc.)
        if (accionAlFinal != null)
        {
            accionAlFinal.Invoke();
        }
    }

    private void MostrarRespuestaCorrecta(System.Action accionAlFinal = null)
    {
        // Sonido original del juego
        if (audio != null)
            audio.seleccionAudio(1);

        // Mostrar subtítulo y esperar a que termine el audio
        StartCoroutine(MostrarSubtituloConAudio(
            dialogoCorrecto,
            audioCorrecto,
            accionAlFinal
        ));
    }

    private void MostrarRespuestaIncorrecta()
    {
        // Sonido original del juego
        if (audio != null)
            audio.seleccionAudio(2);

        // Mostrar subtítulo y audio sin cambiar de pantalla
        StartCoroutine(MostrarSubtituloConAudio(
            dialogoIncorrecto,
            audioIncorrecto
        ));
    }

    // ==========================================================
    // PREGUNTA 1
    // ==========================================================

    public void Q1()
    {
        if (Torque1.text == "0.30")
        {
            Torque1.gameObject.GetComponent<Image>().color = Color.green;

            if (AC1.text == "86.6")
            {
                AC1.gameObject.GetComponent<Image>().color = Color.green;

                MostrarRespuestaCorrecta(() =>
                {
                    pregunta1.SetActive(false);
                    diagrama.SetActive(false);
                    diagrama1.SetActive(true);
                });
            }
            else
            {
                AC1.gameObject.GetComponent<Image>().color = Color.red;
                MostrarRespuestaIncorrecta();
            }
        }
        else
        {
            Torque1.gameObject.GetComponent<Image>().color = Color.red;
            MostrarRespuestaIncorrecta();
        }
    }

    // ==========================================================
    // PREGUNTA 2
    // ==========================================================

    public void Q2()
    {
        if (Torque2.text == "0.46")
        {
            Torque2.gameObject.GetComponent<Image>().color = Color.green;

            MostrarRespuestaCorrecta(() =>
            {
                diagrama1.SetActive(false);
                diagrama2.SetActive(true);
                pregunta2.SetActive(false);
            });
        }
        else
        {
            Torque2.gameObject.GetComponent<Image>().color = Color.red;
            MostrarRespuestaIncorrecta();
        }
    }

    // ==========================================================
    // PREGUNTA 3
    // ==========================================================

    public void Q3()
    {
        if (Balon.text == "0.6")
        {
            Balon.gameObject.GetComponent<Image>().color = Color.green;

            if (Cilindro.text == "0.6")
            {
                Cilindro.gameObject.GetComponent<Image>().color = Color.green;

                MostrarRespuestaCorrecta(() =>
                {
                    pregunta3.SetActive(false);
                    pregunta3_2.SetActive(true);
                });
            }
            else
            {
                Cilindro.gameObject.GetComponent<Image>().color = Color.red;
                MostrarRespuestaIncorrecta();
            }
        }
        else
        {
            Balon.gameObject.GetComponent<Image>().color = Color.red;
            MostrarRespuestaIncorrecta();
        }
    }

    // ==========================================================
    // PREGUNTA 3.2
    // ==========================================================

    public void Q3_2()
    {
        if (Balon2.text == "69.4")
        {
            Balon2.gameObject.GetComponent<Image>().color = Color.green;

            if (Cilindro2.text == "104.2")
            {
                Cilindro2.gameObject.GetComponent<Image>().color = Color.green;

                MostrarRespuestaCorrecta(() =>
                {
                    pregunta3_2.SetActive(false);
                    diagrama2.SetActive(false);
                    diagrama3.SetActive(true);
                });
            }
            else
            {
                Cilindro2.gameObject.GetComponent<Image>().color = Color.red;
                MostrarRespuestaIncorrecta();
            }
        }
        else
        {
            Balon2.gameObject.GetComponent<Image>().color = Color.red;
            MostrarRespuestaIncorrecta();
        }
    }

    // ==========================================================
    // PREGUNTA 4
    // ==========================================================

    public void Q4()
    {
        if (Balon4.text == "0")
        {
            Balon4.gameObject.GetComponent<Image>().color = Color.green;

            if (Balon4_1.text == "2.4")
            {
                Balon4_1.gameObject.GetComponent<Image>().color = Color.green;

                MostrarRespuestaCorrecta(() =>
                {
                    pregunta4.SetActive(false);
                    pregunta4_2.SetActive(false);
                });
            }
            else
            {
                Balon4_1.gameObject.GetComponent<Image>().color = Color.red;
                MostrarRespuestaIncorrecta();
            }
        }
        else
        {
            Balon4.gameObject.GetComponent<Image>().color = Color.red;
            MostrarRespuestaIncorrecta();
        }
    }

    // ==========================================================
    // PREGUNTA 4.2
    // ==========================================================

    public void Q4_2()
    {
        if (Balon4_2.text == "693")
        {
            Balon4_2.gameObject.GetComponent<Image>().color = Color.green;

            MostrarRespuestaCorrecta(() =>
            {
                pregunta4_2.SetActive(false);
            });
        }
        else
        {
            Balon4_2.gameObject.GetComponent<Image>().color = Color.red;
            MostrarRespuestaIncorrecta();
        }
    }
}       