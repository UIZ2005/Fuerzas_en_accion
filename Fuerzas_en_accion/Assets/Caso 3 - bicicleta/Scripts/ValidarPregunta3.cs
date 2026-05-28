using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidarPregunta3 : MonoBehaviour
{
    [Header("Input Field")]
    public InputField inputRespuesta;

    [Header("Botón Continuar")]
    public GameObject botonContinuar;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;
    private barraProgreso progreso;

    public GameObject audiobueno;
    public GameObject audiomalo;

    void Start()
    {
        progreso = FindAnyObjectByType<barraProgreso>();
        if (botonContinuar != null)
        {
            botonContinuar.SetActive(false);
        }
    }
    // Esta función se ejecuta al oprimir Validar
    public void Validar()
    {
        string respuesta = inputRespuesta.text.Trim();

        Image imagenInput = inputRespuesta.GetComponent<Image>();

        if (respuesta == "70")
        {
            audiobueno.SetActive(true);
            //PASA A LA SIGUIENTE
            progreso.Avanzar();
            // Pintar verde
            if (imagenInput != null)
            {
                imagenInput.color = Color.green;
            }

            // Sonido correcto
            if (audioSource != null && sonidoCorrecto != null)
            {
                audioSource.PlayOneShot(sonidoCorrecto);
            }
        }
        else
        {
            // Pintar rojo
            audiomalo.SetActive(true);
            if (imagenInput != null)
            {
                imagenInput.color = Color.red;
            }

            // Sonido incorrecto
            if (audioSource != null && sonidoIncorrecto != null)
            {
                audioSource.PlayOneShot(sonidoIncorrecto);
            }
        }

        if (botonContinuar != null)
        {
            botonContinuar.SetActive(true);
        }
    }


}
