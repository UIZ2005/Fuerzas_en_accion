using System.Collections;
using UnityEngine;
using TMPro;

public class Subs : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI texto;

    [Header("Mensajes (editar desde Unity)")]
    [TextArea]
    public string mensajeActivado;

    [TextArea]
    public string mensajeDesactivado;

    [Header("Animación")]
    public float duracionEntrada = 0.2f;
    public float tiempoVisible = 3f;
    public float duracionSalida = 0.2f;

    private Coroutine rutina;

    // BOTÓN ACTIVAR
    public void ActivarSubtitulos()
    {
        MostrarMensaje(mensajeActivado);
    }

    // BOTÓN DESACTIVAR
    public void DesactivarSubtitulos()
    {
        MostrarMensaje(mensajeDesactivado);
    }

    void MostrarMensaje(string mensaje)
    {
        texto.text = mensaje;

        if (rutina != null)
            StopCoroutine(rutina);

        rutina = StartCoroutine(MostrarPanelAnimado());
    }

    IEnumerator MostrarPanelAnimado()
    {
        panel.SetActive(true);

        panel.transform.localScale = Vector3.zero;

        float t = 0f;

        // Entrada
        while (t < duracionEntrada)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(0, 1, t / duracionEntrada);
            panel.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        panel.transform.localScale = Vector3.one;

        // Visible
        yield return new WaitForSeconds(tiempoVisible);

        // Salida
        t = 0f;

        while (t < duracionSalida)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1, 0, t / duracionSalida);
            panel.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        panel.SetActive(false);
    }
}