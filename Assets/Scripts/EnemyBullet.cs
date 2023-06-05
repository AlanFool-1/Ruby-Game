using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public int attack = 1;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(StopBullet(3f));
  
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        transform.right = direction;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController e = other.collider.GetComponent<RubyController>();
        Debug.Log(other.collider);
        if (e != null)
        {
            Rigidbody2D RubyRigidbody = e.GetComponent<Rigidbody2D>();
            e.ChangeHealth(-1);
            
        }
        Destroy(gameObject);
    }
    private IEnumerator StopBullet(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
