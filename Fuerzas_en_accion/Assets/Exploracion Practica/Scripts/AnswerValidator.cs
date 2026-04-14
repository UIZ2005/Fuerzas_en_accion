using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerValidator : MonoBehaviour
{


    [Header("Referencia")]
    public DropZone dropZone;
    public QuestionAnimator questionAnimator;

    [Header("Botones del nuevo panel")]
    public Button[] nextPanelButtons;

    private string selectedButtonName;

    // 🔊 FEEDBACK
    public GameObject subtitlePanel;

    [Header("Feedback")]
    public AudioSource audioSource;

    public AudioClip correctAudio;
    public AudioClip incorrectAudio;

    public TextMeshProUGUI subtitleText;

    [TextArea] public string correctSubtitle;
    [TextArea] public string incorrectSubtitle;

    public void ValidarRespuesta()
    {
        if (dropZone.currentItem == null)
            return;

        selectedButtonName = dropZone.currentItem.gameObject.name;

        // Cambiar al siguiente panel
        questionAnimator.MostrarSiguientePregunta();

        Invoke(nameof(ApplyResultToNextPanel), 0.15f);
    }

    void ApplyResultToNextPanel()
    {
        foreach (Button btn in nextPanelButtons)
        {
            if (btn.gameObject.name != selectedButtonName)
            {
                btn.gameObject.SetActive(false);
            }
            else
            {
                btn.gameObject.SetActive(true);
                ApplyButtonStyle(btn);
            }
        }
    }

    void ApplyButtonStyle(Button btn)
    {
        ColorBlock cb = btn.colors;
        TMP_Text tmpText = btn.GetComponentInChildren<TMP_Text>();

        // 🟢 RESPUESTA CORRECTA
        if (btn.gameObject.name == "Boton3")
        {
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
            cb.selectedColor = Color.green;
            cb.pressedColor = Color.green;
            btn.colors = cb;

            StartCoroutine(PlayFeedback(correctAudio, correctSubtitle));
        }
        else // 🔴 RESPUESTA INCORRECTA
        {
            cb.normalColor = Color.red;
            cb.highlightedColor = Color.red;
            cb.selectedColor = Color.red;
            cb.pressedColor = Color.red;
            btn.colors = cb;

            if (tmpText != null)
                tmpText.color = Color.white;

            StartCoroutine(PlayFeedback(incorrectAudio, incorrectSubtitle));
        }
    }

    IEnumerator PlayFeedback(AudioClip clip, string subtitle)
    {
        // Evitar que se encimen audios
        audioSource.Stop();

        // 🟫 Activar panel
        subtitlePanel.SetActive(true);

        // 🧾 Mostrar subtítulo
        subtitleText.text = subtitle;

        // 🎧 Reproducir audio
        audioSource.clip = clip;
        audioSource.Play();

        // ⏳ Esperar a que termine el audio
        yield return new WaitForSeconds(clip.length);

        // 🧾 Ocultar subtítulo
        subtitleText.text = "";

        // 🟫 Ocultar panel
        subtitlePanel.SetActive(false);
    }
}