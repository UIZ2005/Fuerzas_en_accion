using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotate : MonoBehaviour
{
    public float sensitivity = 0.3f;
    public float zSensitivity = 0.5f;
    public float smoothSpeed = 10f;

    private Vector2 rotationVelocity;
    private Vector2 currentRotation;
    private float currentZ = 0f;

    private bool isDraggingLeft = false;
    private bool isDraggingRight = false;

    void Update()
    {
        // -------- CLICK IZQUIERDO (X/Y)
        if (Input.GetMouseButtonDown(0))
            isDraggingLeft = true;

        if (Input.GetMouseButtonUp(0))
            isDraggingLeft = false;

        // -------- CLICK DERECHO (Z)
        if (Input.GetMouseButtonDown(1))
            isDraggingRight = true;

        if (Input.GetMouseButtonUp(1))
            isDraggingRight = false;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // -------- ROTACIÓN X/Y
        if (isDraggingLeft)
        {
            rotationVelocity.x += mouseY * sensitivity;
            rotationVelocity.y -= mouseX * sensitivity;
        }

        // -------- ROTACIÓN Z
        if (isDraggingRight)
        {
            currentZ -= mouseX * zSensitivity * 10f;
        }

        // -------- SUAVIZADO
        rotationVelocity = Vector2.Lerp(rotationVelocity, Vector2.zero, Time.deltaTime * 3f);

        currentRotation += rotationVelocity;

        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentZ);
    }
}