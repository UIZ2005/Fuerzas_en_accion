using System.Collections;
using UnityEngine;
using TMPro;

public class Subs : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI texto;

    private Coroutine rutina;

    //BOTÓN ACTIVAR
    public void ActivarSubtitulos()
    {
        MostrarMensaje("Subtítulos activados");
    }

    //BOTÓN DESACTIVAR
    public void DesactivarSubtitulos()
    {
        MostrarMensaje("Subtítulos desactivados");
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

        //Escala inicial (invisible)
        panel.transform.localScale = Vector3.zero;

        //Animación de entrada (pop)
        float t = 0f;
        float duracionEntrada = 0.2f;

        while (t < duracionEntrada)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(0, 1, t / duracionEntrada);
            panel.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        panel.transform.localScale = Vector3.one;

        // Tiempo visible
        yield return new WaitForSeconds(3f);

        // Animación de salida
        t = 0f;
        float duracionSalida = 0.2f;

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