using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValidacionDiagramaCaso2 : MonoBehaviour
{
    [Header("Objetos del diagrama")]
    public GameObject Fuerza;
    public GameObject Gravedad;
    public GameObject Normal;

    public GameObject VecH1;
    public GameObject VecH2;
    public GameObject VecH3;

    public GameObject[] vectores;
    private barraProgreso progreso;

    [Header("UI")]
    public GameObject diagrama;
    public GameObject pregunta;
    public TMP_InputField input;

    [Header("Validación")]
    private bool goodinput = false;
    public float limitsup = 340;
    public float limitin = 15;
    public string answer = "f";
    public GameObject sistemaPuntos;

    [Header("Audio")]
    private AudioManager audio;

    // ==========================================================
    // SISTEMA DE SUBTÍTULOS
    // ==========================================================

    [Header("Sistema de Subtítulos")]
    public GameObject panelSubtitulos;          // Panel que se mostrará/ocultará
    public TextMeshProUGUI textoSubtitulos;     // Texto donde aparecerá el subtítulo

    [Header("Diálogos Personalizables")]

    // Estos campos aparecerán como cuadros de texto grandes en el Inspector
    // para que puedas escribir libremente el diálogo.
    [TextArea(3, 10)]
    public string dialogoRespuestaCorrecta =
        "¡Muy bien! La respuesta es correcta.";

    [TextArea(3, 10)]
    public string dialogoRespuestaIncorrecta =
        "La respuesta es incorrecta. Inténtalo nuevamente.";

    [Header("Audios de Retroalimentación")]
    public AudioClip audioCorrecto;
    public AudioClip audioIncorrecto;

    private AudioSource audioSourceSubtitulos;

    void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
        progreso = FindAnyObjectByType<barraProgreso>();

        // Obtener o crear AudioSource para los subtítulos
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
    // CORRUTINA PARA MOSTRAR SUBTÍTULOS CON AUDIO
    // ==========================================================

    private IEnumerator MostrarSubtituloConAudio(
        string texto,
        AudioClip clip,
        bool cerrarDiagramaAlFinal)
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

        // Esperar el tiempo del audio
        yield return new WaitForSeconds(duracion);

        // Ocultar panel
        if (panelSubtitulos != null)
            panelSubtitulos.SetActive(false);

        // Si es correcta, cerrar el diagrama solo al finalizar el audio
        if (cerrarDiagramaAlFinal)
        {
            if (diagrama != null)
                diagrama.SetActive(false);

            if (pregunta != null)
                pregunta.SetActive(true);
        }
    }

    // ==========================================================
    // RESPUESTA CORRECTA / INCORRECTA
    // ==========================================================

    private void MostrarRespuestaCorrecta()
    {
        GetComponent<Image>().color = Color.green;
        progreso.Avanzar();

        // Sonido original del juego
        if (audio != null)
            audio.seleccionAudio(1);

        // Mostrar subtítulo usando el texto escrito en el Inspector
        StartCoroutine(MostrarSubtituloConAudio(
            dialogoRespuestaCorrecta,
            audioCorrecto,
            true
        ));
    }

    private void MostrarRespuestaIncorrecta()
    {
        GetComponent<Image>().color = Color.red;

        // Sonido original del juego
        if (audio != null)
            audio.seleccionAudio(2);

        // Mostrar subtítulo usando el texto escrito en el Inspector
        StartCoroutine(MostrarSubtituloConAudio(
            dialogoRespuestaIncorrecta,
            audioIncorrecto,
            false
        ));
    }

    // ==========================================================
    // VALIDACIÓN DEL INPUT
    // ==========================================================

    private bool ValidarInput()
    {
        if (string.IsNullOrEmpty(input.text))
        {
            goodinput = false;
            return false;
        }

        if (input.text.Trim().ToLower() == answer.Trim().ToLower())
        {
            goodinput = true;
            return true;
        }

        goodinput = false;
        return false;
    }

    // ==========================================================
    // VALIDACIÓN 1
    // ==========================================================

    public void validacion()
    {
        if (!ValidarInput())
        {
            MostrarRespuestaIncorrecta();
            return;
        }

        if (Fuerza.transform.position == VecH1.transform.position)
        {
            float z = vectores[0].transform.eulerAngles.z;

            if (z > limitsup || z < limitin)
            {
                if (Normal.transform.position == VecH2.transform.position)
                {
                    z = vectores[1].transform.eulerAngles.z;

                    if (z > limitsup || z < limitin)
                    {
                        if (Gravedad.transform.position == VecH3.transform.position)
                        {
                            z = vectores[2].transform.eulerAngles.z;

                            if ((z > limitsup || z < limitin) && goodinput)
                            {
                                MostrarRespuestaCorrecta();
                                return;
                            }
                        }
                    }
                }
            }
        }

        MostrarRespuestaIncorrecta();
    }

    // ==========================================================
    // VALIDACIÓN 2
    // ==========================================================

    public void validacion2()
    {
        if (!ValidarInput())
        {
            MostrarRespuestaIncorrecta();
            return;
        }

        if (Fuerza.transform.position == VecH1.transform.position)
        {
            float z = vectores[0].transform.eulerAngles.z;

            if (z > 190f || z < 230f)
            {
                if (Normal.transform.position == VecH2.transform.position)
                {
                    z = vectores[1].transform.eulerAngles.z;

                    if (z > 15f || z < 50f)
                    {
                        if (Gravedad.transform.position == VecH3.transform.position)
                        {
                            z = vectores[2].transform.eulerAngles.z;

                            if ((z > limitsup || z < limitin) && goodinput)
                            {
                                MostrarRespuestaCorrecta();
                                return;
                            }
                        }
                    }
                }
            }
        }

        MostrarRespuestaIncorrecta();
    }

    // ==========================================================
    // VALIDACIÓN 3
    // ==========================================================

    public void validacion3()
    {
        if (!ValidarInput())
        {
            MostrarRespuestaIncorrecta();
            return;
        }

        if (Fuerza.transform.position == VecH1.transform.position)
        {
            float z = vectores[0].transform.eulerAngles.z;

            if (z > 160f || z < 200f)
            {
                if (Normal.transform.position == VecH2.transform.position)
                {
                    z = vectores[1].transform.eulerAngles.z;

                    if (z > limitsup || z < limitin)
                    {
                        if (Gravedad.transform.position == VecH3.transform.position)
                        {
                            z = vectores[2].transform.eulerAngles.z;

                            if ((z > limitsup || z < limitin) && goodinput)
                            {
                                sistemaPuntos.SetActive(true);
                                MostrarRespuestaCorrecta();
                                return;
                            }
                        }
                    }
                }
            }
        }

        MostrarRespuestaIncorrecta();
    }
}                                                                                                                                                                                                                                                                       