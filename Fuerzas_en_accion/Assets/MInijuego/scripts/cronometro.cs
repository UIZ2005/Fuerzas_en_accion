using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cronometro : MonoBehaviour
{
    [Header("Tiempo")]
    public float tiempoTotal = 180f; // 3 minutos
    private float tiempoActual;

    [Header("UI")]
    public Image barraCircular;
    public Text textoTiempo;
    public Image recuadro;

    [Header("Colores")]
    public Color colorNormal = Color.white;
    public Color colorAlerta = new Color(0.75f, 0.22f, 0.17f); // #C0392B

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoAlerta;
    public AudioClip sonidoFinal;

    private bool alertaActivada = false;
    private bool finalizado = false;

    void Start()
    {
        tiempoActual = tiempoTotal;
        ActualizarUI();
    }

    void Update()
    {
        if (finalizado) return;

        tiempoActual -= Time.deltaTime;

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            Finalizar();
        }

        //  Activar alerta en 30 segundos
        if (tiempoActual <= 30f && !alertaActivada)
        {
            ActivarAlerta();
        }

        ActualizarUI();
    }

    void ActualizarUI()
    {
        // Texto mm:ss
        int minutos = Mathf.FloorToInt(tiempoActual / 60);
        int segundos = Mathf.FloorToInt(tiempoActual % 60);

        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // Barra circular (0 a 1)
        barraCircular.fillAmount = tiempoActual / tiempoTotal;
    }

    void ActivarAlerta()
    {
        alertaActivada = true;

        // Cambiar color
        
        barraCircular.color = colorAlerta;
        recuadro.color = colorAlerta;

        // Sonido alerta
        if (audioSource != null && sonidoAlerta != null)
        {
            audioSource.PlayOneShot(sonidoAlerta, 0.6f);
        }
    }

    void Finalizar()
    {
        finalizado = true;

        // sonido final
        if (audioSource != null && sonidoFinal != null)
        {
            audioSource.PlayOneShot(sonidoFinal, 0.8f);
        }

        Debug.Log("Tiempo terminado");
    }
}
