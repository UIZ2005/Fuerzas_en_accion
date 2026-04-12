using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera cam;

    private Color originalColor;
    private Color hoverColor;

    private bool isDragging = false;
    private float zDepth;
    private Vector3 offset;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        originalColor = spriteRenderer.color;
        ColorUtility.TryParseHtmlString("#562AFF", out hoverColor);

        zDepth = cam.WorldToScreenPoint(transform.position).z;
    }

    void Update()
    {
        if (isDragging)
        {
            DragObject();
        }
    }

    void OnMouseEnter()
    {
        spriteRenderer.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (!isDragging)
            spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        isDragging = true;

        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = zDepth;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);

        offset = transform.position - mouseWorld;
    }

    void OnMouseUp()
    {
        isDragging = false;
        spriteRenderer.color = originalColor;
    }

    void DragObject()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = zDepth;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);

        transform.position = mouseWorld + offset;
    }
}