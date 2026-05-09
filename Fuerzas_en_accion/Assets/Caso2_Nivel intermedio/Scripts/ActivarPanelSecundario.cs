using UnityEngine;

public class ActivarPanelSecundario : MonoBehaviour
{
    [Header("Panel que se activa primero")]
    public GameObject panelPrincipal;

    [Header("Panel que quieres activar automáticamente")]
    public GameObject panelSecundario;

    private bool estabaActivo = false;

    void Update()
    {
        // Detecta cuando el panel principal se activa
        if (panelPrincipal != null && panelPrincipal.activeSelf && !estabaActivo)
        {
            estabaActivo = true;

            // Activa el segundo panel
            if (panelSecundario != null)
            {
                panelSecundario.SetActive(true);
            }
        }

        // Reinicia el estado cuando el panel principal se desactiva
        if (panelPrincipal != null && !panelPrincipal.activeSelf)
        {
            estabaActivo = false;
        }
    }
}