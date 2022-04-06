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

    [Header("LayerMask")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] LayerMask playerLayerMask;

    [SerializeField] Transform eye;

    [HideInInspector]public bool isMoving = false;
    [HideInInspector] public bool isRight = true;
    [HideInInspector] public bool isAttacking = false;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        HideExclamationMark();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        RaycastToGround();
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
        return raycastHit.collider != null;
    }

    public bool RayCastToPlayer()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(eye.transform.position, rayDir, collider.bounds.extents.y + rayDistanceToPlayer);
        Debug.DrawRay(collider.bounds.center + (Vector3.one * 0.2f), rayDir * (collider.bounds.extents.y + rayDistanceToPlayer), Color.grey);
        if (raycastHit.collider)
        {
            distanceToPlayer = Mathf.Abs(raycastHit.collider.transform.position.x - transform.position.x);
            //Debug.Log(distanceToPlayer);
            Debug.Log(raycastHit.collider.gameObject.name);
            if (raycastHit.collider.gameObject.name == player.gameObject.name)
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

}
