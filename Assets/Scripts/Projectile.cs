using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public int attack = 1;
    public int BulletLevel = 1;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(StopBullet(3f));
    }
    public void Launch(Vector2 direction, float force, int Level)
    {
        rigidbody2d.AddForce(direction * force);
        BulletLevel = Level;
        if (BulletLevel == 4) attack += 1;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        //Debug.Log(other.collider);
        if (e != null)
        {
            Rigidbody2D enemyRigidbody = e.GetComponent<Rigidbody2D>();
            enemyRigidbody.isKinematic = true;
            e.ChangeBlood(attack, BulletLevel);
        }
        Destroy(gameObject);
    }
    private IEnumerator StopBullet(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
