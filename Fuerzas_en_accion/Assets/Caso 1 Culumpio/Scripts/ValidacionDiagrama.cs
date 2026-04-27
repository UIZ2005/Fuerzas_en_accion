using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidacionDiagrama : MonoBehaviour
{
    [Header("Subtítulos Sistema 2")]
    public Sistema2 sistema2;


    public GameObject Fuerza;
    public GameObject Gravedad;
    public GameObject Normal;

    public GameObject VecH1;
    public GameObject VecH2;
    public GameObject VecH3;

    public GameObject[] vectores;
    private barraProgreso progreso;
    public GameObject diagrama;

    private AudioManager audio;

    [Header("Subtítulos")]
    public SubtitleSystem subtitleSystem;

    void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
        progreso = FindAnyObjectByType<barraProgreso>();
    }

    public void validacion()
    {
        if (Fuerza.transform.position == VecH1.transform.position)
        {
            float z = vectores[0].transform.eulerAngles.z;

            if (z > 340f || z < 15f)    
            {
                // Fuerza correcta

                if (Normal.transform.position == VecH2.transform.position)
                {
                    z = vectores[1].transform.eulerAngles.z;

                    if (z > 340f || z < 15f)
                    {
                        // Normal correcta

                        if (Gravedad.transform.position == VecH3.transform.position)
                        {
                            z = vectores[2].transform.eulerAngles.z;

                            if (z > 340f || z < 15f)
                            {
                                //  TODO CORRECTO
                                GetComponent<Image>().color = Color.green;
                                progreso.Avanzar();
                                audio.seleccionAudio(1);
                                diagrama.SetActive(false);

                                // SUBTÍTULOS SISTEMA 2
                                if (sistema2 != null)
                                {
                                    sistema2.Ejecutar();
                                }
                            }
                            else
                            {
                                //  Gravedad mal orientada
                                GetComponent<Image>().color = Color.red;
                                audio.seleccionAudio(2);
                            }
                        }
                        else
                        {
                            //  Gravedad mal ubicada
                            GetComponent<Image>().color = Color.red;
                            audio.seleccionAudio(2);
                        }
                    }
                    else
                    {
                        //  Normal mal orientada
                        GetComponent<Image>().color = Color.red;
                        audio.seleccionAudio(2);
                    }
                }
                else
                {
                    //  Normal mal ubicada
                    GetComponent<Image>().color = Color.red;
                    audio.seleccionAudio(2);
                }
            }
            else
            {
                //  Fuerza mal orientada
                GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
            }
        }
        else
        {
            //  Fuerza mal ubicada
            GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }
}