using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    Transform[] knockbackPlace;
    [SerializeField] float knockBackPower;
    [SerializeField] knockBackPoints points;
    Transform currentKnockbackPoint;
    
    [Header("Movement")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float playerJumpForce = 20f;

    [Header("Attributes")]
    [SerializeField] public float sandalDamage = 1;
    [SerializeField] public float sandalCooldown = 1f;

    [Header("Ground Raycast")]
    [SerializeField] float extraRaycastToGround = 0.1f;
    [SerializeField] LayerMask groundLayer;
    bool isMoving = true;
    bool isGround = true;
    float dir;
    Rigidbody2D rb;

    [Header("Animation")]
    [SerializeField] Animator anim;

    [Header("DeathPanel")]
    [SerializeField] DeathPanel panel;

    HealthScript healthScript;

    ScreenShake screenShake;

    private Vector2 knocbackPos;

    [SerializeField] private UnityEvent OnMove;
    [SerializeField] private UnityEvent OnStop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthScript = GetComponent<HealthScript>();
        knockbackPlace = points.knocbackPoints;
        screenShake = GetComponent<ScreenShake>();

        Time.timeScale = 1f;
    }


    void Update()
    {
        Jump();
        ChangeDirection();
        DustTrail();
    }


    void DustTrail()
    {
        if (dir == 0 && isGround)
        {
            OnStop.Invoke();
        }
        else
        {
            OnMove.Invoke();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        //raycast To check if character is at the ground
        RaycastToGround();
        if(Input.GetKeyDown(KeyCode.Space) && isGround && isMoving)
        {
            rb.AddForce(Vector2.up * playerJumpForce);
            anim.SetTrigger("SpacePressed");
        }
        
    }

    void RaycastToGround()
    {
        Collider2D capsuleCollider = GetComponent<Collider2D>();
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, capsuleCollider.bounds.extents.y + extraRaycastToGround, groundLayer); ;
        Debug.DrawRay(capsuleCollider.bounds.center, Vector2.down * (capsuleCollider.bounds.extents.y + extraRaycastToGround), Color.red);
        //Debug.Log(raycastHit.collider);
        if (raycastHit.collider != null)
        {
            isGround = true;
            anim.SetBool("IsAir", false);
        }
        else
        {
            isGround = false;
            anim.SetBool("IsAir", true);
        }
    }

    private void Move()
    {
        if (isMoving)
        {
            dir = Input.GetAxisRaw("Horizontal");
            if (dir != 0f)
            {
                anim.SetBool("IsWalking", true);
            }
            else
                anim.SetBool("IsWalking", false);
            Vector2 tempSpeed = new Vector2(dir * Time.deltaTime * playerSpeed, 0);

            rb.AddForce(tempSpeed, ForceMode2D.Impulse);

            
        }
    }

    private void ChangeDirection()
    {
        if (dir > 0 && transform.localScale.x == -1)
        {
            transform.localScale = new Vector2(1, 1);
        }
        if (dir < 0 && transform.localScale.x == 1)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public float GetPlayerDirection()
    {
        return transform.localScale.x;
    }

    public void ForceDisablePlayerMovement()
    {
        isMoving = false;
    }

    public void EnablePlayerMovement()
    {
        isMoving = true;
    }

    public void TakeDamage(float damage)
    {
        healthScript.TakeDamage(damage);
        anim.SetTrigger("Damaged");
        screenShake.GenerateScreenShake();
        FindObjectOfType<AudioManager>().PlaySound("PlayerDamaged_SFX");

        if(healthScript.GetCurrentHP() <= 0)
        {
            panel.EnableDeathPanel();
        }

    }

    public void knocBackEffect()
    {
        Vector2 directionToKnockback = (Vector2)(currentKnockbackPoint.position - transform.position);
        rb.AddForce(directionToKnockback * knockBackPower, ForceMode2D.Impulse);
    }

    public void EnemyComingFrom(bool temp)
    {
        if (!temp)
        {
            //Debug.Log("right");
            if(transform.localScale.x == 1)
                currentKnockbackPoint = knockbackPlace[1];
            else
                currentKnockbackPoint = knockbackPlace[0];
        }
        else
        {
            if (transform.localScale.x == -1)
                currentKnockbackPoint = knockbackPlace[1];
            else
                currentKnockbackPoint = knockbackPlace[0];
        }

    }

    public bool GetDir()
    {
        if (transform.localScale.x > 0f) return true;
        else return false;
    }


}
