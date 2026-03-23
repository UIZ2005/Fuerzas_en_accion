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
    float lastMouseX;

    // Rotacion de objetos
    public Transform[] objetosARotar;

    // Diccionario para velocidad angular por objeto
    Dictionary<Transform, float> angularVelocities = new Dictionary<Transform, float>();

    // Parámetros físicos
    public float friccion = 0.3f;
    public float fuerza = 500f; // Fuerza con la que se hace el giro

    bool isDragging = false;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        foreach (Transform obj in objetosARotar)
        {
            angularVelocities[obj] = 0f;
        }
    }

    void Update()
    {
        DetectarClick();

        // Actualiza la rotacion
        RotarObjetos();

        // --- CONTROL DEL CURSOR CON TAB ---
        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Movimiento WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = new Vector3(0, yVelocity, 0);

        controller.Move((move * speed + velocity) * Time.deltaTime);

        // Mouse cámara
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    void DetectarClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Rueda") || hit.transform.CompareTag("Pedales"))
                {
                    isDragging = true;

                    // reinicia la referencia del mouse
                    lastMouseX = Input.mousePosition.x;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void RotarObjetos()
    {
        float currentMouseX = Input.mousePosition.x;
        float mouseDelta = currentMouseX - lastMouseX;
        lastMouseX = currentMouseX;

        foreach (Transform obj in objetosARotar)
        {
            float masaActual = 1f;

            if (obj.CompareTag("Pedales"))
                masaActual = 0.8f;
            else if (obj.CompareTag("Rueda"))
                masaActual = 2.5f;

            float angularVelocity = angularVelocities[obj];

            // Aplica fuerza si sostiene el click y arrastra
            if (isDragging && Input.GetMouseButton(0))
            {
                float direccion = Vector3.Dot(playerCamera.right, obj.forward) > 0 ? 1f : -1f;
                float torque = mouseDelta * fuerza * direccion;
                float angularAcceleration = torque / masaActual;
                angularVelocity += angularAcceleration * Time.deltaTime;
            }

            // Fricción simulada
            angularVelocity *= (1 - friccion * Time.deltaTime);

            // Corte de velocidad
            if (Mathf.Abs(angularVelocity) < 0.01f)
            {
                angularVelocity = 0f;
            }

            angularVelocities[obj] = angularVelocity;

            obj.Rotate(0f, 0f, -angularVelocity * Time.deltaTime);
        }
    }
}