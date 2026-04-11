using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastrarObjeto : MonoBehaviour, IDragHandler
{
    public bool vector;
    public void OnDrag(PointerEventData eventData)
    {
        if (vector)
        {
            transform.parent.position += (Vector3)eventData.delta;
        }
        else
        {
            transform.position += (Vector3)eventData.delta;
        }
        
    }
}
