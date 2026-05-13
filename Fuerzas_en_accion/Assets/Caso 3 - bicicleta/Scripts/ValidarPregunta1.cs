using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidarPregunta1 : MonoBehaviour
{
    [Header("Input Field")]
    public InputField inputRespuesta;

    [Header("Boton")]
    public GameObject botonValidar;

    [Header("Objeto a Rotar")]
    public Transform objetoARotar;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;

    [Header("Rotacion")]
    public float gradosRotacion = 40f;

    // Duración de la animación
    public float duracionRotacion = 3f;


    public void Validar()
    {
        // Obtener texto escrito
        string respuesta =
            inputRespuesta.text.Trim();

        // Imagen del input
        Image imagenInput =
            inputRespuesta.GetComponent<Image>();

        // RESPUESTA CORRECTA
        if (respuesta == "20")
        {
            // Sonido correcto
            if (sonidoCorrecto != null)
            {
                audioSource.PlayOneShot(sonidoCorrecto);
            }

            // Pintar input verde
            if (imagenInput != null)
            {
                imagenInput.color = Color.green;
            }

            // Rotación suave
            if (objetoARotar != null)
            {
                StartCoroutine(
                    RotacionSuave()
                );
            }

            // Desaparecer botón
            if (botonValidar != null)
            {
                botonValidar.SetActive(false);
            }
        }
        else
        {
            // Sonido incorrecto
            if (sonidoIncorrecto != null)
            {
                audioSource.PlayOneShot(sonidoIncorrecto);
            }

            // Pintar input rojo
            if (imagenInput != null)
            {
                imagenInput.color = Color.red;
            }
        }
    }

    IEnumerator RotacionSuave()
    {
        Quaternion rotacionInicial =
            objetoARotar.rotation;

        Quaternion rotacionFinal =
            rotacionInicial *
            Quaternion.AngleAxis(
                gradosRotacion,
                objetoARotar.forward
            );

        float tiempo = 0f;

        while (tiempo < duracionRotacion)
        {
            tiempo += Time.deltaTime;

            float t =
                Mathf.SmoothStep(
                    0f,
                    1f,
                    tiempo / duracionRotacion
                );

            objetoARotar.rotation =
                Quaternion.Slerp(
                    rotacionInicial,
                    rotacionFinal,
                    t
                );

            yield return null;
        }

        objetoARotar.rotation = rotacionFinal;
    }
}