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
    private ChangeCursor cursor;

    [Header("Detectores")]
    public Animator detectorVectorR;
    public Animator detectorVectorF;
    void Start()
    {
        cursor = GetComponent<ChangeCursor>();
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
        cursor.enter();
    }

    void OnMouseExit()
    {
        if (!isDragging)
            cursor.exit();
            spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        isDragging = true;
        if (detectorVectorF != null)
        {
            detectorVectorF.SetBool("isDrag", true);
            detectorVectorR.SetBool("isDrag", true);
        }
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = zDepth;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);

        offset = transform.position - mouseWorld;
    }

    void OnMouseUp()
    {
        isDragging = false;
        Debug.Log(transform.position);

        if (detectorVectorF != null)
        {
            detectorVectorF.SetBool("isDrag", false);
            detectorVectorR.SetBool("isDrag", false);
        }
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