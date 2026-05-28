using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicTest : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip testMusic;

    [Range(0f,1f)]
    public float targetVolume = 0.079f;

    public float crossfadeDuration = 0.5f;

    private bool evaluationActive = false;

    void Start()
    {
        audioSource.clip = testMusic;
    }

    public void StartEvaluationMusic()
    {
        evaluationActive = true;
        StartCoroutine(PlayLoopWithCrossfade());
    }

    public void StopEvaluationMusic()
    {
        evaluationActive = false;
        audioSource.Stop();
    }

    IEnumerator PlayLoopWithCrossfade()
    {
        while (evaluationActive)
        {
            audioSource.volume = targetVolume;
            audioSource.Play();

            yield return new WaitForSeconds(testMusic.length - crossfadeDuration);

            float timer = 0f;

            while (timer < crossfadeDuration)
            {
                timer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(targetVolume, 0f, timer / crossfadeDuration);
                yield return null;
            }

            audioSource.Stop();
            audioSource.time = 0f;

            audioSource.volume = 0f;
            audioSource.Play();

            timer = 0f;

            while (timer < crossfadeDuration)
            {
                timer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(0f, targetVolume, timer / crossfadeDuration);
                yield return null;
            }
        }
    }
}