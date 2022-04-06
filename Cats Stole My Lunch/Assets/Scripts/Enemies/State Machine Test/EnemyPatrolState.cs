using UnityEngine;

public class EnemyPatrolState : EnemyStateBase
{
    float timer = 0;
    [SerializeField] float maxTime = 0;
    IdleBehaviour idleBehaviour;
    Enemies enemyScript;
    Animator animator;
    BehaviourCollider col;

    public EnemyPatrolState(float time, BehaviourCollider col)
    {
        maxTime = time;
        this.col = col;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        col.ChangeText("Patrol State");
        enemyScript = enemy.gameObject.GetComponent<Enemies>();
        animator = enemy.gameObject.GetComponent<Animator>();

        enemyScript.isMoving = true;
        timer = 0;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        timer += Time.deltaTime;
        //check if hit wall or more than time
        if (enemyScript.RaycastToWall() || timer > maxTime)
        {
            enemyScript.isMoving = false;
            animator.SetBool("IsPatrolling", false);
            enemy.SwitchState(enemy.IdleState);
        }
        //check if cat see player
        if (enemyScript.RayCastToPlayer())
        {
            enemyScript.isMoving = false;
            enemy.SwitchState(enemy.DetectState);
        }
    }




    public override void ExitState(EnemyStateManager enemy)
    {

    }   
}
