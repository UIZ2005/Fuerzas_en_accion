using System.Collections;
using System.Collections.Generic;
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
            // Si NO es el seleccionado → desaparecer
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

        // BOTON 3 = verde
        if (btn.gameObject.name == "Boton3")
        {
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
            cb.selectedColor = Color.green;
            cb.pressedColor = Color.green;
            btn.colors = cb;

            // texto NO cambia
        }
        else
        {
            // BOTON 1 y 2 = rojo
            cb.normalColor = Color.red;
            cb.highlightedColor = Color.red;
            cb.selectedColor = Color.red;
            cb.pressedColor = Color.red;
            btn.colors = cb;

            if (tmpText != null)
                tmpText.color = Color.white;
        }
    }
}