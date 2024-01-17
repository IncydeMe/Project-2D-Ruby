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

    Vector2 lookDirection = new Vector2(1f, 0f);
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
        Vector2 move = new Vector2(horizontalInput, verticalInput);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        rubyAnimation.SetFloat("Look X", lookDirection.x);
        rubyAnimation.SetFloat("Look Y", lookDirection.y);
        rubyAnimation.SetFloat("Speed", move.magnitude);
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
