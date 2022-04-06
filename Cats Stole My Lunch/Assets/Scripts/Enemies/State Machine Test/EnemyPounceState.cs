using UnityEngine;

public class EnemyPounceState : EnemyStateBase
{
    Player player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    Enemies enemyScript;
    BehaviourCollider col;
    Vector2 jumpPos;
    float currentPos;
    float targetX;
    float distance;
    float nextX;
    float baseY;
    float height;
    float attackingTreshold = 1f;
    Vector2 targetPos;

    float walkingSpeed;
    Animator animator;
    public EnemyPounceState(float distanceToPlayer, BehaviourCollider col, Player player)
    {
        attackingTreshold = distanceToPlayer;
        this.col = col;
        this.player = player;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        enemyScript = enemy.gameObject.GetComponent<Enemies>();
        animator = enemy.gameObject.GetComponent<Animator>();
        col.ChangeText("Pounce State");

        animator.SetBool("IsPatrolling", false);
        enemyScript.isMoving = false;
        jumpPos = enemyScript.transform.position;
        walkingSpeed = enemyScript.walkingSpeed;

        targetPos = GetTargetClosestToEnemy(player.GetComponentInChildren<TargetPoints>());
        Debug.Log("Current Target: " + targetPos);

        TurnToPlayerDir();
        //enemyScript.isAttacking = true;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        PounceToEnemy();
        //enemyScript.isAttacking = true;
        CheckDistanceToPlayer(enemy);
    }
    public override void ExitState(EnemyStateManager enemy)
    {
        enemyScript.isAttacking = false;
    }

    Vector2 GetTargetClosestToEnemy(TargetPoints targetPoints)
    {
        float leastDistance = float.MaxValue;
        Vector2 selectedTarget = Vector2.zero;
        for (int i = 0; i < targetPoints.waypoints.Length; i++)
        {
            float temp = Mathf.Abs(targetPoints.waypoints[i].position.x - enemyScript.transform.position.x);
            if (temp < leastDistance)
            {
                leastDistance = temp;
                selectedTarget = targetPoints.waypoints[i].position;
            }
        }
        return selectedTarget;
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

    void PounceToEnemy()
    {

        currentPos = enemyScript.transform.position.x;
        targetX = targetPos.x;

        distance = targetX - jumpPos.x;
        nextX = Mathf.MoveTowards(enemyScript.transform.position.x, targetX, walkingSpeed * Time.deltaTime);
        baseY = Mathf.Lerp(jumpPos.y, player.transform.position.y, (nextX - jumpPos.x) / distance);
        height = 2 * (nextX - jumpPos.x) * (nextX - targetX) / (-0.25f * distance * distance);

        Vector3 movePos = new Vector3(nextX, baseY + height, enemyScript.transform.position.z);
        enemyScript.transform.position = movePos;

    }

    void CheckDistanceToPlayer(EnemyStateManager enemy)
    {
        Vector2 distance = (Vector2)enemyScript.transform.position - targetPos;
        //Debug.Log(distance.x);
        if (Mathf.Abs(distance.x) <= attackingTreshold)
        {
            enemy.ExitState(enemy.PounceState);
            enemy.SwitchState(enemy.AttackState);
        }
    }
}
