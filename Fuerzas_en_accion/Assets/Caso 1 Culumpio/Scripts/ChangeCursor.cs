using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D normalCursor;
    public Texture2D handCursor;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENTER");
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("EXIT");
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }
}
