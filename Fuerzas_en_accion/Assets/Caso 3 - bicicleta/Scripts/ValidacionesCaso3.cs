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
    private barraProgreso progreso;
    public GameObject staticbici;
    public GameObject movebici;
    public Animator door;
    public GameObject limit;


    public GameObject sonidobueno1;
    public GameObject sonidobueno2;
    public GameObject sonidomalo3;
    void Start()
    {
        progreso = GetComponent<barraProgreso>();
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
            sonidobueno1.SetActive(true);
            plato.gameObject.GetComponent<Image>().color = Color.green;
            if (pinon.text == "4N*m")
            {
                //PASA A LA SIGUIENTE
                sonidobueno2.gameObject.SetActive(true);
                pinon.gameObject.GetComponent<Image>().color = Color.green;
                SiguientePregunta();
            }else{
                pinon.gameObject.GetComponent<Image>().color = Color.red;
                sonidomalo3.SetActive(true);
            }
        }
        else{
            if (pinon.text == "4N*m")
            {
              pinon.gameObject.GetComponent<Image>().color = Color.green;
            }
            else
            {
              pinon.gameObject.GetComponent<Image>().color = Color.red;
            } 
            plato.gameObject.GetComponent<Image>().color = Color.red;
            sonidomalo3.SetActive(true);
        }
    }
    public void ValidacionPreguntaPlatoSelect()
    {
        if (pinonSelect ==1 && platoSelect==1)
        {
            //ULTIMA PREGUNTA FINALIZA TODO
            SiguientePregunta();
            progreso.Avanzar();
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
        if (i >= 2)
        {
            staticbici.SetActive(false);
            movebici.SetActive(true);
            door.SetBool("up", true);
            limit.SetActive(false);
        }
       
       //pone aqui lo de desactivar y activar los panels poneles id o que sea progresivo no se jsjsjsjsjs
    }
}
