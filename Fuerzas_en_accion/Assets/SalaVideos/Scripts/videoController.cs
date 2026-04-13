using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class videoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [Header("Audio UI")]
    public AudioSource uiAudioSource;
    public AudioClip cerrarSonido;


    [Header("Botón Play/Pause")]
    public Image iconoPlayPause;
    public Sprite iconoPlay;
    public Sprite iconoPause;

    [Header("Volumen")]
    public Slider sliderVolumen;
    public AudioSource audioSource;

    [Header("Progreso")]
    public Slider sliderProgreso;

    public Animator animator;
    public PlayerController player;

    public GameObject panelVideo;

    public ControladorPreguntas preguntaController;

    void Start()
    {
        if (sliderVolumen != null)
            sliderVolumen.value = 1f;

        CambiarIcono();
    }

    void Update()
    {
        if (!player.enUI) return;

        CambiarIcono();

        if (videoPlayer.isPlaying && videoPlayer.length > 0)
        {
            sliderProgreso.value = (float)(videoPlayer.time / videoPlayer.length);
        }

        // Controles
        if (Input.GetKeyDown(KeyCode.Space))
            PlayPause();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            videoPlayer.time = Mathf.Min((float)videoPlayer.length, (float)videoPlayer.time + 10f);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            videoPlayer.time = Mathf.Max(0f, (float)videoPlayer.time - 10f);
    }

    
    public void CargarVideo(HologramData data)
    {
        StartCoroutine(PrepararVideo(data));
    }

    IEnumerator PrepararVideo(HologramData data)
    {
        videoPlayer.clip = data.videoClip;

        videoPlayer.Prepare();

        // Espera a que el video esté listo
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        //  inicializa preguntas DESPUÉS de preparar video
        preguntaController.Inicializar(data.pregunta, videoPlayer);

        videoPlayer.Play();
    }

    public void PlayPause()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
        else
            videoPlayer.Play();

        CambiarIcono();
    }


    public void Adelantar()
    {
        videoPlayer.time = Mathf.Min((float)videoPlayer.length, (float)videoPlayer.time + 10f);
    }

    public void Retroceder()
    {
        videoPlayer.time = Mathf.Max(0f, (float)videoPlayer.time - 10f);
    }

    void CambiarIcono()
    {
        if (videoPlayer.isPlaying)
            iconoPlayPause.sprite = iconoPause;
        else
            iconoPlayPause.sprite = iconoPlay;
    }

    public void CambiarVolumen()
    {
        if (audioSource != null)
            audioSource.volume = sliderVolumen.value;
    }

    public void CerrarVideo()
    {
        StartCoroutine(DesactivarVideo());
    }

    IEnumerator DesactivarVideo()
    {
        animator.SetTrigger("Close");

        if (uiAudioSource != null && cerrarSonido != null)
        {
            uiAudioSource.PlayOneShot(cerrarSonido);
        }

        yield return new WaitForSeconds(2f);

        videoPlayer.Stop();

        gameObject.SetActive(false);
        player.enUI = false;
    } 

}
