using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounceBehaviour : StateMachineBehaviour
{
    Player player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    Enemies enemy;

    Vector2 jumpPos;
    float currentPos;
    float targetX;
    float distance;
    float nextX;
    float baseY;
    float height;
    [SerializeField] float attackingTreshold = 2f;
    Vector2 targetPos;

    float walkingSpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<Enemies>();
        animator.GetComponentInChildren<BehaviourCollider>().AttackingState();
        player = FindObjectOfType<Player>();

        animator.SetBool("IsPatrolling", false);
        enemy.isMoving = false;
        jumpPos = enemy.transform.position;
        walkingSpeed = enemy.walkingSpeed;

        targetPos = GetTargetClosestToEnemy(player.GetComponentInChildren<TargetPoints>());
        Debug.Log("Current Target: " + targetPos);

        TurnToPlayerDir();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        CheckDistanceToPlayer(animator);
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    void CheckDistanceToPlayer(Animator animator)
    {
        Vector2 distance = (Vector2)enemy.transform.position - targetPos;
        Debug.Log(distance.x);
        if (Mathf.Abs(distance.x) <= attackingTreshold)
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    void TurnToPlayerDir()
    {
        Vector2 dir = player.transform.position - enemy.transform.position;
        if (dir.x < 0)
        {
            enemy.isRight = false;
        }
        else
        {
            enemy.isRight = true;
        }
    }

    void PounceToEnemy()
    {

        currentPos = enemy.transform.position.x;
        targetX = targetPos.x;

        distance = targetX - jumpPos.x;
        nextX = Mathf.MoveTowards(enemy.transform.position.x, targetX, walkingSpeed * Time.deltaTime);
        baseY = Mathf.Lerp(jumpPos.y, player.transform.position.y, (nextX - jumpPos.x) / distance);
        height = 2 * (nextX - jumpPos.x) * (nextX - targetX) / (-0.25f * distance * distance);

        Vector3 movePos = new Vector3(nextX, baseY + height, enemy.transform.position.z);
        enemy.transform.position = movePos;

    }

    Vector2 GetTargetClosestToEnemy(TargetPoints targetPoints)
    {
        float leastDistance = float.MaxValue;
        Vector2 selectedTarget = Vector2.zero;
        for (int i = 0; i < targetPoints.waypoints.Length; i++)
        {
            float temp = Mathf.Abs(targetPoints.waypoints[i].position.x - enemy.transform.position.x);
            if (temp < leastDistance)
            {
                leastDistance = temp;
                selectedTarget = targetPoints.waypoints[i].position;
            }
        }
        return selectedTarget;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
