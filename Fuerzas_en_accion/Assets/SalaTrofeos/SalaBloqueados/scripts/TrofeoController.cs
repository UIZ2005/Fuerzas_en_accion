using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrofeoController : MonoBehaviour
{
    [Header("Objetos en la Sala de Trofeos")]
    public GameObject objetoADesactivar;
    public GameObject objetoAActivar;

    private const string TROFEO_DESBLOQUEADO = "TrofeoDesbloqueado";

    void Start()
    {
        bool desbloqueado = PlayerPrefs.GetInt(TROFEO_DESBLOQUEADO, 0) == 1;

        if (desbloqueado)
        {
            if (objetoADesactivar != null)
                objetoADesactivar.SetActive(false);

            if (objetoAActivar != null)
                objetoAActivar.SetActive(true);
        }
        else
        {
            // Estado inicial si aún no está desbloqueado
            if (objetoADesactivar != null)
                objetoADesactivar.SetActive(true);

            if (objetoAActivar != null)
                objetoAActivar.SetActive(false);
        }
    }

    // Método opcional para reiniciar el progreso durante pruebas
    public void ReiniciarTrofeo()
    {
        PlayerPrefs.DeleteKey(TROFEO_DESBLOQUEADO);
        PlayerPrefs.Save();
    }
}