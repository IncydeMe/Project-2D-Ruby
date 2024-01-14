using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float maxLifeTime;

    private float damage;

    private Vector3 fireDirection;
    

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
        BulletExpired();
    }

    private void BulletMovement()
    {
        transform.position += fireDirection * Time.deltaTime * bulletSpeed;
    }

    private void BulletExpired()
    {
        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeFireDirection(Vector3 direction)
    {
        fireDirection = direction;
    }
}
