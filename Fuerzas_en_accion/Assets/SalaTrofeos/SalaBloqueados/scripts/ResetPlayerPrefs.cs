using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    void Start()
    {
        // Elimina únicamente las claves de los trofeos
        PlayerPrefs.DeleteKey("TrofeoBronce");
        PlayerPrefs.DeleteKey("TrofeoPlata");
        PlayerPrefs.DeleteKey("TrofeoOro");
        PlayerPrefs.DeleteKey("Medalla");
        PlayerPrefs.DeleteKey("SubsInicial");

        // Guarda los cambios
        PlayerPrefs.Save();

        Debug.Log("Todos los trofeos han sido reiniciados.");
    }
}