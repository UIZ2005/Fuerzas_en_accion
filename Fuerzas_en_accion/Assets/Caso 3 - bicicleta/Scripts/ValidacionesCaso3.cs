using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ValidacionesCaso3 : MonoBehaviour
{
    // Start is called before the first frame update
    private int pinonSelect;
    private int platoSelect;

    public TMP_InputField plato;
    public TMP_InputField pinon;

    public GameObject[] preguntas;
    private int i=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPinon(int x )
    {
        pinonSelect = x;
    }
    public void SetPlato(int x)
    {
        platoSelect = x;
    }
    public void validacionPlatoPinon()
    {
        if(plato.text== "16N*m")
        {
            plato.gameObject.GetComponent<Image>().color = Color.green;
            if (pinon.text == "4N*m")
            {
                pinon.gameObject.GetComponent<Image>().color = Color.green;
                SiguientePregunta();
            }else{
                pinon.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
        else{
            plato.gameObject.GetComponent<Image>().color = Color.red;
        }
    }
    public void ValidacionPreguntaPlatoSelect()
    {
        if (pinonSelect ==1 && platoSelect==1)
        {
            SiguientePregunta();
        }
        else
        {
            //sonido malo
        }
    }

    public void SiguientePregunta()
    {
        preguntas[i].SetActive(false);
        i += 1;
        if (preguntas[i] != null)
        {
            preguntas[i].SetActive(true);
        }
       
       //pone aqui lo de desactivar y activar los panels poneles id o que sea progresivo no se jsjsjsjsjs
    }
}
