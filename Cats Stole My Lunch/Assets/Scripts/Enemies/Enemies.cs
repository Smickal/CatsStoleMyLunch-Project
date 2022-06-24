using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    
    [Header("Attributes")]
    [SerializeField] public float walkingSpeed;
    [SerializeField] float sandalSpeedTreshold;
    [SerializeField] float attackPower = 1f;
    [SerializeField] float knockBackPower = 5f;
    


    [Header("RayCast Attributes")]
    Vector2 rayDir = Vector2.right;
    [SerializeField] float rayDistanceToGround;
    [SerializeField] float rayDistanceToWall = 1f;
    [SerializeField] float rayDistanceToPlayer = 10f;
    float distanceToPlayer;


    [Header("UI")]
    [SerializeField] Transform tmpText;
    [SerializeField] public TMP_Text behaviourText;
    [SerializeField] Image ExclamationMark;
    [SerializeField] GameObject hpBar;
 
    [Header("LayerMask")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] LayerMask cameraColliderMask;

    [SerializeField] Transform eye;

    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isRight = true;
    [HideInInspector] public bool isAttacking = false;

    private bool isJumping = false;
    Animator anim;
    EnemyStateManager stateManager;

    float internalJumpTimer = 0.2f;
    float timer;

    [SerializeField] BoxCollider2D attCollider;

    //popUp Container
    [SerializeField] GameObject damagePopUpText;
    [SerializeField] GameObject popUpContainer;
    Rigidbody2D rb;

    //knocback Attributes
    Transform currentKnockbackPoint;
    Transform[] knockbackPlace; 
    [SerializeField] knockBackPoints points;

    HealthScript healthScript;
    bool isHit = false;
    private void Awake()
    {
        player = FindObjectOfType<Player>();

        anim = GetComponent<Animator>();
        healthScript = GetComponent<HealthScript>();
        stateManager = GetComponent<EnemyStateManager>();
        rb = GetComponent<Rigidbody2D>();

        knockbackPlace = points.knocbackPoints;

        hpBar.SetActive(false);
    }

    private void Start()
    {      
        HideExclamationMark();
    }
    // Update is called once per frame
    void Update()
    {
        Move();

        if(RaycastToGround() && isJumping && timer >= internalJumpTimer)
        {
            anim.SetTrigger("CatLanding");
            isJumping = false;
            timer = 0f;
            stateManager.SwitchState(stateManager.GetAttackState(), "SetToAttack");
        }

        if (isJumping) timer += Time.deltaTime;

        RaycastToWall();
        ChangeDirection();

    }

    public void ChangeDirection()
    {
        if (isRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            tmpText.localScale = new Vector3(1, 1, 1);
            rayDir = Vector2.right;

        }
        if(!isRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            tmpText.localScale = new Vector3(-1, 1, 1);
            rayDir = Vector2.left;

        }
    }

    void Move()
    {
        if(isMoving && isRight)
            transform.Translate(transform.right * walkingSpeed * Time.deltaTime, Space.World);     

        if(isMoving && !isRight)
            transform.Translate(transform.right* -1 * walkingSpeed * Time.deltaTime, Space.World);
    }


    public  bool RaycastToGround()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit =  Physics2D.Raycast(collider.bounds.center,
                                                        Vector2.down,
                                                        collider.bounds.extents.y + rayDistanceToGround,
                                                        groundLayerMask);
        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + rayDistanceToGround), Color.red);
        //Debug.Log(raycastHit.collider);  
        return raycastHit.collider != null;
    }

    public bool RaycastToWall()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, 
                                                        rayDir, 
                                                        collider.bounds.extents.y + rayDistanceToWall,
                                                        wallLayerMask);
        Debug.DrawRay(collider.bounds.center, rayDir * (collider.bounds.extents.y + rayDistanceToWall), Color.green);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    public bool RayCastToPlayer()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(eye.transform.position, rayDir, collider.bounds.extents.y + rayDistanceToPlayer, ~cameraColliderMask);
        //Debug.Log(raycastHit.collider);
        Debug.DrawRay(collider.bounds.center + (Vector3.one * 0.2f), rayDir * (collider.bounds.extents.y + rayDistanceToPlayer), Color.grey);
        if (raycastHit.collider)
        {
            distanceToPlayer = Mathf.Abs(raycastHit.collider.transform.position.x - transform.position.x);
            //Debug.Log(distanceToPlayer);
            //Debug.Log(raycastHit.collider.gameObject.name);
            if (raycastHit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public float GetDistanceToPlayer()
    {
        return distanceToPlayer;
    }




    private void OnDrawGizmos()
    {
        Bounds  boundCol = GetComponent<BoxCollider2D>().bounds;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(boundCol.center, boundCol.center + ((Vector3)rayDir * rayDistanceToWall));
        Gizmos.DrawLine(boundCol.center, boundCol.center + (Vector3.down * rayDistanceToGround));
    }


    public void HideExclamationMark()
    {
        ExclamationMark.gameObject.SetActive(false);
    }


    public void EnableExclamationMark()
    {
        ExclamationMark.gameObject.SetActive(true);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
       if (col.gameObject.tag == "Sandal" && col.gameObject.GetComponent<Sandal>().GetCurrentVelocity() >= sandalSpeedTreshold)
       {
            float sandalDamage = col.gameObject.GetComponent<Sandal>().GetDamage();
            hpBar.SetActive(true);


            PlayerComingFrom(player.GetDir());

            knocBackEffect();

            FindObjectOfType<AudioManager>().PlaySound("SandalHit_SFX");
            FindObjectOfType<AudioManager>().PlaySound("Cat_Damaged_SFX");

            GameObject popUpText = Instantiate(damagePopUpText, transform.position, Quaternion.identity, popUpContainer.transform) as GameObject;
            popUpText.GetComponent<DamagePopUp>().Setup(this.gameObject, sandalDamage);



            anim.SetTrigger("Damaged");

            isHit = true;
            healthScript.TakeDamage(sandalDamage);

            if(stateManager.GetCurrentState() != stateManager.GetAttackState())
            {
                stateManager.SwitchState(stateManager.GetAttackState(), "SetToAttack");
            }
       }
    }


    public bool GetIsHit()
    {
        return isHit;
    }

    public void SetCatPounce()
    {
        isJumping = true;
    }

    public void SetCatStopPounce()
    {
        isJumping = false;
    }

    public bool GetCatPounce()
    {
        return isJumping;
    }

    public void Attack()
    {
        FindObjectOfType<AudioManager>().PlaySound("CatAttack_SFX");
        if (attCollider.IsTouching(player.GetComponent<CapsuleCollider2D>()))
        {
            //Debug.Log("Hit Player");

            player.TakeDamage(attackPower);
            player.EnemyComingFrom(isRight);
            player.knocBackEffect();


        }
            
    }

    public void MoveTowardsPlayer()
    {
        anim.SetTrigger("SetToAttack");
    }
    
    void knocBackEffect()
    {
        Vector2 directionToKnockback = (Vector2)(currentKnockbackPoint.position - transform.position);
        rb.AddForce(directionToKnockback * knockBackPower, ForceMode2D.Impulse);
    }



    public void PlayerComingFrom(bool temp)
    {
        if (temp)
        {
            if (transform.localScale.x > 0f)
                currentKnockbackPoint = knockbackPlace[0];
            else
                currentKnockbackPoint = knockbackPlace[1];
        }
        else
        {
            if (transform.localScale.x < 0f)
                currentKnockbackPoint = knockbackPlace[0];
            else
                currentKnockbackPoint = knockbackPlace[1];
        }

    }

    public void PlayEnemyAttackSFX()
    {
        FindObjectOfType<AudioManager>().PlaySound("CatAttack_SFX");
    }
}
