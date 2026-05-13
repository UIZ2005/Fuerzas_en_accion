using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValiarPregunta2 : MonoBehaviour
{
    [Header("Botones")]
    public Button boton1;

    public Button boton2;

    public Button boton3;

    [Header("Boton Continuar")]
    public GameObject botonContinuar;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip sonidoCorrecto;

    public AudioClip sonidoIncorrecto;

    // Evitar múltiples respuestas
    private bool respondido = false;

    void Start()
    {

        if (botonContinuar != null)
        {
            botonContinuar.SetActive(false);
        }
    }

    // BOTON 1 = CORRECTO
    public void OprimirBoton1()
    {
        if (respondido) return;

        respondido = true;

        // Pintar verde
        PintarBoton(boton1, Color.green);

        // Sonido correcto
        if (sonidoCorrecto != null)
        {
            audioSource.PlayOneShot(sonidoCorrecto);
        }

        // Desactivar otros botones
        if (boton2 != null)
        {
            boton2.gameObject.SetActive(false);
        }

        if (boton3 != null)
        {
            boton3.gameObject.SetActive(false);
        }

        // Mostrar continuar
        ActivarContinuar();
    }

    // BOTON 2 = INCORRECTO
    public void OprimirBoton2()
    {
        if (respondido) return;

        respondido = true;

        // Pintar rojo
        PintarBoton(boton2, Color.red);

        // Sonido incorrecto
        if (sonidoIncorrecto != null)
        {
            audioSource.PlayOneShot(sonidoIncorrecto);
        }

        // Desactivar otros botones
        if (boton1 != null)
        {
            boton1.gameObject.SetActive(false);
        }

        if (boton3 != null)
        {
            boton3.gameObject.SetActive(false);
        }

        // Mostrar continuar
        ActivarContinuar();
    }

    // BOTON 3 = INCORRECTO
    public void OprimirBoton3()
    {
        if (respondido) return;

        respondido = true;

        // Pintar rojo
        PintarBoton(boton3, Color.red);

        // Sonido incorrecto
        if (sonidoIncorrecto != null)
        {
            audioSource.PlayOneShot(sonidoIncorrecto);
        }

        // Desactivar otros botones
        if (boton1 != null)
        {
            boton1.gameObject.SetActive(false);
        }

        if (boton2 != null)
        {
            boton2.gameObject.SetActive(false);
        }

        // Mostrar continuar
        ActivarContinuar();
    }

    void PintarBoton(Button boton, Color color)
    {
        if (boton != null)
        {
            Image imagen =
                boton.GetComponent<Image>();

            if (imagen != null)
            {
                imagen.color = color;
            }
        }
    }

    void ActivarContinuar()
    {
        if (botonContinuar != null)
        {
            botonContinuar.SetActive(true);
        }
    }
}