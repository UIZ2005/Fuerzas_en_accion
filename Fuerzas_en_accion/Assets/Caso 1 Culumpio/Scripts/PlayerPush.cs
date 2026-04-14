using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 2f;
    private AudioManager audio;
    private void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        // Solo si tiene Rigidbody y no es kinematic
        if (rb == null || rb.isKinematic) return;

        // Evitar empujar hacia abajo
        if (hit.moveDirection.y < -0.3f) return;

        // Dirección de empuje
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
    }
}
