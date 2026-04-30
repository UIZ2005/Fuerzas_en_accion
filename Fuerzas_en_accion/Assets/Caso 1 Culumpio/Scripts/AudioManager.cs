using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // EFECTOS 
    [Header("Efectos")]
    [SerializeField] private AudioClip[] audios;
    private AudioSource efectosSource;

    // VOCES
    [Header("Voces")]
    [SerializeField] private AudioClip[] voces;
    public AudioSource vozSource;

    void Start()
    {
        efectosSource = GetComponent<AudioSource>();
    }

    // EFECTOS
    public void seleccionAudio(int indice)
    {
        efectosSource.PlayOneShot(audios[indice]);
    }

    // VOZ SIMPLE (por índice)
    public void reproducirVoz(int indice)
    {
        if (vozSource != null && indice >= 0 && indice < voces.Length)
        {
            vozSource.clip = voces[indice];
            vozSource.Play();
        }
    }

    // VOZ CON ESPERA (por si la necesitas en coroutines)
    public IEnumerator reproducirVozCoroutine(int indice)
    {
        if (vozSource != null && indice >= 0 && indice < voces.Length)
        {
            vozSource.clip = voces[indice];
            vozSource.Play();

            yield return new WaitForSeconds(voces[indice].length);
        }
    }

    // DETENER VOZ
    public void detenerVoz()
    {
        if (vozSource != null)
        {
            vozSource.Stop();
        }
    }
}