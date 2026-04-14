using UnityEngine;
using UnityEngine.Video;

public class HologramCanvas : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 5f;

    public GameObject videoContainer;
    public videoController videoController;

    public GameObject image;
    public AudioSource Fondo;

    private Animator panelAnimator;
    public PlayerController player;

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
                HologramData data = hit.collider.GetComponent<HologramData>();

                if (data != null)
                {
                    Fondo.Pause();

                    image.SetActive(true);
                    videoContainer.SetActive(true);

                    videoController.CargarVideo(data); // CLAVE

                    player.enUI = true;

                    panelAnimator.SetTrigger("Open");
                }
            }
        }
    }
}