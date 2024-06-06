using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportTarget;
    public ParticleSystem teleportEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;
            
            SoundManager.Instance.PlayEffectSound(SoundManager.Instance.TeleportSound);
            
            ParticleSystem effect = Instantiate(teleportEffect, teleportTarget.position, Quaternion.identity);
            effect.Play();
            
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
    }
}