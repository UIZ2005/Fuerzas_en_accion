using UnityEngine;

public class HologramCanvas : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 5f;

    public GameObject videoContainer;
    private Animator panelAnimator;

    void Start()
    {
        panelAnimator = videoContainer.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Hologram"))
                {
                    videoContainer.SetActive(true);
                    panelAnimator.SetTrigger("Open");
                }
            }
        }
    }
}