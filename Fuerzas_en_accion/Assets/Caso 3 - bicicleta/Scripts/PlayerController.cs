using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2.5f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;

    public Transform playerCamera;

    float xRotation = 0f;
    float yVelocity = 0f;
    float lastMouseX;

    [Header("Objetos")]
    public Transform[] objetosARotar;

    Dictionary<Transform, float> angularVelocities =
        new Dictionary<Transform, float>();

    public float friccion = 0.3f;
    public float fuerza = 500f;

    bool isDragging = false;
    Transform objetoActivo = null;

    CharacterController controller;

    [Header("UI")]
    public bool enUI = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoCadena;

    // Aproximadamente -8 dB
    public float volumenAudio = 0.4f;


    public float velocidadMinimaAudio = 5f;

    bool audioActivo = false;


    bool haciendoFadeOut = false;

    Coroutine fadeCoroutine;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (objetosARotar != null && objetosARotar.Length > 0)
        {
            foreach (Transform obj in objetosARotar)
            {
                if (obj != null)
                    angularVelocities[obj] = 0f;
            }
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volumenAudio;
    }

    void Update()
    {
        DetectarClick();

        RotarObjetos();

        ActualizarEstadoAudio();

        ManejarCursor();

        MovimientoJugador();

        MovimientoCamara();
    }

    void ManejarCursor()
    {
        if (Input.GetKey(KeyCode.Tab) || enUI)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void MovimientoJugador()
    {
        float x = enUI ? 0 : Input.GetAxis("Horizontal");
        float z = enUI ? 0 : Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = new Vector3(0, yVelocity, 0);

        controller.Move((move * speed + velocity) * Time.deltaTime);
    }

    void MovimientoCamara()
    {
        if (!enUI && Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX =
                Input.GetAxis("Mouse X") *
                mouseSensitivity *
                Time.deltaTime;

            float mouseY =
                Input.GetAxis("Mouse Y") *
                mouseSensitivity *
                Time.deltaTime;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.localRotation =
                Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }
    }

    void DetectarClick()
    {
        if (objetosARotar == null || objetosARotar.Length == 0)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray =
                playerCamera.GetComponent<Camera>()
                .ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (
                    hit.transform.CompareTag("Rueda") ||
                    hit.transform.CompareTag("Pedales")
                )
                {
                    isDragging = true;

                    objetoActivo = hit.transform;

                    lastMouseX = Input.mousePosition.x;

                    IniciarAudio();
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
        if (objetosARotar == null || objetosARotar.Length == 0)
            return;

        float currentMouseX = Input.mousePosition.x;

        float rawMouseDelta =
            currentMouseX - lastMouseX;

        lastMouseX = currentMouseX;

        float direction = 1f;

        if (objetoActivo != null)
        {
            Vector3 dirToObj =
                (objetoActivo.position - playerCamera.position)
                .normalized;

            direction =
                Vector3.Dot(playerCamera.right, dirToObj);
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

            float angularVelocity =
                angularVelocities.ContainsKey(obj)
                ? angularVelocities[obj]
                : 0f;

            // Aplicar torque mientras arrastra
            if (isDragging && Input.GetMouseButton(0))
            {
                float torque = mouseDelta * fuerza;

                float angularAcceleration =
                    torque / masaActual;

                angularVelocity +=
                    angularAcceleration * Time.deltaTime;
            }

            angularVelocity *=
                (1 - friccion * Time.deltaTime);

            if (Mathf.Abs(angularVelocity) < 0.01f)
            {
                angularVelocity = 0f;
            }

            angularVelocities[obj] = angularVelocity;

            obj.Rotate(
                0f,
                0f,
                -angularVelocity * Time.deltaTime
            );
        }
    }

    void ActualizarEstadoAudio()
    {
        bool bicicletaEnMovimiento = false;

        foreach (float velocidad in angularVelocities.Values)
        {
            if (Mathf.Abs(velocidad) > velocidadMinimaAudio)
            {
                bicicletaEnMovimiento = true;
                break;
            }
        }


        if (bicicletaEnMovimiento)
        {
            if (!audioActivo && !haciendoFadeOut)
            {
                IniciarAudio();
            }
        }
        else
        {
            
            if (audioActivo && !haciendoFadeOut)
            {
                fadeCoroutine =
                    StartCoroutine(FadeOutAudio(0.15f));
            }
        }
    }

    void IniciarAudio()
    {
        if (sonidoCadena == null) return;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        haciendoFadeOut = false;

        audioSource.clip = sonidoCadena;

        // Variación leve
        audioSource.pitch =
            Random.Range(0.95f, 1.05f);

        audioSource.volume = volumenAudio;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        audioActivo = true;
    }

    IEnumerator FadeOutAudio(float duracion)
    {
        haciendoFadeOut = true;

        float volumenInicial = audioSource.volume;

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;

            audioSource.volume =
                Mathf.Lerp(
                    volumenInicial,
                    0f,
                    tiempo / duracion
                );

            yield return null;
        }

        audioSource.Stop();

        audioSource.volume = volumenAudio;

        audioActivo = false;

        haciendoFadeOut = false;
    }
}