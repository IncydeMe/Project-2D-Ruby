using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby : MonoBehaviour
{
    #region Ruby_Variables
    [SerializeField]
    private Animator rubyAnimation;

    [SerializeField]
    private Rigidbody2D rubyRb;

    //====MOVEMENT====
    [SerializeField]
    private float moveSpeed;

    private float horizontalInput;
    private float verticalInput;

    Vector2 lookDirection = new Vector2(1f, 0f);

    //====Health====
    private int maxHealth = 5;
    private int currentHealth;
    private float invincibleTime = 2f;
    private float invincibleTimer;
    private bool isInvincible;
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
        rubyRb.freezeRotation = true;
        isInvincible = false;
        invincibleTimer = 0;
    }

    void Update()
    {
        //Check invincible time
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0 )
            {
                isInvincible = false;
            }
        }

        //Get movement input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        ActionControl();
    }


    private void FixedUpdate()
    {
        rubyRb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
    }

    private void ActionControl()
    {
        Run();
        Attack();
    }

    private void Run()
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

    private void Attack()
    {
        if (Input.GetKeyDown(shootKey))
        {
            GameObject projectileObject = Instantiate(bullet, launchOffSet.position, transform.rotation);
            CogBullet cogBullet = projectileObject.GetComponent<CogBullet>();
            cogBullet.Launch(lookDirection);

            rubyAnimation.SetTrigger("Launch");
        }
    }

    public void ChangeHealth(int changeAmount)
    {
        if(changeAmount < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibleTimer = invincibleTime;

            rubyAnimation.SetTrigger("Hit");

            //Add particle
        }

        currentHealth = Mathf.Clamp(currentHealth + changeAmount, 0, maxHealth);
    }
}
