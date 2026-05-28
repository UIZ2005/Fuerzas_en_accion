using UnityEngine;

public class BikeController : MonoBehaviour
{
    [Header("Movimiento")]
    public float acceleration = 20f;
    public float maxSpeed = 15f;
    public float rotationSpeed = 80f;
    public float brakeForce = 5f;

    [Header("Referencias")]
    public Transform seatPoint;

    private bool playerNearby = false;
    private bool isMounted = false;

    private GameObject player;
    private CharacterController playerController;

    [Header("Salto")]
    public float jumpForce = 5f;
    public float maxJumpCharge = 2f;

    private float currentJumpCharge = 0f;
    private bool isChargingJump = false;

    public LayerMask groundLayer;
    public float groundCheckDistance = 1.2f;


    private Rigidbody rb;

    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isMounted) return;

        float moveInput = -Input.GetAxis("Vertical");
        float turnInput = -Input.GetAxis("Horizontal");

        // =========================
        // ACELERACIÓN
        // =========================
        if (moveInput > 0)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else if (moveInput < 0)
        {
            currentSpeed -= acceleration * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce * Time.fixedDeltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2f, maxSpeed);

        // =========================
        // MOVIMIENTO FÍSICO
        // =========================
        Vector3 movement = transform.right * currentSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);

        // =========================
        // ROTACIÓN
        // =========================
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            Quaternion turnRotation = Quaternion.Euler(
                0f,
                turnInput * rotationSpeed * Time.fixedDeltaTime,
                0f
            );

            rb.MoveRotation(rb.rotation * turnRotation);
        }
        // =========================
        // CARGAR SALTO
        // =========================
        if (isMounted && IsGrounded())
        {
            // Mantener espacio
            if (Input.GetKey(KeyCode.Space))
            {
                isChargingJump = true;

                currentJumpCharge += Time.deltaTime;

                currentJumpCharge = Mathf.Clamp(
                    currentJumpCharge,
                    0,
                    maxJumpCharge
                );
            }

            // Soltar espacio
            if (Input.GetKeyUp(KeyCode.Space))
            {
                float finalJumpForce =
                    jumpForce * currentJumpCharge;

                rb.AddForce(
                    Vector3.up * finalJumpForce,
                    ForceMode.Impulse
                );

                currentJumpCharge = 0f;
                isChargingJump = false;
            }
        }
    }

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
    }

    void MountBike()
    {
        isMounted = true;

        playerController.enabled = false;

        player.transform.position = seatPoint.position;
        player.transform.parent = transform;
    }

    void DismountBike()
    {
        isMounted = false;

        player.transform.parent = null;

        player.transform.position = transform.position + transform.right * 2f;

        playerController.enabled = true;
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
    bool IsGrounded()
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );
    }
}