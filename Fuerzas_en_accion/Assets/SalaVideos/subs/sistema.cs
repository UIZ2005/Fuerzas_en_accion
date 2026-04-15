using System.Collections;
using UnityEngine;
using TMPro;

public class sistema : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public GameObject subtitlePanel;
    public AudioSource audioSource;

    [System.Serializable]
    public class DialogueLine
    {
        public AudioClip audio;
        public string subtitle;
    }

    public DialogueLine[] dialogue;

    public void IniciarSubtitulos()
    {
        StopAllCoroutines();
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()

    {
        Debug.Log("si entro a la funcoin");
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < dialogue.Length; i++)
        {
            subtitlePanel.SetActive(true);
            Debug.Log("se activo el panel");

            audioSource.clip = dialogue[i].audio;
            audioSource.Play();

            subtitleText.text = dialogue[i].subtitle;

            yield return new WaitForSeconds(audioSource.clip.length);

            subtitleText.text = "";
            subtitlePanel.SetActive(false);

            yield return new WaitForSeconds(1f);
        }
    }
}