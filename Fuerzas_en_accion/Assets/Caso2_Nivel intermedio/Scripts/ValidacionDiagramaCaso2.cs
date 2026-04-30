using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValidacionDiagramaCaso2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Fuerza;
    public GameObject Gravedad;
    public GameObject Normal;

    public GameObject VecH1;
    public GameObject VecH2;
    public GameObject VecH3;

    public GameObject[] vectores;
    private barraProgreso progreso;
    public GameObject diagrama;
    public TMP_InputField input;
    private bool goodinput=false;
    public float limitsup=340;
    public float limitin=15;
    public string answer ="f";

    private AudioManager audio;
    void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
        progreso = FindAnyObjectByType<barraProgreso>();
    }

    // Update is called once per frame

        public void validacion()
        {

            if (string.IsNullOrEmpty(input.text))
            {
                GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
                goodinput = false;
                //esta vacio el input 
            }
            if (input.text == answer)
            {
                goodinput = true;
            }
            else
            {
                GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
                goodinput = false;
            }

            if (Fuerza.transform.position == VecH1.transform.position)
            {
                float z = vectores[0].transform.eulerAngles.z;

                if (z > limitsup || z < limitin)
                {
                    // Fuerza correcta

                    if (Normal.transform.position == VecH2.transform.position)
                    {
                        z = vectores[1].transform.eulerAngles.z;

                        if (z > limitsup || z < limitin)
                        {
                            // Normal correcta

                            if (Gravedad.transform.position == VecH3.transform.position)
                            {
                                z = vectores[2].transform.eulerAngles.z;

                                if (z > limitsup || z < limitin && goodinput)
                                {
                                    //  TODO CORRECTO
                                    GetComponent<Image>().color = Color.green;
                                    progreso.Avanzar();
                                    audio.seleccionAudio(1);
                                    diagrama.SetActive(false);
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
    public void validacion2()
    {

        if (string.IsNullOrEmpty(input.text))
        {
            GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
            goodinput = false;
            //esta vacio el input 
        }
        if (input.text == answer)
        {
            goodinput = true;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
            goodinput = false;
        }

        if (Fuerza.transform.position == VecH1.transform.position)
        {
            float z = vectores[0].transform.eulerAngles.z;

            if (z > 190f || z < 230f)
            {
                // Fuerza correcta

                if (Normal.transform.position == VecH2.transform.position)
                {
                    z = vectores[1].transform.eulerAngles.z;

                    if (z > 15f || z < 50f)
                    {
                        // Normal correcta

                        if (Gravedad.transform.position == VecH3.transform.position)
                        {
                            z = vectores[2].transform.eulerAngles.z;

                            if (z > limitsup || z < limitin && goodinput)
                            {
                                //  TODO CORRECTO
                                GetComponent<Image>().color = Color.green;
                                progreso.Avanzar();
                                audio.seleccionAudio(1);
                                diagrama.SetActive(false);
                            }
                            else
                            {
                                //  Gravedad mal orientada
                                Debug.Log("mal gravedad");
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

                        Debug.Log("mal normal");
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
                Debug.Log("mal fuerza");
                GetComponent<Image>().color = Color.red;
                audio.seleccionAudio(2);
            }
        }
        else
        {
            //  Fuerza mal ubicada
            Debug.Log("mal fuerza U");
            GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
        }
    }

    public void validacion3()
    {

        if (string.IsNullOrEmpty(input.text))
        {
            GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
            goodinput = false;
            //esta vacio el input 
        }
        if (input.text == answer)
        {
            goodinput = true;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            audio.seleccionAudio(2);
            goodinput = false;
        }

        if (Fuerza.transform.position == VecH1.transform.position)
        {
            float z = vectores[0].transform.eulerAngles.z;

            if (z > 160 || z < 200)
            {
                // Fuerza correcta

                if (Normal.transform.position == VecH2.transform.position)
                {
                    z = vectores[1].transform.eulerAngles.z;

                    if (z > limitsup || z < limitin)
                    {
                        // Normal correcta

                        if (Gravedad.transform.position == VecH3.transform.position)
                        {
                            z = vectores[2].transform.eulerAngles.z;

                            if (z > limitsup || z < limitin && goodinput)
                            {
                                //  TODO CORRECTO
                                GetComponent<Image>().color = Color.green;
                                progreso.Avanzar();
                                audio.seleccionAudio(1);
                                diagrama.SetActive(false);
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
