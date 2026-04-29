using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzamientoBalon : MonoBehaviour
{

    public Transform puntoLanzamiento;
    public float fuerzaFija = 10f;           // fuerza siempre constante
    public float sensibilidadScroll = 30f;   // grados por scroll

    [Range(10f, 80f)]
    public float anguloActual = 35f;         // ángulo inicial en grados

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
        if (!modoLanzamiento) return;
        if (!balonfisicas.agarrado) return;

        // SCROLL controla el ángulo de tiro
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            anguloActual += scroll * sensibilidadScroll;
            anguloActual = Mathf.Clamp(anguloActual, 10f, 80f);
        }

        // Siempre dibuja la trayectoria en modo lanzamiento
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

    // Dirección usando el ángulo actual + dirección horizontal de la cámara
    Vector3 ObtenerDireccion()
    {
        // Horizontal: hacia donde mira la cámara (ignorando inclinación vertical)
        Vector3 horizontal = puntoLanzamiento.forward;
        horizontal.y = 0f;
        horizontal.Normalize();

        // Rotar esa dirección hacia arriba según el ángulo
        Vector3 dir = Quaternion.AngleAxis(-anguloActual, puntoLanzamiento.right)
                      * horizontal;
        return dir.normalized;
    }

    void Lanzar()
    {
        balonfisicas.transform.SetParent(null);

        balonfisicas.rb.detectCollisions = true;
        balonfisicas.rb.isKinematic = false;
        balonfisicas.rb.useGravity = true;
        balonfisicas.rb.velocity = Vector3.zero;
        balonfisicas.rb.angularVelocity = Vector3.zero;

        // Posicionar el balón justo frente a la cámara antes de lanzar
        balonfisicas.transform.position = puntoLanzamiento.position
                                        + puntoLanzamiento.forward * 0.7f;

        balonfisicas.rb.AddForce(ObtenerDireccion() * fuerzaFija, ForceMode.Impulse);

        balonfisicas.agarrado = false;
        modoLanzamiento = false;
        line.positionCount = 0;
    }

    void CancelarLanzamiento()
    {
        balonfisicas.transform.SetParent(null);
        balonfisicas.rb.detectCollisions = true;
        balonfisicas.rb.isKinematic = false;
        balonfisicas.rb.useGravity = true;
        balonfisicas.rb.velocity = Vector3.zero;
        balonfisicas.agarrado = false;
        modoLanzamiento = false;
        line.positionCount = 0;
    }

    void DibujarTrayectoria()
    {
        line.positionCount = puntosTrayectoria;

        Vector3 posicionInicial = balonfisicas.transform.position;
        Vector3 velocidadInicial = ObtenerDireccion() * fuerzaFija;

        for (int i = 0; i < puntosTrayectoria; i++)
        {
            float t = i * tiempoEntrePuntos;
            Vector3 posicion = posicionInicial
                + velocidadInicial * t
                + 0.5f * Physics.gravity * t * t;
            line.SetPosition(i, posicion);
        }
    }

    public void ResetFuerzaLanzamiento()
    {
        anguloActual = 35f;
        DibujarTrayectoria();
    }
}
