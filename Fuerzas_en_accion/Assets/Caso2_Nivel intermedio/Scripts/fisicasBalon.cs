using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fisicasBalon : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Fuerza")]
    public float fuerzaMax = 20f;
    public float velocidadCarga = 15f;

    private float fuerzaActual = 0f;
    private bool cargando = false;

    private Vector3 puntoImpacto;

    [Header("Visual")]
    public Transform flechaFuerza;
    public float escalaMax = 3f;

    [Header("Camara FPS")]
    public Camera playerCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (flechaFuerza != null)
            flechaFuerza.localScale = Vector3.zero;
    }

    void Update()
    {
        DetectarClick();

        if (cargando)
        {
            CargarFuerza();
            ActualizarFlecha();
        }

        if (Input.GetMouseButtonUp(0) && cargando)
        {
            AplicarFuerza();
            ResetFuerza();
        }
    }

    void DetectarClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click detectado");

            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.green, 1f);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log("Golpeó: " + hit.collider.name);

                // USAR TAG (IMPORTANTE)
                if (hit.collider.CompareTag("Balon"))
                {
                    Debug.Log(" LE DISTE AL BALÓN");

                    cargando = true;
                    puntoImpacto = hit.point;
                }
            }
        }
    }

    void CargarFuerza()
    {
        fuerzaActual += velocidadCarga * Time.deltaTime;
        fuerzaActual = Mathf.Clamp(fuerzaActual, 0, fuerzaMax);
    }

    void AplicarFuerza()
    {
        Vector3 direccion = (transform.position - puntoImpacto).normalized;

        rb.AddForceAtPosition(direccion * fuerzaActual, puntoImpacto, ForceMode.Impulse);

        // CÁLCULO DE TORQUE
        Vector3 r = puntoImpacto - transform.position;
        Vector3 torque = Vector3.Cross(r, direccion * fuerzaActual);

        Debug.Log("Fuerza: " + fuerzaActual);
        Debug.Log("Torque: " + torque);
        Debug.Log("Magnitud torque: " + torque.magnitude);

        // VECTOR DE GIRO
        Debug.DrawRay(transform.position, torque, Color.red, 2f);
    }

    void ActualizarFlecha()
    {
        if (flechaFuerza == null) return;

        Vector3 direccion = (transform.position - puntoImpacto).normalized;

        float escala = (fuerzaActual / fuerzaMax) * escalaMax;

        flechaFuerza.position = puntoImpacto;
        flechaFuerza.rotation = Quaternion.LookRotation(direccion);
        flechaFuerza.localScale = new Vector3(escala, 0.1f, 0.1f);

        // VECTOR FUERZA
        Debug.DrawRay(puntoImpacto, direccion * escala, Color.green);
    }

    void ResetFuerza()
    {
        fuerzaActual = 0;
        cargando = false;

        if (flechaFuerza != null)
            flechaFuerza.localScale = Vector3.zero;
    }
}
