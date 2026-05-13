using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    private Image img;

    [Header("Colores")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;

    private GameObject[] platos;
    private GameObject[] pinones;

    public bool isPlato=false;
    private bool isSelected = false;

    void Start()
    {
        img = GetComponent<Image>();
        platos = GameObject.FindGameObjectsWithTag("plato");
        pinones = GameObject.FindGameObjectsWithTag("pinon");
        img.color = normalColor;
    }

    // Mouse entra
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            img.color = hoverColor;
        }
    }

    // Mouse sale
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            img.color = normalColor;
        }
    }

    // Click
    public void OnPointerClick(PointerEventData eventData)
    {
        // Deseleccionar todos
        if (isPlato)
        {
            foreach (GameObject plato in platos)
            {
                ImageHover script = plato.GetComponent<ImageHover>();

                script.isSelected = false;
                script.img.color = normalColor;
            }
        }
        else
        {
            foreach (GameObject pinon in pinones)
            {
                ImageHover script = pinon.GetComponent<ImageHover>();

                script.isSelected = false;
                script.img.color = normalColor;
            }
        }

        // Seleccionar este
        isSelected = true;
        img.color = selectedColor;
    }
}