using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuntajeCanasta : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoPuntos;  

    [Header("Sonido")]
    public AudioSource audioSource;     // arrastra un AudioSource aquí
    public AudioClip sonidoCanasta;     // arrastra tu AudioClip aquí

    private int puntos = 0;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Balon")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;


        if (rb.velocity.y >= 0) return;

        puntos++;
        ActualizarUI();
        ReproducirSonido();

        Debug.Log("¡Canasta! Puntos: " + puntos);
    }

    void ActualizarUI()
    {
        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + puntos;
    }

    void ReproducirSonido()
    {
        if (audioSource != null && sonidoCanasta != null)
            audioSource.PlayOneShot(sonidoCanasta);
    }
}
