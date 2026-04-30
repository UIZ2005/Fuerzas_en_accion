using UnityEngine;

public class collidersubs : MonoBehaviour
{
    public GameObject panel;

    [Header("Audio")]
    public AudioSource vozSource;
    public AudioClip audioEntrada;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mostrar panel
            panel.SetActive(true);

            //  Reproducir audio
            if (vozSource != null && audioEntrada != null)
            {
                vozSource.clip = audioEntrada;
                vozSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ocultar panel
            panel.SetActive(false);

            //  Detener audio
            if (vozSource != null)
            {
                vozSource.Stop();
            }
        }
    }
}