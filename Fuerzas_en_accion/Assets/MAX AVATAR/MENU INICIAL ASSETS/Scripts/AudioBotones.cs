using UnityEngine;

public class AudioBotones : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip clickSound;

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickSound);
    }
}