using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Medalla : MonoBehaviour
{
    [Header("Clave de la medalla")]
    // Debe coincidir exactamente con la clave usada en:
    // GuardarMedallaCuandoCanvasSeaVisible
    // Ejemplo: "MedallaCaso1"
    public string claveMedalla = "Medalla";

    [Header("Objetos a controlar")]
    // Objeto que se muestra cuando la medalla está bloqueada
    public GameObject objetoBloqueado;

    // Objeto que se muestra cuando la medalla está desbloqueada
    public GameObject objetoDesbloqueado;

    void Start()
    {
        ActualizarEstado();
    }

    public void ActualizarEstado()
    {
        // Verifica si la medalla fue desbloqueada
        bool desbloqueada = PlayerPrefs.GetInt(claveMedalla, 0) == 1;

        if (desbloqueada)
        {
            // Ocultar objeto bloqueado
            if (objetoBloqueado != null)
                objetoBloqueado.SetActive(false);

            // Mostrar objeto desbloqueado
            if (objetoDesbloqueado != null)
                objetoDesbloqueado.SetActive(true);
        }
        else
        {
            // Mostrar objeto bloqueado
            if (objetoBloqueado != null)
                objetoBloqueado.SetActive(true);

            // Ocultar objeto desbloqueado
            if (objetoDesbloqueado != null)
                objetoDesbloqueado.SetActive(false);
        }
    }

    // Reinicia únicamente esta medalla
    public void ReiniciarMedalla()
    {
        PlayerPrefs.DeleteKey(claveMedalla);
        PlayerPrefs.Save();

        ActualizarEstado();

        Debug.Log("Medalla reiniciada: " + claveMedalla);
    }

    // Reinicia todas las medallas (agrega más claves según necesites)
    public static void ReiniciarTodas()
    {
        PlayerPrefs.DeleteKey("Medalla1");

        PlayerPrefs.Save();

        Debug.Log("Todas las medallas han sido reiniciadas.");
    }
}