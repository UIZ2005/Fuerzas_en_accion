using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidacionDiagrama : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Fuerza;
    public GameObject Gravedad;
    public GameObject Normal;

    public GameObject VecH1;
    public GameObject VecH2;
    public GameObject VecH3;

    public GameObject[] vectores;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void validacion()
    {
       
        if (Fuerza.transform.position == VecH1.transform.position)
        {
           

            float z = vectores[0].transform.eulerAngles.z;

            if(z > 340f || z < 15f)
            {
                //el vector de fuerza se encuentra bien

               
                if (Normal.transform.position == VecH2.transform.position)
                {
                    z = vectores[1].transform.eulerAngles.z;
                    if (z > 340f || z < 15f)
                    {
                        if(Gravedad.transform.position == VecH3.transform.position)
                        {
                            z = vectores[2].transform.eulerAngles.z;
                            if (z > 340f || z < 15f)
                            {
                                GetComponent<Image>().color = Color.green;
                            }
                            else
                            {
                                GetComponent<Image>().color = Color.red;
                                //el vector de gravedad tiene mala orientacion
                            }
                        }
                        else
                        {
                            GetComponent<Image>().color = Color.red;
                            //el vector gravedad esta mal ubicado
                        }
                    }
                    else
                    {
                        GetComponent<Image>().color = Color.red;
                        //el vector normal tiene mal orientacion
                    }
                }
                else
                {
                    GetComponent<Image>().color = Color.red;
                    //El vector Normal esta mal ubicado
                }
            }
            else
            {
                GetComponent<Image>().color = Color.red;
                //El vector de fuerza tiene mal orientacion
            }
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            // El vector de fuerza esta mal ubicado
        }
    }
}
