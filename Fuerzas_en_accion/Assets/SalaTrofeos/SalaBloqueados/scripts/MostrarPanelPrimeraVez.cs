using UnityEngine;

public class MostrarPanelPrimeraVez : MonoBehaviour
{
    [Header("Panel a mostrar")]
    public GameObject panel;

    [Header("Clave única")]
    [Tooltip("Ejemplo: TutorialSalaTrofeos")]
    public string clavePlayerPrefs = "TutorialSalaTrofeos";

    [Header("Opciones de prueba")]
    [Tooltip("Si está activado, se eliminará la clave cada vez que se inicie la escena.")]
    public bool reiniciarAlIniciar = false;

    void Awake()
    {
        // Validaciones básicas
        if (panel == null)
        {
            Debug.LogError("No se ha asignado el panel en " + gameObject.name);
            return;
        }

        if (string.IsNullOrWhiteSpace(clavePlayerPrefs))
        {
            Debug.LogError("La clavePlayerPrefs está vacía en " + gameObject.name);
            return;
        }

        // Normalizar la clave
        string clave = clavePlayerPrefs.Trim();

        // Reiniciar automáticamente si se desea (solo para pruebas)
        if (reiniciarAlIniciar)
        {
            PlayerPrefs.DeleteKey(clave);
            PlayerPrefs.Save();

            // En algunas plataformas Unity mantiene valores en memoria;
            // esta llamada fuerza a recargar los PlayerPrefs.
            PlayerPrefs.DeleteKey(clave);
            PlayerPrefs.Save();

            Debug.Log("Clave reiniciada al iniciar: " + clave);
        }

        // IMPORTANTE:
        // HasKey es más confiable para este caso que GetInt(..., 0),
        // porque distingue entre "no existe" y "ya fue creada".
        bool yaMostrado = PlayerPrefs.HasKey(clave);

        Debug.Log("Clave: " + clave + " | Ya mostrada: " + yaMostrado);

        if (!yaMostrado)
        {
            // Primera vez: mostrar el panel
            panel.SetActive(true);

            // Guardar inmediatamente la clave
            PlayerPrefs.SetInt(clave, 1);
            PlayerPrefs.Save();

            Debug.Log("Panel mostrado por primera vez.");
        }
        else
        {
            // Ya se había mostrado antes
            panel.SetActive(false);

            Debug.Log("El panel ya había sido mostrado anteriormente.");
        }
    }

    // Reinicia únicamente esta clave
    [ContextMenu("Reiniciar esta clave")]
    public void ReiniciarPanel()
    {
        if (string.IsNullOrWhiteSpace(clavePlayerPrefs))
            return;

        string clave = clavePlayerPrefs.Trim();

        PlayerPrefs.DeleteKey(clave);
        PlayerPrefs.Save();

        Debug.Log("Clave eliminada manualmente: " + clave);
    }

    // Borra absolutamente todas las PlayerPrefs
    [ContextMenu("Reiniciar todas las PlayerPrefs")]
    public void ReiniciarTodasLasPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("Todas las PlayerPrefs fueron eliminadas.");
    }
}