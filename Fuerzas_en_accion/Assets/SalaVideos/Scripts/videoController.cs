using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class videoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

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



    void Start()
    {
        // iniciar con volumen al 100%
        if (sliderVolumen != null)
            sliderVolumen.value = 1f;

        videoPlayer.Play();

        CambiarIcono();


        
    }

    void Update()
    {

        // Solo funciona cuando el video está abierto
        if (!player.enUI) return;

        CambiarIcono();

        if (videoPlayer.isPlaying && videoPlayer.length > 0)
        {
            sliderProgreso.value = (float)(videoPlayer.time / videoPlayer.length);

        }

        //  ESPACIO  Play / Pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayPause();
        }

        // FLECHA DERECHA  Adelantar 10s
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            videoPlayer.time = Mathf.Min((float)videoPlayer.length, (float)videoPlayer.time + 10f);
        }

        // FLECHA IZQUIERDA Retroceder 10s
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            videoPlayer.time = Mathf.Max(0f, (float)videoPlayer.time - 10f);
        }

    }

    //  PLAY / PAUSE
    public void PlayPause()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
        else
            videoPlayer.Play();

        CambiarIcono();
    }

    void CambiarIcono()
    {
        if (videoPlayer.isPlaying)
            iconoPlayPause.sprite = iconoPause;
        else
            iconoPlayPause.sprite = iconoPlay;
    }

    //  ADELANTAR 
    public void Adelantar()
    {
        videoPlayer.time += 10f;
    }

    //  RETROCEDER 
    public void Retroceder()
    {
        videoPlayer.time -= 10f;
        if (videoPlayer.time < 0) videoPlayer.time = 0;
    }

    //  VOLUMEN
    public void CambiarVolumen()
    {
        if (audioSource != null)
            audioSource.volume = sliderVolumen.value;
    }
    public void CerrarVideo()
    {
         // SOLO anima
    }
    public void DesactivarVideo()
    {
        animator.SetTrigger("Close");
        Debug.Log("Botón presionado");
        videoPlayer.Stop();
        
        gameObject.SetActive(false);

        player.enUI = false; // vuelve activar el cursos y camara
    }

}
