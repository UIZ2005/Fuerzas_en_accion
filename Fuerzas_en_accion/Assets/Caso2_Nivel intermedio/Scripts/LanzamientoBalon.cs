using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzamientoBalon : MonoBehaviour
{
    public Transform puntoLanzamiento;
    public float fuerzaFija = 10f;
    public float sensibilidadScroll = 30f;

    [Range(10f, 80f)]
    public float anguloActual = 35f;

    private bool cargando = false;
    private LineRenderer line;

    public int puntosTrayectoria = 30;
    public float tiempoEntrePuntos = 0.1f;

    public fisicasBalon balonfisicas;
    public bool modoLanzamiento = false;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
    }

    void Update()
    {
        //  FIX 1: evitar error cuando el balón fue destruido
        if (balonfisicas == null) return;

        if (!modoLanzamiento) return;
        if (!balonfisicas.agarrado) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            anguloActual += scroll * sensibilidadScroll;
            anguloActual = Mathf.Clamp(anguloActual, 10f, 80f);
        }

        DibujarTrayectoria();

        if (Input.GetMouseButtonDown(0))
            cargando = true;

        if (Input.GetMouseButtonUp(0) && cargando)
        {
            Lanzar();
            cargando = false;
        }

        if (Input.GetMouseButtonDown(1))
            CancelarLanzamiento();
    }

    Vector3 ObtenerDireccion()
    {
        Vector3 horizontal = puntoLanzamiento.forward;
        horizontal.y = 0f;
        horizontal.Normalize();

        Vector3 dir = Quaternion.AngleAxis(-anguloActual, puntoLanzamiento.right)
                      * horizontal;

        return dir.normalized;
    }

    void Lanzar()
    {
        if (balonfisicas == null) return;

        balonfisicas.transform.SetParent(null);

        balonfisicas.rb.detectCollisions = true;
        balonfisicas.rb.isKinematic = false;
        balonfisicas.rb.useGravity = true;

        balonfisicas.rb.velocity = Vector3.zero;
        balonfisicas.rb.angularVelocity = Vector3.zero;

        balonfisicas.transform.position =
            puntoLanzamiento.position + puntoLanzamiento.forward * 0.7f;

        balonfisicas.rb.AddForce(
            ObtenerDireccion() * fuerzaFija,
            ForceMode.Impulse
        );

        balonfisicas.agarrado = false;
        modoLanzamiento = false;

        line.positionCount = 0;
    }

    void CancelarLanzamiento()
    {
        if (balonfisicas == null) return;

        balonfisicas.transform.SetParent(null);

        balonfisicas.rb.detectCollisions = true;
        balonfisicas.rb.isKinematic = false;
        balonfisicas.rb.useGravity = true;

        balonfisicas.rb.velocity = Vector3.zero;
        balonfisicas.rb.angularVelocity = Vector3.zero;

        balonfisicas.agarrado = false;
        modoLanzamiento = false;

        line.positionCount = 0;
    }

    void DibujarTrayectoria()
    {
        // FIX 2: evitar uso de balón destruido
        if (balonfisicas == null) return;

        line.positionCount = puntosTrayectoria;

        Vector3 posicionInicial = balonfisicas.transform.position;
        Vector3 velocidadInicial = ObtenerDireccion() * fuerzaFija;

        for (int i = 0; i < puntosTrayectoria; i++)
        {
            float t = i * tiempoEntrePuntos;

            Vector3 posicion =
                posicionInicial +
                velocidadInicial * t +
                0.5f * Physics.gravity * t * t;

            line.SetPosition(i, posicion);
        }
    }

    public void ResetFuerzaLanzamiento()
    {
        anguloActual = 35f;

        // FIX 3: evitar error si el balón ya no existe
        if (balonfisicas == null) return;

        DibujarTrayectoria();
    }
}