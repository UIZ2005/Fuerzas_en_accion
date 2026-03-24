using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fisicasBalon : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Fuerzas")]
    public float fuerza = 8f;
    public float alturaImpacto = 0.5f;

    [Header("Debug")]
    public bool mostrarDebug = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.forward * 20f, ForceMode.Impulse);
        }

        // EMPUJE DESDE EL CENTRO (sin torque)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EmpujeCentro();
        }

        // EMPUJE DESDE EL BORDE (genera torque)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EmpujeConTorque();
        }

        // EMPUJE MÁS FUERTE (más aceleración angular)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EmpujeFuerte();
        }

        MostrarDebug();
    }

    // Empuje desde el centro (casi sin rotación)
    void EmpujeCentro()
    {
        rb.AddForce(Vector3.right * fuerza, ForceMode.Impulse);
    }

    //  Empuje desde un punto desplazado, genera torque real
    void EmpujeConTorque()
    {
        Vector3 puntoImpacto = transform.position + Vector3.up * alturaImpacto;

        rb.AddForceAtPosition(Vector3.right * fuerza, puntoImpacto, ForceMode.Impulse);
    }

    // Empuje más fuerte (más torque y velocidad)
    void EmpujeFuerte()
    {
        Vector3 puntoImpacto = transform.position + Vector3.up * alturaImpacto;

        rb.AddForceAtPosition(Vector3.forward * fuerza * 2f, puntoImpacto, ForceMode.Impulse);
    }

    //  Detectar superficie (inclinación)
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            Vector3 normal = hit.normal;

            if (mostrarDebug)
            {
                Debug.DrawRay(transform.position, normal, Color.green);
                Debug.Log("Inclinación superficie: " + Vector3.Angle(normal, Vector3.up));
            }
        }
    }

    
    void MostrarDebug()
    {
        if (!mostrarDebug) return;

        Debug.Log("Velocidad: " + rb.velocity.magnitude);
        Debug.Log("Velocidad Angular: " + rb.angularVelocity.magnitude);
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (mostrarDebug)
        {
            Debug.Log("Colisionó con: " + collision.gameObject.name);
        }
    }
}
