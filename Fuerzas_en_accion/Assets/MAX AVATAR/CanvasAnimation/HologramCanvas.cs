using UnityEngine;
using UnityEngine.Video;

public class HologramCanvas : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 5f;

    public GameObject videoContainer;
    public VideoPlayer videoPlayer;
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
                Debug.Log("Objeto clickeado: " + hit.collider.name);

                if (data != null)
                {
                    Fondo.Pause();

                    //Activar el background que es eso de bg
                    image.SetActive(true);

                    // activar UI
                    videoContainer.SetActive(true);

                    // asignar video correcto
                    videoPlayer.clip = data.videoClip;

                    // reproducir
                    videoPlayer.Play();

                    player.enUI = true;

                    // animación
                    panelAnimator.SetTrigger("Open");
                }
            }
        }
    }
}