using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float maxLifeTime;

    private float damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = direction * bulletSpeed;    
    }
}
