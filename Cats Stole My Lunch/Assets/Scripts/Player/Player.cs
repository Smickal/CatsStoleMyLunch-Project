using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float playerJumpForce = 20f;

    [Header("Ground Raycast")]
    [SerializeField] float extraRaycastToGround = 0.1f;
    [SerializeField] LayerMask groundLayer;
    bool isMoving = true;
    bool isGround = true;
    float dir;
    Rigidbody2D rb;

    HealthScript healthScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthScript = gameObject.GetComponent<HealthScript>();
    }


    void Update()
    {
        Jump();
        ChangeDirection();

        if (Input.GetKeyDown(KeyCode.Y))
        {
            healthScript.TakeDamage(10);
            healthScript.SetCurrentHPToDisplay();
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
        }
        
    }

    void RaycastToGround()
    {
        Collider2D boxCollider = GetComponent<Collider2D>();
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, boxCollider.bounds.extents.y + extraRaycastToGround, groundLayer); ;
        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraRaycastToGround), Color.red);
        //Debug.Log(raycastHit.collider);
        if (raycastHit.collider != null) isGround = true;
        else isGround = false;
    }

    private void Move()
    {
        if (isMoving)
        {
            dir = Input.GetAxisRaw("Horizontal");
            Vector2 tempSpeed = new Vector2(dir * Time.deltaTime * playerSpeed, 0);

            //transform.Translate(new Vector2(dir * playerSpeed * Time.deltaTime, 0));
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

}
