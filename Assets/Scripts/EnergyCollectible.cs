using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollectible : MonoBehaviour
{
    public ParticleSystem collectEffect;
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.curenergy < controller.maxenergy)
            {
                controller.ChangeEnergy(50);
                Instantiate(collectEffect, transform.position, Quaternion.identity);
                controller.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }
    }
}
