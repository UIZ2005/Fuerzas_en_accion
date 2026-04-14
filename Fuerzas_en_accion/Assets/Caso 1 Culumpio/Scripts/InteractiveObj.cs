using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveObj : MonoBehaviour
{
    [Header("Para Los Puntos")]
    public Material material;
    public bool iscorrect;
    private selected scrpit;
    private InteractiveObj[] puntosInteractivos;
    private barraProgreso progreso;
    public TextMeshProUGUI textoPregunta;
    public GameObject buttons;

    [Header("Para los vectores")]
    public bool isVec = false;
    public float time;
    public GameObject DiagramaButton;

    private AudioManager audio;
    
    void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
        scrpit = FindAnyObjectByType<selected>();
        puntosInteractivos = FindObjectsOfType<InteractiveObj>();
        progreso = FindAnyObjectByType<barraProgreso>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void click()
    {
        scrpit.enabled = false;
        GetComponent<MeshRenderer>().material = material;
        if (isVec)
        {
            StartCoroutine(PrecionoVector());
        }
        else
        {
            StartCoroutine(precionoPunto());
        }


    }
    public void cambiarTexto(string text)
    {
        StartCoroutine(changeText(text, time));
    }
    IEnumerator PrecionoVector()
    {
        if (iscorrect)
        {
            textoPregunta.text = "¡Exacto!\r\nCuando la fuerza es perpendicular al brazo de palanca, el torque es máximo";
            audio.seleccionAudio(1);
            //si la respuesta fue correcta

        }
        else
        {
            textoPregunta.text = "Recuerda que el torque depende del seno del ángulo. A 90° se genera el máximo efecto";
            audio.seleccionAudio(2);
            //si la respuesta fue incorrecta
        }
        yield return new WaitForSeconds(2.5f);
        if (iscorrect)
        {
            DiagramaButton.SetActive(true);
            textoPregunta.text = "Ahora, vamos a ver cuáles fuerzas son las que se aplican en un columpio, para eso abre el diagrama de fuerzas";
            progreso.Avanzar();
            foreach (InteractiveObj obj in puntosInteractivos)
            {
                obj.gameObject.transform.parent.gameObject.SetActive(false);
            }
            //si la respuesta fue correcta

        }
        else
        {
            textoPregunta.text = "¿Qué ángulo de fuerza genera el mayor torque?";
            //si la respuesta fue incorrecta
        }
        scrpit.enabled = true;
        yield return null;
    }
    IEnumerator precionoPunto()
    {
        if (iscorrect)
        {
            textoPregunta.text = "¡Correcto!\r\nMientras más lejos del eje que apliques la fuerza, mayor torque y más fácil moverás el columpio";
            audio.seleccionAudio(1);
            //si la respuesta fue correcta

        }
        else
        {
            textoPregunta.text = "No te preocupes.\r\nRecuerda que: la distancia al punto de giro multiplica el efecto de la fuerza";
            audio.seleccionAudio(2);
            //si la respuesta fue incorrecta
        }
        yield return new WaitForSeconds(5);
        if (iscorrect)
        {
            buttons.SetActive(true);
            textoPregunta.text = "Si aplicas la misma fuerza en el asiento pero el columpio tuviera cuerdas más largas, ¿el torque aumentaría, disminuiría o se mantendría igual?";
            progreso.Avanzar();
            foreach (InteractiveObj obj in puntosInteractivos)
            {
                if (!obj.isVec)
                {
                    obj.gameObject.SetActive(false);
                } 
            }
            //si la respuesta fue correcta

        }
        else
        {
            textoPregunta.text = "¿Dónde será más fácil que el columpio se mueva?";
            
            //si la respuesta fue incorrecta
        }
        scrpit.enabled = true;

        yield return null;
    }

    
    IEnumerator changeText(string text, float time)
    {
        yield return new WaitForSeconds(time);
        textoPregunta.text = text;
        yield return null;
    }



}
