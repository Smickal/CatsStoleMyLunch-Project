using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    float timer = 0;
    [SerializeField] float maxTime = 0;
    public bool seeWall = false;
    Enemies enemyScript;
    Animator animator;
    BehaviourCollider col;


    public EnemyIdleState(float time, BehaviourCollider col)
    {
        this.maxTime = time;
        this.col = col;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        //Debug.Log("IDLE STATE CALLED!");
        col.ChangeText("IDLE STATE");
        enemyScript = enemy.GetComponent<Enemies>();
        animator = enemy.GetComponent<Animator>();


        timer = 0;
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Check Idle For Time
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
                //enemyScript.isRight = !enemyScript.isRight;
                enemy.ExitState(enemy.IdleState);
                enemy.SwitchState(enemy.PatrolState, "SetToPatrol");
        }

        //Check For player
        if (enemyScript.RayCastToPlayer())
        {
            enemy.SwitchState(enemy.DetectState, "SetToDetect");
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        seeWall = false;
        enemyScript.isRight = !enemyScript.isRight;
        //Debug.Log("exit idle");
    }


}
