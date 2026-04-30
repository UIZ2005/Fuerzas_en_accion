using System.Collections;
using UnityEngine;
using TMPro;

public class Sistema2 : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI texto;

    [Header("Panel a ocultar")]
    public GameObject panelAOcultar;

    [Header("Audio")]
    public AudioSource audioSource;

    [System.Serializable]
    public class Linea
    {
        public AudioClip audio;
        [TextArea]
        public string subtitulo;
    }

    [Header("Diálogo")]
    public Linea[] dialogo;

    private Coroutine rutina;

    public void Ejecutar()
    {
        if (rutina != null)
            StopCoroutine(rutina);

        rutina = StartCoroutine(Secuencia());
    }

    IEnumerator Secuencia()
    {
        // OCULTAR OTRO PANEL
        if (panelAOcultar != null)
        {
            panelAOcultar.SetActive(false);
        }

        for (int i = 0; i < dialogo.Length; i++)
        {
            // MOSTRAR PANEL DE SUBTÍTULOS
            panel.SetActive(true);

            texto.text = dialogo[i].subtitulo;

            if (dialogo[i].audio != null)
            {
                audioSource.clip = dialogo[i].audio;
                audioSource.Play();

                yield return new WaitForSeconds(dialogo[i].audio.length);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }

            texto.text = "";
            panel.SetActive(false);

            yield return new WaitForSeconds(1f);
        }
    }
}