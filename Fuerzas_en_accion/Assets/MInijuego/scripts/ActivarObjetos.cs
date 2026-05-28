using UnityEngine;

public class ActivarAlDesactivar : MonoBehaviour
{
    public GameObject objetoA;
    public GameObject objetoB;
    public GameObject objetoC;

    void Start()
    {
        // Espera 15 segundos y ejecuta la función
        Invoke("CambiarObjetos", 13f);
    }

    void CambiarObjetos()
    {
        // Apaga A
        objetoA.SetActive(false);

        // Activa B y C
        objetoB.SetActive(true);
        objetoC.SetActive(true);
    }
}