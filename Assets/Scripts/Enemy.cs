using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ruby;

    #region Enemy
    public float moveSpeed;

    private float distance;

    [SerializeField]
    private float distanceBetween;
    [SerializeField]
    private Rigidbody2D enermyRb;
    [SerializeField]
    private Animator enermyAnimation;
    #endregion

    void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        distance = Vector2.Distance(transform.position, ruby.transform.position);
        Vector2 direction = ruby.transform.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            enermyRb.velocity = direction * moveSpeed;
            enermyAnimation.SetFloat("ForwardX", direction.x);
            enermyAnimation.SetFloat("ForwardY", direction.y);
            enermyAnimation.SetFloat("Speed", 1);
        }
        else
        {
            enermyAnimation.SetFloat("Speed", 0);
            enermyRb.velocity = Vector2.zero;
        }
    }
}
