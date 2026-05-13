using UnityEngine;

public class MostrarPanelSimple : MonoBehaviour
{
    [Header("Panel a mostrar")]
    public GameObject panel;

    void Start()
    {
        // Asegurar que el panel comience oculto
        if (panel != null)
            panel.SetActive(false);
    }

    void OnMouseDown()
    {
        // Se ejecuta automáticamente cuando se hace clic izquierdo
        // sobre este objeto, siempre que tenga un Collider.
        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("Panel activado: " + panel.name);
        }
    }

    // Método para cerrar el panel desde un botón
    public void CerrarPanel()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}