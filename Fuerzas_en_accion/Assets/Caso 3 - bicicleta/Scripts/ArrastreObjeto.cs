using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastreObjeto : MonoBehaviour
{
    [Header("Camara")]
    public Camera cam;

    [Header("Zona destino")]
    public Transform zonaDestino;

    [Header("UI")]
    public GameObject panelActual;
    public GameObject panelSiguiente;

    [Header("Movimiento")]
    public float distanciaCamara = 3f;

    // Distancia para detectar llegada
    public float rangoColocacion = 1.0f;

    private bool arrastrando = false;
    private bool colocado = false;

    private Vector3 offset;

    void OnMouseDown()
    {
        if (colocado) return;

        arrastrando = true;

        Vector3 mouseWorldPos =
            ObtenerPosicionMouse();

        offset = transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        if (colocado) return;

        arrastrando = false;
    }

    void Update()
    {
        // Si ya quedó colocado
        if (colocado) return;

        // Movimiento
        if (arrastrando)
        {
            Vector3 nuevaPos =
                ObtenerPosicionMouse() + offset;

            transform.position = nuevaPos;

            // Verificar distancia a la zona
            float distancia =
                Vector3.Distance(
                    transform.position,
                    zonaDestino.position
                );

            // Apenas entra al rango
            if (distancia <= rangoColocacion)
            {
                ColocarObjeto();
            }
        }
    }

    Vector3 ObtenerPosicionMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = distanciaCamara;

        return cam.ScreenToWorldPoint(mousePos);
    }

    void ColocarObjeto()
    {
        // BLOQUEAR TODO
        colocado = true;
        arrastrando = false;

        // Posición exacta centro
        transform.position = zonaDestino.position;

        // Rotación exacta
        transform.rotation =
            Quaternion.Euler(90f, 90f, 0f);

        // UI
        if (panelActual != null)
        {
            panelActual.SetActive(false);
        }

        if (panelSiguiente != null)
        {
            panelSiguiente.SetActive(true);
        }

        enabled = false;
    }
}