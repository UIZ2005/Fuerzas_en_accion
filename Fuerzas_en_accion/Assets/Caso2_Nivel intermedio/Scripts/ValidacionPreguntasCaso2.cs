using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValidacionPreguntasCaso2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("diagrama")]
    public GameObject botondiagrama;

    [Header("Pregunta 1")]
    public GameObject pregunta1;
    public TMP_InputField Torque1;
    public TMP_InputField AC1;

    [Header("Pregunta 2")]
    public GameObject pregunta2;
    public TMP_InputField Torque2;

    [Header("Pregunta 3")]
    public GameObject pregunta3;
    public TMP_InputField Balon;
    public TMP_InputField Cilindro;

    [Header("Pregunta 3.2")]
    public GameObject pregunta3_2;
    public TMP_InputField Balon2;
    public TMP_InputField Cilindro2;

    [Header("Pregunta 4")]
    public GameObject pregunta4;
    public TMP_InputField Balon4;
    public TMP_InputField Balon4_1;


    [Header("Pregunta 4.2")]
    public GameObject pregunta4_2;
    public TMP_InputField Balon4_2;

    private AudioManager audio;
    void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
    }

    public void Q1()
    {
        if (Torque1.text == "0.30")
        {
            Torque1.gameObject.GetComponent<Image>().color = Color.green;
            if (AC1.text == "86.6")
            {
                //todo esta bien, pasa a la siguiente pregunta
                AC1.gameObject.GetComponent<Image>().color = Color.green;
                audio.seleccionAudio(1);
                pregunta1.SetActive(false);
                botondiagrama.SetActive(true);
            }
            else
            {
                //La aceleracion angular esta mala
                AC1.gameObject.GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
            }
        }
        else
        {
            //El torque esta malo
            Torque1.gameObject.GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }
    public void Q2()
    {
        if (Torque2.text == "0.46")
        {
            //todo esta bien, pasa a la siguiente pregunta
            audio.seleccionAudio(1);
            botondiagrama.SetActive(true);
            pregunta2.SetActive(false);
        }
        else
        {
            //El torque esta malo

            Torque2.gameObject.GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }
    public void Q3()
    {
        if (Balon.text == "0.6")
        {
            Balon.gameObject.GetComponent<Image>().color = Color.green;
            //Balon esta bien
            if (Cilindro.text == "0.6")
            {
                audio.seleccionAudio(1);
                pregunta3.SetActive(false);
                pregunta3_2.SetActive(false);
            }
            else
            {
                Cilindro.gameObject.GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
            }
        }
        else
        {
            //El torque esta malo

            Balon.gameObject.GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }
    public void Q3_2()
    {
        if (Balon.text == "69.4")
        {
            Balon.gameObject.GetComponent<Image>().color = Color.green;
            //Balon esta bien
            if (Cilindro.text == "104.2")
            {
                audio.seleccionAudio(1);
                pregunta3_2.SetActive(false);
                botondiagrama.SetActive(true);
            }
            else
            {
                Cilindro.gameObject.GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
            }
        }
        else
        {
            //El torque esta malo

            Balon.gameObject.GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }
    public void Q4()
    {
        if (Balon4.text == "0")
        {
            Balon4.gameObject.GetComponent<Image>().color = Color.green;
            //Balon esta bien
            if (Balon4_1.text == "2.4")
            {
                audio.seleccionAudio(1);
                pregunta4.SetActive(false);
                pregunta4_2.SetActive(false);
            }
            else
            {
                Balon4_1.gameObject.GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
            }
        }
        else
        {
            //El torque esta malo

            Balon4.gameObject.GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }
    public void Q4_2()
    {
        if (Balon4_2.text == "693")
        {
            Balon4_2.gameObject.GetComponent<Image>().color = Color.green;
            //Balon esta bien
                audio.seleccionAudio(1);
                pregunta4_2.SetActive(false);
    
        }
        else
        {
            //El torque esta malo

            Balon4_2.gameObject.GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }
}
