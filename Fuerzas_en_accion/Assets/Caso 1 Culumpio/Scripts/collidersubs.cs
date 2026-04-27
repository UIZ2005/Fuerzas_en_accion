using UnityEngine;

public class collidersubs : MonoBehaviour
{
    public GameObject panel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        panel.SetActive(false);
    }
}
}