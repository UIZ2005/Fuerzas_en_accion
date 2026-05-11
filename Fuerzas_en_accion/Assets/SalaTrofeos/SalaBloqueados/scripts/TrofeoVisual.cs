using UnityEngine;

public class TrofeoVisual : MonoBehaviour
{
    [Header("Clave del trofeo")]
    // Debe coincidir exactamente con la clave usada en barraProgreso:
    // "TrofeoBronce", "TrofeoPlata" o "TrofeoOro"
    public string claveTrofeo;

    [Header("Objetos a controlar")]
    // Este objeto se desactivará cuando el trofeo esté desbloqueado
    public GameObject objetoADesactivar;

    // Este objeto se activará cuando el trofeo esté desbloqueado
    public GameObject objetoAActivar;

    void Start()
    {
        ActualizarEstado();
    }

    public void ActualizarEstado()
    {
        // Verifica si el trofeo fue desbloqueado
        bool desbloqueado = PlayerPrefs.GetInt(claveTrofeo, 0) == 1;

        if (desbloqueado)
        {
            // Si el trofeo está desbloqueado:
            // desactiva un objeto y activa el otro
            if (objetoADesactivar != null)
                objetoADesactivar.SetActive(false);

            if (objetoAActivar != null)
                objetoAActivar.SetActive(true);
        }
        else
        {
            // Si el trofeo NO está desbloqueado:
            // mantiene el estado contrario
            if (objetoADesactivar != null)
                objetoADesactivar.SetActive(true);

            if (objetoAActivar != null)
                objetoAActivar.SetActive(false);
        }
    }

    // Reinicia únicamente este trofeo
    public void ReiniciarTrofeo()
    {
        PlayerPrefs.DeleteKey(claveTrofeo);
        PlayerPrefs.Save();

        ActualizarEstado();

        Debug.Log("Trofeo reiniciado: " + claveTrofeo);
    }

    // Reinicia todos los trofeos (útil para pruebas)
    public static void ReiniciarTodos()
    {
        PlayerPrefs.DeleteKey("TrofeoBronce");
        PlayerPrefs.DeleteKey("TrofeoPlata");
        PlayerPrefs.DeleteKey("TrofeoOro");
        PlayerPrefs.Save();

        Debug.Log("Todos los trofeos fueron reiniciados.");
    }
}