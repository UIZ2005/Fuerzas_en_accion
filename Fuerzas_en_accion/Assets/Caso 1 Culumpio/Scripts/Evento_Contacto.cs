using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evento_Contacto : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioManager audio;
    private bool Sono = true;
    private void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Sono)
        {
            audio.seleccionAudio(0);
            Sono = false;
        }
    }
}
