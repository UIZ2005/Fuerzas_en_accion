using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public string sphereName;
    public SphereInteraction manager;

    private Renderer rend;
    private Color originalColor;
    private Color hoverColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        if (sphereName == "Rosada")
            ColorUtility.TryParseHtmlString("#FD38B1", out hoverColor);

        else if (sphereName == "Morada")
            ColorUtility.TryParseHtmlString("#AE00FF", out hoverColor);

        else if (sphereName == "Azul")
            ColorUtility.TryParseHtmlString("#0023FF", out hoverColor);
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        manager.SelectSphere(sphereName);
    }
}
