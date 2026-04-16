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
        
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void enter()
    {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }
    public void exit()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }
}
