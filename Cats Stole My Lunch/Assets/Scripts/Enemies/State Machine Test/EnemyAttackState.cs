using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{
    BehaviourCollider col;
    Player player;
    Enemies enemyScript;
    BoxCollider2D attHitbox;
    Animator anim;

    //Cat Attack Attributes
    float attackDamage;
    float attackSpeed;
    float moveForce;



    public EnemyAttackState(BehaviourCollider col, float attackDamage, float attackSpeed, float moveSpeed, BoxCollider2D boxCol, Player player, Animator anim)
    {
        this.col = col;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.moveForce = moveSpeed;
        attHitbox = boxCol;
        this.player = player;
        this.anim = anim;
        
    }


    public override void EnterState(EnemyStateManager enemy)
    {
        col.ChangeText("Attacking State!");
        enemyScript = enemy.gameObject.GetComponent<Enemies>();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Move To Player
        if (player)
            Attack(enemy);
        else
            enemy.SwitchState(enemy.IdleState, "SetToIdle");
        //Attack When Close


    }

    public override void ExitState(EnemyStateManager enemy)
    {

    }

    void Attack(EnemyStateManager enemy)
    {
        //find Closest Target
        float distanceToPlayerX =  Mathf.Abs(player.transform.position.x - enemy.transform.position.x);
        float distanceToPlayerY = Mathf.Abs(player.transform.position.y - enemy.transform.position.y);

        float distanceToPlayer = Mathf.Sqrt(distanceToPlayerX * distanceToPlayerX + distanceToPlayerY * distanceToPlayerY);

        //Debug.Log(distanceToPlayer);
        TurnToPlayerDir();
        //Walk Towards Target Position
        enemyScript.isMoving = true;
        //Gebuk musuh
        if (distanceToPlayer <= 1.4f)
        {
            
            //Debug.Log("CLAW ENEMY!");
            enemyScript.isMoving = false;
            anim.SetTrigger("SetToClaw");
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


}
