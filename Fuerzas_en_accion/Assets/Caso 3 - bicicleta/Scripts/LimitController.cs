using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    public GameObject targetObject;
    public float activeTime = 2f;

    private Coroutine currentRoutine;
    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ENTER");
            playerInside = true;
            Activate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            Debug.Log("STAY (fallback)");
            playerInside = true;
            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("EXIT");
            playerInside = false;
        }
    }

    void Activate()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ActivateTemporarily());
    }

    IEnumerator ActivateTemporarily()
    {
        targetObject.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        targetObject.SetActive(false);
    }
}