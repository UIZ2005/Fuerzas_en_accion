using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidacionesCaso3 : MonoBehaviour
{
    // Start is called before the first frame update
    private int pinonSelect;
    private int platoSelect;
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

       //pone aqui lo de desactivar y activar los panels poneles id o que sea progresivo no se jsjsjsjsjs
    }
}
