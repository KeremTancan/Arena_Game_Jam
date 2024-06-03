using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportTarget;  // The location where the player will be teleported

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;
            SoundManager.Instance.PlayEffectSound(SoundManager.Instance.TeleportSound);
        }
    }
}