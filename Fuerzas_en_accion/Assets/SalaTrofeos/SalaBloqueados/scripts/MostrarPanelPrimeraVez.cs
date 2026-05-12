using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarPanelPrimeraVez : MonoBehaviour
{
    [Header("Panel a mostrar")]
    // Arrastra aquí el panel que quieres mostrar solo la primera vez
    public GameObject panel;

    [Header("Clave de guardado")]
    // Identificador único para esta escena o panel
    // Ejemplo: "TutorialSalaTrofeos"
    public string clavePlayerPrefs;

    void Start()
    {
        // Verifica si el panel ya fue mostrado anteriormente
        bool yaMostrado = PlayerPrefs.GetInt(clavePlayerPrefs, 0) == 1;

        if (!yaMostrado)
        {
            // Mostrar el panel por primera vez
            if (panel != null)
                panel.SetActive(true);

            // Guardar que ya fue mostrado
            PlayerPrefs.SetInt(clavePlayerPrefs, 1);
            PlayerPrefs.Save();

            Debug.Log("Panel mostrado por primera vez: " + clavePlayerPrefs);
        }
        else
        {
            // En entradas posteriores a la escena, mantenerlo oculto
            if (panel != null)
                panel.SetActive(false);
        }
    }

    // Método opcional para reiniciar el estado y volver a mostrar el panel
    public void ReiniciarPanel()
    {
        PlayerPrefs.DeleteKey(clavePlayerPrefs);
        PlayerPrefs.Save();

        Debug.Log("Se reinició el panel: " + clavePlayerPrefs);
    }
}
