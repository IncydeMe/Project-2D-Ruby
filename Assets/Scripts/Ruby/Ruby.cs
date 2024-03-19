using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private int bulletAmount;
    private int currentBulletAmount;

    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.J;

    public GameObject bullet;
    public Rigidbody2D bulletRb;
    public Transform launchOffSet;
    #endregion

    #region UI
    [SerializeField]
    private MaskableGraphic healthBar;
    private float healthBarWidth, healthBarHeight;

    [SerializeField]
    private GameObject[] ammo;
    #endregion

    private bool isActionable;

    public static Ruby Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        isActionable = true;
        healthBarWidth = healthBar.rectTransform.rect.width;
        healthBarHeight = healthBar.rectTransform.rect.height;
        currentBulletAmount = bulletAmount;
        currentHealth = maxHealth;
        rubyRb.freezeRotation = true;
        isInvincible = false;
        invincibleTimer = 0;
    }

    void Update()
    {
        //Check invincible time
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
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
            if (currentBulletAmount > 0)
            {
                //Change bullet amount and bullet UI
                currentBulletAmount--;
                ammo[currentBulletAmount].gameObject.SetActive(false);

                //Launch Bullet
                GameObject projectileObject = Instantiate(bullet, launchOffSet.position, transform.rotation);
                CogBullet cogBullet = projectileObject.GetComponent<CogBullet>();
                cogBullet.Launch(lookDirection);

                rubyAnimation.SetTrigger("Launch");
            }
        }
    }

    public void ChangeHealth(int changeAmount)
    {
        if (currentHealth <= maxHealth && currentHealth > 0)
        {
            if (changeAmount < 0)
            {
                if (isInvincible) return;

                isInvincible = true;
                invincibleTimer = invincibleTime;

                rubyAnimation.SetTrigger("Hit");

                //Add particle
            }

            if (changeAmount > 0 && currentHealth == maxHealth) throw new Exception("Health Full");

            currentHealth = Mathf.Clamp(currentHealth + changeAmount, 0, maxHealth);
            float remainHealthPercentage = (float)currentHealth / (float)maxHealth;
            //Change size of healthbar
            healthBar.rectTransform.sizeDelta = new Vector2(healthBarWidth * remainHealthPercentage, healthBarHeight);
        }
        else
        {
            throw new Exception("Error: Health value out of the limit");
        }
    }

    public void IncreaseAmmo()
    {
        if (currentBulletAmount < bulletAmount)
        {
            ammo[currentBulletAmount].gameObject.SetActive(true);
            currentBulletAmount++;
        }
        else
            throw new Exception("Ammo Full");
    }

    public void ChangeActionableState()
    {
        isActionable = !isActionable;
    }
}
