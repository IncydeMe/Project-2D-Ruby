using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D bulletRb;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float maxLifeTime;

    private float damage;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletExpired();
    }

    private void BulletExpired()
    {
        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction)
    {
        bulletRb.velocity = direction * bulletSpeed;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.GetComponent<Ruby>() != null) return;

    //    Enemy enemy = collision.collider.GetComponent<Enemy>();

    //    if(enemy != null)
    //    {
    //        enemy.Fix();
    //    }

    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Fix();
            }

            Destroy(gameObject);
        }
    }
}
