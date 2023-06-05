using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollectible : MonoBehaviour
{
    public ParticleSystem collectEffect;
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.bulletcount< controller.maxBulletCount)
            {
                controller.ChangeBulletCount(20);
                Instantiate(collectEffect, transform.position, Quaternion.identity);
                controller.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }
    }
}
