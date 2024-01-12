using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Ruby : MonoBehaviour
{
    [SerializeField]
    private Animator rubyAnimation;

    [SerializeField] 
    private float moveSpeed;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] 
    private Rigidbody2D rb;

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
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontalInput * moveSpeed, verticalInput * moveSpeed);
    }

    private void AnimationControl()
    {
        RunningControl();
        FacingDirectionControl();
    }

    private void RunningControl()
    {
        if(horizontalInput != 0 || verticalInput != 0)
        {
            rubyAnimation.SetFloat("Speed", moveSpeed);
        }
        else
        {
            rubyAnimation.SetFloat("Speed", 0);
        }
    }

    private void FacingDirectionControl()
    {
        if(horizontalInput != 0 || verticalInput != 0)
        {
            rubyAnimation.SetFloat("Look X", horizontalInput);
            rubyAnimation.SetFloat("Look Y", verticalInput);
        }
    }
}
