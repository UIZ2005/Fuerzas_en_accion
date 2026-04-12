using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    [Header("Drop Doors")]
    public DropDoor[] dropDoors;

    [Header("Snap Distance")]
    public float snapDistance = 20f;

    private Vector3 startPosition;

    [HideInInspector]
    public DropDoor currentZone;

    private bool isDragging = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;

        if (currentZone != null)
        {
            currentZone.ClearZone();
            currentZone = null;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        TrySnapToZone();
    }

    void TrySnapToZone()
    {
        DropDoor nearest = null;
        float minDist = Mathf.Infinity;

        foreach (DropDoor zone in dropDoors)
        {
            float dist = Vector3.Distance(
                transform.position,
                zone.transform.position
            );

            if (dist < minDist && dist <= snapDistance)
            {
                minDist = dist;
                nearest = zone;
            }
        }

        if (nearest != null)
        {
            nearest.PlaceVector(this);
        }
    }

    public void ReturnToStart()
    {
        transform.position = startPosition;
        currentZone = null;
    }
}