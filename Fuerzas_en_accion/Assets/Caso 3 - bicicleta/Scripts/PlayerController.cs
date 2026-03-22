using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;

    public Transform playerCamera;

    float xRotation = 0f;
    float yVelocity = 0f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movimiento WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // GRAVEDAD
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f; // mantiene pegado al suelo
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = new Vector3(0, yVelocity, 0);

        controller.Move((move * speed + velocity) * Time.deltaTime);

        // Mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}