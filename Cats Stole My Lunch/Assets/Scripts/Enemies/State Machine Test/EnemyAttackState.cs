using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{
    BehaviourCollider col;
    Player player;
    Enemies enemyScript;
    BoxCollider2D attHitbox;
    //Cat Attack Attributes
    float attackDamage;
    float attackSpeed;
    float moveForce;



    public EnemyAttackState(BehaviourCollider col, float attackDamage, float attackSpeed, float moveSpeed, BoxCollider2D boxCol, Player player)
    {
        this.col = col;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.moveForce = moveSpeed;
        attHitbox = boxCol;
        this.player = player;
        
    }


    public override void EnterState(EnemyStateManager enemy)
    {
        col.ChangeText("Attacking State!");
        enemyScript = enemy.gameObject.GetComponent<Enemies>();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Move To Player
        MoveTowardsPlayer(enemy);
        //Attack Player
        if(Input.GetKeyDown(KeyCode.L))
            Attack();
    }

    public override void ExitState(EnemyStateManager enemy)
    {

    }

    void MoveTowardsPlayer(EnemyStateManager enemy)
    {
        //find Closest Target
        float distanceToPlayer =  Mathf.Abs(player.transform.position.x - enemy.transform.position.x);
        TurnToPlayerDir();
        //Walk Towards Target Position
        enemyScript.isMoving = true;

        if( distanceToPlayer <= 3.5f)
        {
            //Debug.Log("CLAW ENEMY!");
            enemyScript.isMoving = false;
        }
    }

    void TurnToPlayerDir()
    {
        Vector2 dir = player.transform.position - enemyScript.transform.position;
        if (dir.x < 0)
        {
            enemyScript.isRight = false;
        }
        else
        {
            enemyScript.isRight = true;
        }
    }

    void Attack()
    {
        if (attHitbox.IsTouching(player.GetComponent<BoxCollider2D>()))
        {
            Debug.Log("Hit Player");
        }
    }

}
