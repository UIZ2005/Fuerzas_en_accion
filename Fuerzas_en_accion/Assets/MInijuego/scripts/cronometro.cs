using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cronometro : MonoBehaviour
{
    [Header("Tiempo")]
    public float tiempoTotal = 60f;    private float tiempoActual;

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
    public AudioClip sonidoUltimos5s;
    public AudioClip sonidoFinal;

    private bool alertaActivada = false;
    private bool sonido5sActivado = false;
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

        // Activar alerta en 30 segundos
        if (tiempoActual <= 30f && !alertaActivada)
        {
            ActivarAlerta();
        }

        // Sonido en los últimos 5 segundos
        if (tiempoActual <= 5f && !sonido5sActivado)
        {
            sonido5sActivado = true;

            if (audioSource != null && sonidoUltimos5s != null)
            {
                audioSource.clip = sonidoUltimos5s;
                audioSource.loop = false;
                audioSource.Play();
            }
        }

        ActualizarUI();
    }

    void ActualizarUI()
    {
        int minutos = Mathf.FloorToInt(tiempoActual / 60);
        int segundos = Mathf.FloorToInt(tiempoActual % 60);

        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        barraCircular.fillAmount = tiempoActual / tiempoTotal;
    }

    void ActivarAlerta()
    {
        alertaActivada = true;

        barraCircular.color = colorAlerta;
        recuadro.color = colorAlerta;

        if (audioSource != null && sonidoAlerta != null)
        {
            audioSource.PlayOneShot(sonidoAlerta, 0.6f);
        }
    }

    void Finalizar()
    {
        finalizado = true;

        // Detener cualquier audio
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Sonido final
        if (audioSource != null && sonidoFinal != null)
        {
            audioSource.PlayOneShot(sonidoFinal, 0.8f);
        }

        Debug.Log("Tiempo terminado");
    }
}