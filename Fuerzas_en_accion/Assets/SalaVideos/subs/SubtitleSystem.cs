using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitleSystem : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public AudioSource audioSource;

    [System.Serializable]
    public class DialogueLine
    {
        public AudioClip audio;
        public string subtitle;
    }

    public DialogueLine[] dialogue;

    void Start()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(5f); // espera inicial

        for (int i = 0; i < dialogue.Length; i++)
        {
            // reproducir audio
            audioSource.clip = dialogue[i].audio;
            audioSource.Play();

            // mostrar subtítulo
            subtitleText.text = dialogue[i].subtitle;

            // esperar a que termine el audio
            yield return new WaitForSeconds(audioSource.clip.length);

            // ocultar subtítulo
            subtitleText.text = "";

            // pausa de 3 segundos
            yield return new WaitForSeconds(3f);
        }
    }
}