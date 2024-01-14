using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Ruby : MonoBehaviour
{
    #region Ruby_Variables
    [SerializeField]
    private Animator rubyAnimation;

    [SerializeField]
    private float moveSpeed;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private Rigidbody2D rb;
    #endregion

    #region Bullet_variables
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.J;

    public CogBullet bullet;
    public Transform launchOffSet;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        AnimationControl();

        AttackControl();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
    }

    private void AnimationControl()
    {
        RunningControl();
    }

    private void RunningControl()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            rubyAnimation.SetFloat("Speed", moveSpeed);
            FacingDirectionControl();
        }
        else
        {
            rubyAnimation.SetFloat("Speed", 0);
        }
    }

    private void FacingDirectionControl()
    {


        //Basic
        rubyAnimation.SetFloat("Look X", horizontalInput);
        rubyAnimation.SetFloat("Look Y", verticalInput);
    }

    private void AttackControl()
    {
        if(Input.GetKeyDown(shootKey))
        {
            
            Instantiate(bullet, launchOffSet.position, transform.rotation);
        }
    }
}
