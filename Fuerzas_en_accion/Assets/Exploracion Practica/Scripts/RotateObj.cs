using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void OnMouseDown()
    {
        isDragging = true;
        lastMousePosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            float rotX = delta.y * rotationSpeed;
            float rotY = -delta.x * rotationSpeed;

            transform.Rotate(Vector3.up, rotY, Space.World);
            transform.Rotate(Vector3.right, rotX, Space.World);

            lastMousePosition = Input.mousePosition;
        }
    }
}
