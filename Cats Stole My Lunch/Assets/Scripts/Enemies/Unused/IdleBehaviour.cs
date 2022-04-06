using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IdleBehaviour : StateMachineBehaviour
{
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    float timer = 0;
    [SerializeField] float maxTime = 0;
    bool turnBack = false;
    public bool seeWall = false;
    Enemies enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponentInChildren<BehaviourCollider>().IdleState();
        enemy = animator.gameObject.GetComponent<Enemies>();
        timer = 0;
        turnBack = false;
       
        if(seeWall)
        {
            enemy.isRight = !enemy.isRight;
            turnBack = true;
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        timer += Time.deltaTime;
        if(timer >= maxTime && !turnBack) {
            if (!seeWall)
            {
                enemy.isRight = !enemy.isRight;
                animator.SetBool("IsPatrolling", true);
                turnBack = true;
                
            }
            else
            {
                animator.SetBool("IsPatrolling", true);
            }
            turnBack = true;
        }

        if(timer >= maxTime && turnBack)
        {
            animator.SetBool("IsPatrolling", true);
        }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seeWall = false;
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
