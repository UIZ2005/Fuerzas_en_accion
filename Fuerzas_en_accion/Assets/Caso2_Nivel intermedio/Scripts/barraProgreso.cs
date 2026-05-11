using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraProgreso : MonoBehaviour
{
    public Image barra;

    public GameObject check1;
    public GameObject check2;
    public GameObject check3;

    public GameObject candado;

    private int nivel = 0;

    // CLAVE que se guardará en PlayerPrefs.
    // Cambia este valor en el Inspector según la escena:
    // "TrofeoBronce", "TrofeoPlata" o "TrofeoOro"
    [Header("Clave del trofeo a desbloquear")]
    public string claveTrofeo;

    void Start()
    {
        barra.fillAmount = 0;

        PrepararCheck(check1);
        PrepararCheck(check2);
        PrepararCheck(check3);

        if (candado != null)
            candado.SetActive(true);
    }

    void Update()
    {
        // Solo para pruebas
        if (Input.GetKeyDown(KeyCode.E))
        {
            Avanzar();
        }

        // Reinicia el trofeo actual con la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey(claveTrofeo);
            PlayerPrefs.Save();
            Debug.Log("Trofeo reiniciado: " + claveTrofeo);
        }
    }

    void PrepararCheck(GameObject check)
    {
        if (check == null) return;

        check.SetActive(true);

        RectTransform rt = check.GetComponent<RectTransform>();
        rt.localScale = Vector3.zero;

        CanvasGroup cg = check.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = check.AddComponent<CanvasGroup>();

        cg.alpha = 0;
    }

    public void Avanzar()
    {
        if (nivel >= 4) return;

        nivel++;

        switch (nivel)
        {
            case 1:
                StartCoroutine(AnimarBarra(0.33f));
                StartCoroutine(AnimarCheck(check1));
                break;

            case 2:
                StartCoroutine(AnimarBarra(0.66f));
                StartCoroutine(AnimarCheck(check2));
                break;

            case 3:
                StartCoroutine(AnimarBarra(1f));
                StartCoroutine(AnimarCheck(check3));
                break;

            case 4:
                if (candado != null)
                {
                    StartCoroutine(AnimarDesbloqueo());
                }
                break;
        }
    }

    // Barra animación
    IEnumerator AnimarBarra(float objetivo)
    {
        float tiempo = 0;
        float duracion = 0.4f;
        float inicio = barra.fillAmount;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            barra.fillAmount = Mathf.Lerp(inicio, objetivo, t);
            yield return null;
        }

        barra.fillAmount = objetivo;
    }

    // Check animado
    IEnumerator AnimarCheck(GameObject check)
    {
        if (check == null) yield break;

        RectTransform rt = check.GetComponent<RectTransform>();
        CanvasGroup cg = check.GetComponent<CanvasGroup>();

        float tiempo = 0;
        float duracion = 0.3f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            // Escala con rebote
            float scale = Mathf.Lerp(0f, 1.2f, t);
            if (t > 0.7f)
                scale = Mathf.Lerp(1.2f, 1f, (t - 0.7f) / 0.3f);

            rt.localScale = Vector3.one * scale;
            cg.alpha = t;

            yield return null;
        }

        rt.localScale = Vector3.one;
        cg.alpha = 1;
    }

    // Animación del candado
    IEnumerator AnimarDesbloqueo()
    {
        RectTransform rt = candado.GetComponent<RectTransform>();
        CanvasGroup cg = candado.GetComponent<CanvasGroup>();

        if (cg == null)
            cg = candado.AddComponent<CanvasGroup>();

        float tiempo = 0;
        float duracion = 0.4f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            rt.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            cg.alpha = 1 - t;

            yield return null;
        }

        candado.SetActive(false);

        // Guardar el trofeo correspondiente
        PlayerPrefs.SetInt(claveTrofeo, 1);
        PlayerPrefs.Save();

        Debug.Log("Trofeo desbloqueado: " + claveTrofeo);
    }
}