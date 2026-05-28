using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioEscena : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string sceneName;
    public bool CambioPorContacto=false;

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
    public void Escena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(CambioPorContacto && other.CompareTag("Player"))
        {
            LoadScene();
        }
    }
    public void changeString(string escena)
    {
        sceneName = escena;
    }
    public void cerrar()
    {
        Debug.Log("cerrando Aplicativo");
        Application.Quit();
    }
    public void recargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}