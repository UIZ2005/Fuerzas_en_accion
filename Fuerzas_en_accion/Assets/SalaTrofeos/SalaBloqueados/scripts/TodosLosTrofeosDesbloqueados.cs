using UnityEngine;

public class MostrarPanelCuandoTodosLosTrofeosEstanDesbloqueados : MonoBehaviour
{
    [Header("Panel a activar")]
    // Este panel se activará cuando los tres trofeos estén desbloqueados
    public GameObject panelObjetivo;

    [Header("Panel a desactivar")]
    // Este panel se ocultará cuando los tres trofeos estén desbloqueados
    public GameObject panelADesactivar;

    [Header("Claves de los trofeos")]
    public string claveBronce = "TrofeoBronce";
    public string clavePlata = "TrofeoPlata";
    public string claveOro = "TrofeoOro";

    [Header("Mostrar solo una vez")]
    // Si está activado, el panel solo se mostrará la primera vez
    // que se detecte que los tres trofeos están desbloqueados.
    public bool mostrarSoloUnaVez = true;

    [Header("Clave de control del panel")]
    // Clave usada para recordar si este panel ya fue mostrado.
    public string clavePanelMostrado = "PanelTodosLosTrofeosMostrado";

    void Start()
    {
        // El panel objetivo inicia oculto
        if (panelObjetivo != null)
            panelObjetivo.SetActive(false);

        VerificarTrofeos();
    }

    public void VerificarTrofeos()
    {
        bool bronceDesbloqueado = PlayerPrefs.GetInt(claveBronce, 0) == 1;
        bool plataDesbloqueado = PlayerPrefs.GetInt(clavePlata, 0) == 1;
        bool oroDesbloqueado = PlayerPrefs.GetInt(claveOro, 0) == 1;

        // Solo continuar si los tres trofeos están desbloqueados
        if (!(bronceDesbloqueado && plataDesbloqueado && oroDesbloqueado))
            return;

        // Si debe mostrarse solo una vez, verificar si ya se mostró
        if (mostrarSoloUnaVez &&
            PlayerPrefs.GetInt(clavePanelMostrado, 0) == 1)
        {
            return;
        }

        // Activar el panel principal
        if (panelObjetivo != null)
            panelObjetivo.SetActive(true);

        // Desactivar el panel asignado
        if (panelADesactivar != null)
            panelADesactivar.SetActive(false);

        // Guardar que ya fue mostrado
        if (mostrarSoloUnaVez)
        {
            PlayerPrefs.SetInt(clavePanelMostrado, 1);
            PlayerPrefs.Save();
        }

        Debug.Log("Todos los trofeos han sido desbloqueados.");
    }

    // Método opcional para reiniciar el estado del panel
    public void ReiniciarPanel()
    {
        PlayerPrefs.DeleteKey(clavePanelMostrado);
        PlayerPrefs.Save();

        Debug.Log("Se reinició el estado del panel: " + clavePanelMostrado);
    }
}