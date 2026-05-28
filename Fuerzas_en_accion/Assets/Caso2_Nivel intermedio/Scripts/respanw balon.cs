using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respanwbalon : MonoBehaviour
{
    public GameObject prefabBalon;
    public Transform puntoSpawn;

    public Camera playerCamera;
    public Transform puntoMano;
    public LanzamientoBalon lanzador;

    private bool ocupado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (ocupado) return;

        if (!other.CompareTag("Balon")) return;

        ocupado = true;

        fisicasBalon balon = other.GetComponent<fisicasBalon>();

        if (balon != null)
        {
            // limpiar estado del sistema ANTES de destruir
            if (balon.lanzador != null)
            {
                balon.lanzador.modoLanzamiento = false;
                balon.lanzador.ResetFuerzaLanzamiento();
            }

            balon.agarrado = false;
        }

        Destroy(other.gameObject);

        GameObject nuevo = Instantiate(
            prefabBalon,
            puntoSpawn.position,
            Quaternion.identity
        );

        fisicasBalon script = nuevo.GetComponent<fisicasBalon>();

        if (script != null)
        {
            script.Inicializar(
                playerCamera,
                puntoMano,
                lanzador
            );
        }
        lanzador.balonfisicas = script;
        Invoke(nameof(Reset), 0.3f);
    }

    void Reset()
    {
        ocupado = false;
    }

}
