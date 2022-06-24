using UnityEngine;

public class EnemyPatrolState : EnemyStateBase
{
    float timer = 0;
    [SerializeField] float maxTime = 0;
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
        col.ChangeText("PATROL STATE");
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
            enemy.SwitchState(enemy.IdleState, "SetToIdle");
        }
        //check if cat see player
        if (enemyScript.RayCastToPlayer())
        {
            enemyScript.isMoving = false;
            enemy.SwitchState(enemy.DetectState, "SetToIdle");
        }
    }




    public override void ExitState(EnemyStateManager enemy)
    {

    }   
}
