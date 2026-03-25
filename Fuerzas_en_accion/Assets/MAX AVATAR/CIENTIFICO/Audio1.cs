using UnityEngine;

public class Audio1 : StateMachineBehaviour
{
    public AudioClip sonido;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource audio = animator.GetComponent<AudioSource>();

        if (audio != null && sonido != null)
        {
            audio.PlayOneShot(sonido);
        }
    }
}