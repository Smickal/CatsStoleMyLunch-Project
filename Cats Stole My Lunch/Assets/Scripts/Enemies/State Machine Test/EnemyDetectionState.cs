using UnityEngine;

public class EnemyDetectionState : EnemyStateBase
{
    Enemies enemyScript;
    BehaviourCollider col;

    float timer = 0;
    float maxTime;
    float thresholToPounce;
    public EnemyDetectionState(float maxTime, float pounceThres, BehaviourCollider col)
    {
        this.maxTime = maxTime;
        thresholToPounce = pounceThres;
        this.col = col;
    }
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("I SEE PLAYEWR!");
        enemyScript = enemy.gameObject.GetComponent<Enemies>();
        col.ChangeText("Detection State");
        
        enemyScript.EnableExclamationMark();
        timer = 0;
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        timer += Time.deltaTime;
        if(timer >= maxTime)
        {
            enemyScript.HideExclamationMark();
            //ngecek lagi ada player ato kgk
            if (enemyScript.RayCastToPlayer())
            {
                //ngecek apakah player berada di jarak tertentu
                if (enemyScript.GetDistanceToPlayer() >= thresholToPounce)
                {
                    enemy.SwitchState(enemy.PounceState);
                }
                else
                {
                    enemy.SwitchState(enemy.AttackState);
                }
            }
            else
            {
                //balik ke idle
                enemy.SwitchState(enemy.IdleState);
            }
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemyScript.HideExclamationMark();
    }
}
