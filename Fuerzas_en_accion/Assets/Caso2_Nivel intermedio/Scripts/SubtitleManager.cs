using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [Header("UI de Subtítulos")]
    public GameObject panelSubtitulo;
    public TextMeshProUGUI textoSubtitulo;

    private Coroutine currentRoutine;

    public void MostrarSubtitulo(string texto, AudioClip clip)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(MostrarRutina(texto, clip.length));
    }

    public void MostrarSubtitulo(string texto, float duracion)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(MostrarRutina(texto, duracion));
    }

    private IEnumerator MostrarRutina(string texto, float duracion)
    {
        panelSubtitulo.SetActive(true);
        textoSubtitulo.text = texto;

        yield return new WaitForSeconds(duracion);

        panelSubtitulo.SetActive(false);
        textoSubtitulo.text = "";
    }
}