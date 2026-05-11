using UnityEngine;

public class RotarTrofeoAlApuntar : MonoBehaviour
{
    [Header("Estado del trofeo")]
    [Tooltip("Si está desactivado, el trofeo no rotará al apuntarlo.")]
    public bool trofeoDesbloqueado = true;

    [Header("Configuración de rotación")]
    [Tooltip("Velocidad de rotación en grados por segundo.")]
    public float velocidadRotacion = 15f;

    [Tooltip("Velocidad con la que vuelve a su rotación original.")]
    public float velocidadRetorno = 5f;

    // Rotación original del trofeo
    private Quaternion rotacionInicial;

    // Indica si el cursor (crosshair) está apuntando al trofeo
    private bool apuntando = false;

    void Start()
    {
        // Guardar la rotación inicial para restaurarla después
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        // Si el trofeo está desbloqueado y el jugador lo está apuntando,
        // rotar continuamente sobre el eje Y
        if (trofeoDesbloqueado && apuntando)
        {
            transform.Rotate(0f, velocidadRotacion * Time.deltaTime, 0f, Space.World);
        }
        else
        {
            // Volver suavemente a la rotación original
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotacionInicial,
                velocidadRetorno * Time.deltaTime
            );
        }
    }

    // Se ejecuta cuando el cursor del mouse entra al collider del objeto.
    // En proyectos con crosshair y raycast, normalmente debes llamar manualmente
    // a los métodos SetApuntando(true/false) desde tu script de interacción.
    void OnMouseEnter()
    {
        apuntando = true;
    }

    void OnMouseExit()
    {
        apuntando = false;
    }

    // Métodos públicos para usar con sistemas de raycast/crosshair
    public void SetApuntando(bool estado)
    {
        apuntando = estado;
    }

    // Permite actualizar el estado leyendo PlayerPrefs si deseas integrarlo
    // con el sistema de trofeos ya implementado.
    public void ActualizarEstadoDesdePlayerPrefs(string claveTrofeo)
    {
        trofeoDesbloqueado = PlayerPrefs.GetInt(claveTrofeo, 0) == 1;
    }
}