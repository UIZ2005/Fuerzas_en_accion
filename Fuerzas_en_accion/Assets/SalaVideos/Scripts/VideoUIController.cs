using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoUIController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    public GameObject panel; // VideoContainer
    public Slider volumeSlider;

    //  Play / Pause
    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }

    //  Adelantar 10s
    public void Forward()
    {
        videoPlayer.time += 10f;
    }

    //  Retroceder 10s
    public void Backward()
    {
        videoPlayer.time -= 10f;
    }

    //  Volumen
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    //  Cerrar panel
    public void Exit()
    {
        videoPlayer.Stop();
        panel.SetActive(false);
    }
}