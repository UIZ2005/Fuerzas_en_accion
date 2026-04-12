using UnityEngine;
using UnityEngine.Video;

public class HologramCanvas : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 5f;

    public GameObject videoContainer;
    public VideoPlayer videoPlayer;

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
                HologramData data = hit.collider.GetComponent<HologramData>();

                if (data != null)
                {
                    // activar UI
                    videoContainer.SetActive(true);

                    // asignar video correcto
                    videoPlayer.clip = data.videoClip;

                    // reproducir
                    videoPlayer.Play();

                    // animación
                    panelAnimator.SetTrigger("Open");
                }
            }
        }
    }
}