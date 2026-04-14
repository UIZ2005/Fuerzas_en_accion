using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip[] audios;
    private AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    public void seleccionAudio(int Indice)
    {
        AudioSource.PlayOneShot(audios[Indice]);
    }
}
