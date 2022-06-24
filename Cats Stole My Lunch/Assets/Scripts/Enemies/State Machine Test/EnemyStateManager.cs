using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyStateBase currentState;
    public EnemyIdleState IdleState;
    public EnemyPatrolState PatrolState;
    public EnemyPounceState PounceState;
    public EnemyAttackState AttackState;
    public EnemyDetectionState DetectState;

    Player player;
    Enemies enemies;

    [SerializeField] private BehaviourCollider col;
    private BoxCollider2D boxCol;

    [Header("Idle Attributes")]
    [SerializeField] private float waitTime = 5f;

    [Header("Patrol Attributes")]
    [SerializeField] private float patrolTime = 5f;

    [Header("Pounce Attributes")]
    [SerializeField] private float distanceToChangeToAttack = 2f;

    [Header("Detection Attributes")]
    [SerializeField] private float enemyDetectionTime = 1f;
    [SerializeField] private float thresholdToPounce = 5f;


    [Header("Attacking Attributes")]
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] float moveForce = 10f;
    [SerializeField] BoxCollider2D attHitbox;

    [Header("Animator")]
    [SerializeField] private Animator anim;



    private void Awake()
    {
        player = FindObjectOfType<Player>();
        boxCol = GetComponent<BoxCollider2D>();
        enemies = GetComponent<Enemies>();
        
        IdleState = new EnemyIdleState(waitTime, col);
        PatrolState = new EnemyPatrolState(patrolTime, col);
        DetectState = new EnemyDetectionState(enemyDetectionTime, thresholdToPounce, col, anim);
        PounceState = new EnemyPounceState(distanceToChangeToAttack, col, player);
        AttackState = new EnemyAttackState(col,attackDamage,attackSpeed,moveForce, attHitbox,player,anim);
    }

    void Start()
    {      
        currentState = IdleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ExitState(EnemyStateBase state)
    {
        state.ExitState(this);
    }

    public void SwitchState(EnemyStateBase state, string animString)
    {    
        currentState = state;
        state.EnterState(this);

        anim.SetTrigger(animString);

    }

    public void SwitchToPounceState(EnemyStateBase state)
    {
        currentState = state;
        state.EnterState(this);
    }


    public EnemyStateBase GetCurrentState()
    {
        return currentState;
    }
    
    public EnemyStateBase GetAttackState()
    {
        return AttackState;
    }

}
