using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPanelAlDesactivar : MonoBehaviour
{
    [Header("Panel que se va a desactivar")]
    public GameObject panelObservado;

    [Header("Panel que se activará automáticamente")]
    public GameObject panelAActivar;

    private bool estabaActivo = false;

    void Start()
    {
        // Guardamos el estado inicial del panel observado
        if (panelObservado != null)
        {
            estabaActivo = panelObservado.activeSelf;
        }
    }

    void Update()
    {
        if (panelObservado == null || panelAActivar == null)
            return;

        // Detecta cuando el panel pasa de activo a inactivo
        if (estabaActivo && !panelObservado.activeSelf)
        {
            panelAActivar.SetActive(true);
        }

        // Actualiza el estado para el siguiente frame
        estabaActivo = panelObservado.activeSelf;
    }
}