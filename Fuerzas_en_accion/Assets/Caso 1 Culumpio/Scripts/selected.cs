using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected : MonoBehaviour
{
    // Start is called before the first frame update
    LayerMask mask;
    public float distancia=1.5f;
    GameObject ultimoreconocido = null;
    public Material[] select;
    public Material[] normal;
    private int N;
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
          
            if (hit.collider.tag == "InterctiveZone")
            {

                if (hit.collider.gameObject.GetComponent<InteractiveObj>().isVec)
                {
                    N = 1;
                    if ((hit.collider.gameObject.GetComponent<InteractiveObj>().iscorrect))
                    {
                        N = 2;
                    }
                }
                else
                {
                    N = 0;
                }
                DeSelect();
                selectObject(hit.transform);
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<InteractiveObj>().click();
                }
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
        transform.GetComponent<MeshRenderer>().material = select[N];
        ultimoreconocido = transform.gameObject;
    }
    void DeSelect()
    {
        if (ultimoreconocido)
        {
            ultimoreconocido.GetComponent<Renderer>().material = normal[N];
            ultimoreconocido = null;
        }
    }
}
