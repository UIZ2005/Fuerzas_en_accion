using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    [Header("Movimiento")]
    [Header("Movimiento")]
    public float acceleration = 8f;
    public float maxSpeed = 12f;
    public float brakeForce = 10f;
    public float rotationSpeed = 80f;

    private float currentSpeed = 0f;

    [Header("Referencias")]
    public Transform seatPoint;

    private bool playerNearby = false;
    private bool isMounted = false;

    private GameObject player;
    private CharacterController playerController;

    void Update()
    {
        // Montar / desmontar
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isMounted)
            {
                MountBike();
            }
            else
            {
                DismountBike();
            }
        }

        // Movimiento bicicleta
        if (isMounted)
        {
            float moveInput = Input.GetAxis("Vertical");
            float turn = -Input.GetAxis("Horizontal");

            // ACELERAR
            if (moveInput > 0)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }

            // REVERSA
            else if (moveInput < 0)
            {
                currentSpeed -= acceleration * Time.deltaTime;
            }

            // FRENADO NATURAL
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce * Time.deltaTime);
            }
            // Limitar velocidad
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2f, maxSpeed);

            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up * turn * rotationSpeed * Time.deltaTime);
        }
    }

    void MountBike()
    {
        isMounted = true;

        playerController.enabled = false;

        player.transform.position = seatPoint.position;
        player.transform.parent = transform;

        // Opcional: ocultar jugador
        // player.SetActive(false);
    }

    void DismountBike()
    {
        isMounted = false;

        player.transform.parent = null;

        player.transform.position = transform.position + transform.right * 2f;

        playerController.enabled = true;

        // player.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.gameObject;
            playerController = player.GetComponent<CharacterController>();

            Debug.Log("Presiona E para montar");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
