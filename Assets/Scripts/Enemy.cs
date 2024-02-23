using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;

    #region Enemy
    public float moveSpeed;
    private Vector2 moveDirection;

    private float distance;
    [SerializeField]
    private float distanceBetween;
    
    [SerializeField]
    private Rigidbody2D enermyRb;
    [SerializeField]
    private Animator enemyAnimation;

    [SerializeField]
    private float changeDirectionTime;
    private float remainingChangeTIme;
    [SerializeField]
    private bool isMoveHorizontal;

    private bool isFixed;
    #endregion

    //========Enermy Type==========
    private enum Type
    {
        CHASE,
        MOVE_AROUND
    }

    [SerializeField]
    private Type enermyType;

    private void Start()
    {
        isFixed = false;
        remainingChangeTIme = changeDirectionTime;
        if(enermyType == Type.MOVE_AROUND)
        {
            moveDirection = isMoveHorizontal ? Vector2.right * moveSpeed : Vector2.down * moveSpeed;
        }
    }

    void Update()
    {
        if(!isFixed)
        {
            switch (enermyType)
            {
                case Type.CHASE:
                    Chase();
                    break;
                case Type.MOVE_AROUND:
                    MoveAround();
                    break;
            }
        }
        else
        {
            Stand();
        }
    }

    private void FixedUpdate()
    {
        enermyRb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void MoveAround()
    {
        remainingChangeTIme -= Time.deltaTime;

        if(remainingChangeTIme <= 0 )
        {
            remainingChangeTIme += changeDirectionTime;
            moveDirection *= -1;
        }

        enemyAnimation.SetFloat("ForwardX", moveDirection.x);
        enemyAnimation.SetFloat("ForwardY", moveDirection.y);
        enemyAnimation.SetFloat("Speed", 1);
    }

    private void Chase()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            moveDirection = direction * moveSpeed;
            enemyAnimation.SetFloat("ForwardX", direction.x);
            enemyAnimation.SetFloat("ForwardY", direction.y);
            enemyAnimation.SetFloat("Speed", 1);
        }
        else
        {
            enemyAnimation.SetFloat("Speed", 0);
            moveDirection = Vector2.zero;
        }
    }

    private void Stand()
    {
        moveDirection = Vector2.zero;
    }

    public void Fix()
    {
        enemyAnimation.SetTrigger("Fixed");
        isFixed = true;

        //Add particle

        
        Destroy(gameObject, 2.5f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isFixed) return;

        Ruby ruby = collision.collider.GetComponent<Ruby>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }
}
