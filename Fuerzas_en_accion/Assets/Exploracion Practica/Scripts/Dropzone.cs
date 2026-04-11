using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public DraggableUI currentItem;

    public void PlaceItem(DraggableUI newItem)
    {
        if (currentItem != null && currentItem != newItem)
        {
            currentItem.ReturnToInitialZone();
        }

        currentItem = newItem;
        newItem.SnapToZone(this);
    }
}