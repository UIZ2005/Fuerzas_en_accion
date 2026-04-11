using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VectorInteraction : MonoBehaviour, IDragHandler
{
    // Start is called before the first frame update
    private Vector3 startPosition;
    private Quaternion startRotation;

    public GameObject vector;
    public GameObject N;
    public Animator animHovers;
    private bool hitZone=false;

    void Start()
    {
        startPosition = gameObject.transform.position;
        startRotation = vector.transform.rotation;
    }
    public void OnDrag(PointerEventData eventData)
    {
        animHovers.SetBool("isDrag", true);
        transform.position += (Vector3)eventData.delta;
        hitZone = false;
        GetComponent<Image>().enabled = false;
        vector.SetActive(true);
        N.SetActive(true);


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && startPosition != transform.position && !hitZone)
        {
            animHovers.SetBool("isDrag", false);
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);


            foreach (var result in results)
            {
                if (result.gameObject.CompareTag("InterctiveZone"))
                {
                    transform.position = result.gameObject.transform.position;
                    hitZone = true;
                    break;
                }
            }

            if (!hitZone)
            {
                GetComponent<Image>().enabled = true;

                transform.position = startPosition;
                vector.transform.rotation = startRotation;
                vector.SetActive(false);
                N.SetActive(false);
            }
        }
        if (Input.GetMouseButtonDown(1) && startPosition!=transform.position && !hitZone)
        {
            vector.transform.Rotate(0, 0, -30);
            Debug.Log(vector.transform.eulerAngles.z);
        }

    }
}
