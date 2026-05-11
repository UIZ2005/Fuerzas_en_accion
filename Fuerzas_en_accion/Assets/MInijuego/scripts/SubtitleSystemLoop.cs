using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitleSystemLoop : MonoBehaviour
{
    [Header("UI")]
    public GameObject subtitlePanel;
    public TextMeshProUGUI subtitleText;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Configuración")]
    public float esperaInicial = 5f;
    public float pausaEntreDialogos = 3f;
    public bool repetirInfinitamente = true;   // Si está activo, el diálogo se repetirá siempre

    private Coroutine currentCoroutine;

    [System.Serializable]
    public class DialogueLine
    {
        public AudioClip audio;
        [TextArea(2, 4)]
        public string subtitle;
    }

    [Header("Diálogo")]
    public DialogueLine[] dialogue;

    void Start()
    {
        currentCoroutine = StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        // Espera inicial antes de comenzar
        yield return new WaitForSeconds(esperaInicial);

        // Repetir indefinidamente o una sola vez según la configuración
        do
        {
            // Recorrer todas las líneas del diálogo
            for (int i = 0; i < dialogue.Length; i++)
            {
                // Validar que exista audio
                if (dialogue[i].audio == null)
                    continue;

                // Activar panel de subtítulos
                if (subtitlePanel != null)
                    subtitlePanel.SetActive(true);

                // Mostrar subtítulo
                if (subtitleText != null)
                    subtitleText.text = dialogue[i].subtitle;

                // Reproducir audio
                if (audioSource != null)
                {
                    audioSource.clip = dialogue[i].audio;
                    audioSource.Play();
                }

                // Esperar hasta que termine el audio
                yield return new WaitForSeconds(dialogue[i].audio.length);

                // Limpiar subtítulo
                if (subtitleText != null)
                    subtitleText.text = "";

                // Ocultar panel
                if (subtitlePanel != null)
                    subtitlePanel.SetActive(false);

                // Pausa entre líneas
                yield return new WaitForSeconds(pausaEntreDialogos);
            }

        } while (repetirInfinitamente);
    }

    public void DetenerSubtitulos()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        if (audioSource != null)
            audioSource.Stop();

        if (subtitleText != null)
            subtitleText.text = "";

        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);
    }

    public void ReiniciarSubtitulos()
    {
        // Detener la secuencia actual
        DetenerSubtitulos();

        // Iniciar nuevamente desde el principio
        currentCoroutine = StartCoroutine(StartSequence());
    }
}