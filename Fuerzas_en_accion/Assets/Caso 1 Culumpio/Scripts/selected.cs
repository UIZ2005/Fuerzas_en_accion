using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected : MonoBehaviour
{
    // Start is called before the first frame update
    LayerMask mask;
    public float distancia=1.5f;
    GameObject ultimoreconocido = null;
    public Material select;
    public Material normal;
    void Start()
    {
        mask = LayerMask.GetMask("RaycastDetect");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit, distancia,mask))
        {
            DeSelect();
            selectObject(hit.transform);
            if (hit.collider.tag == "InterctiveZone")
            {

            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
        else
        {
            DeSelect();
        }
    }
    void selectObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material=select;
        ultimoreconocido = transform.gameObject;
    }
    void DeSelect()
    {
        if (ultimoreconocido)
        {
            ultimoreconocido.GetComponent<Renderer>().material=normal;
            ultimoreconocido = null;
        }
    }
}
