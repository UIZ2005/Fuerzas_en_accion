using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDoor : MonoBehaviour
{
    [HideInInspector]
    public SnapController currentVector;

    [Header("Margen de encaje más flexible")]
    public float snapRadius = 20f;

    public void PlaceVector(SnapController newVector)
    {
        // Si ya hay uno, devolverlo al origen
        if (currentVector != null && currentVector != newVector)
        {
            currentVector.ReturnToStart();
        }

        currentVector = newVector;

        // Encajar al centro exacto
        newVector.transform.position = transform.position;

        // Registrar zona actual
        newVector.currentZone = this;
    }

    public void ClearZone()
    {
        currentVector = null;
    }
}