using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fisicasBalon : MonoBehaviour
{

    public Rigidbody rb;

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

    public bool agarrado = false;
    public Transform puntoMano;
    public LanzamientoBalon lanzador;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (flechaFuerza != null)
            flechaFuerza.localScale = Vector3.zero;
    }

    void Update()
    {
        // CharacterController interfiere con Rigidbody,
        // forzamos posición manualmente cada frame
        if (agarrado && puntoMano != null)
        {
            transform.position = puntoMano.position;
            transform.rotation = puntoMano.rotation;
        }

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
        if (lanzador != null && lanzador.modoLanzamiento) return;
        if (agarrado) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.CompareTag("Balon"))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                        AgarrarBalon();
                    else
                    {
                        cargando = true;
                        puntoImpacto = hit.point;
                    }
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

        Vector3 r = puntoImpacto - transform.position;
        Vector3 torque = Vector3.Cross(r, direccion * fuerzaActual);

        Debug.Log("Fuerza: " + fuerzaActual);
        Debug.Log("Torque: " + torque);
        Debug.Log("Magnitud torque: " + torque.magnitude);
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

        Debug.DrawRay(puntoImpacto, direccion * escala, Color.green);
    }

    void ResetFuerza()
    {
        fuerzaActual = 0;
        cargando = false;

        if (flechaFuerza != null)
            flechaFuerza.localScale = Vector3.zero;
    }

    void AgarrarBalon()
    {
        agarrado = true;
        lanzador.modoLanzamiento = true;
        lanzador.ResetFuerzaLanzamiento();

        rb.isKinematic = true;
        rb.detectCollisions = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // SetParent ya no es necesario porque forzamos posición en Update,
        // pero lo dejamos para que la jerarquía se vea limpia en el editor
        transform.SetParent(puntoMano);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log("Balón agarrado");
    }
}
