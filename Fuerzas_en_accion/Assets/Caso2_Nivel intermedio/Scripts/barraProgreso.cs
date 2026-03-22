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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Avanzar();
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
                //  quitar candado
                if (candado != null)
                {
                    StartCoroutine(AnimarDesbloqueo());
                }
                break;

        }
    }

    //  Barra animacion
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

    //  Check animado 
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

            // escala con rebote
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
    }
}
