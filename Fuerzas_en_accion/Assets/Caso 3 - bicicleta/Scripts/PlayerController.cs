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

    Dictionary<Transform, float> angularVelocities = new Dictionary<Transform, float>();

    public float friccion = 0.3f;
    public float fuerza = 500f;

    bool isDragging = false;
    Transform objetoActivo = null;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // VALIDACIÓN OBJETOS
        if (objetosARotar != null && objetosARotar.Length > 0)
        {
            foreach (Transform obj in objetosARotar)
            {
                if (obj != null)
                    angularVelocities[obj] = 0f;
            }
        }
    }

    void Update()
    {
        DetectarClick();

        RotarObjetos();

        // Cursor
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

        // Movimiento
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

        // Cámara
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
        // SI NO HAY OBJETOS, SALIR
        if (objetosARotar == null || objetosARotar.Length == 0)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Rueda") || hit.transform.CompareTag("Pedales"))
                {
                    isDragging = true;
                    objetoActivo = hit.transform;
                    lastMouseX = Input.mousePosition.x;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            objetoActivo = null;
        }
    }

    void RotarObjetos()
    {
        // SI NO HAY OBJETOS, NO HACER NADA
        if (objetosARotar == null || objetosARotar.Length == 0)
            return;

        float currentMouseX = Input.mousePosition.x;
        float rawMouseDelta = currentMouseX - lastMouseX;
        lastMouseX = currentMouseX;

        float direction = 1f;

        if (objetoActivo != null)
        {
            Vector3 dirToObj = (objetoActivo.position - playerCamera.position).normalized;
            direction = Vector3.Dot(playerCamera.right, dirToObj);
        }

        float mouseDelta = rawMouseDelta * direction;

        foreach (Transform obj in objetosARotar)
        {
            if (obj == null) continue;

            float masaActual = 1f;

            if (obj.CompareTag("Pedales"))
                masaActual = 0.8f;
            else if (obj.CompareTag("Rueda"))
                masaActual = 2.5f;

            float angularVelocity = angularVelocities.ContainsKey(obj) ? angularVelocities[obj] : 0f;

            if (isDragging && Input.GetMouseButton(0))
            {
                float torque = mouseDelta * fuerza;
                float angularAcceleration = torque / masaActual;
                angularVelocity += angularAcceleration * Time.deltaTime;
            }

            angularVelocity *= (1 - friccion * Time.deltaTime);

            if (Mathf.Abs(angularVelocity) < 0.01f)
            {
                angularVelocity = 0f;
            }

            angularVelocities[obj] = angularVelocity;

            obj.Rotate(0f, 0f, -angularVelocity * Time.deltaTime);
        }
    }
}