using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioEscena : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string sceneName;

    // Método para botones UI
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("No se asignó nombre de escena.");
        }
    }
}