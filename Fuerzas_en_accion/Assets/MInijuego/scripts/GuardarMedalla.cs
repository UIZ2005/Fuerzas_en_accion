using UnityEngine;

public class GuardarMedallaAlMostrarPanel : MonoBehaviour
{
    [Header("CanvasGroup del panel de medalla")]
    // Asigna aquí el CanvasGroup del panel que indica que la medalla fue ganada
    public CanvasGroup canvasObjetivo;

    [Header("Clave de la medalla")]
    public string claveMedalla = "Medalla";

    [Header("Tolerancia")]
    // Se usa para evitar problemas de precisión con valores float
    public float tolerancia = 0.01f;

    // Para evitar guardar varias veces
    private bool medallaGuardada = false;

    void Start()
    {
        // Si la medalla ya estaba guardada previamente,
        // evitamos volver a guardarla en esta ejecución.
        medallaGuardada = PlayerPrefs.GetInt(claveMedalla, 0) == 1;
    }

    void Update()
    {
        // Si ya se guardó, no seguir verificando
        if (medallaGuardada)
            return;

        // Si el CanvasGroup llegó a alpha = 1, guardar la medalla
        if (canvasObjetivo != null && canvasObjetivo.alpha >= 1f - tolerancia)
        {
            PlayerPrefs.SetInt(claveMedalla, 1);
            PlayerPrefs.Save();

            Debug.Log("Medalla guardada: " + claveMedalla);

            medallaGuardada = true;
        }
    }

    // Método opcional para reiniciar esta medalla
    public void ReiniciarMedalla()
    {
        PlayerPrefs.DeleteKey(claveMedalla);
        PlayerPrefs.Save();

        medallaGuardada = false;

        Debug.Log("Medalla reiniciada: " + claveMedalla);
    }
}