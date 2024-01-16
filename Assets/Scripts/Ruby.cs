using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using TMPro;
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

    Vector2 lookDirection = new Vector2 (1f, 0f);
    #endregion

    #region Bullet_variables
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.J;

    public GameObject bullet;
    public Rigidbody2D bulletRb;
    public Transform launchOffSet;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        //Get movement input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        ActionControl(); 
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
    }

    private void ActionControl()
    {
        RunningControl();
        AttackControl();
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
        if(horizontalInput < 0)
        {
            rubyAnimation.SetFloat("Look X", -1);
        }
        else
        {
            rubyAnimation.SetFloat("Look X", horizontalInput);
        }
        rubyAnimation.SetFloat("Look Y", verticalInput);

        lookDirection.Set(horizontalInput, verticalInput);
    }

    private void AttackControl()
    {
        if (Input.GetKeyDown(shootKey))
        {
            GameObject projectileObject = Instantiate(bullet, launchOffSet.position, transform.rotation);
            CogBullet cogBullet = projectileObject.GetComponent<CogBullet>();
            cogBullet.Launch(lookDirection);

            rubyAnimation.SetTrigger("Launch");
        }
    }

}
