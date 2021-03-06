using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PatrolState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
     float timer = 0;
    [SerializeField] float maxTime = 0;
    IdleBehaviour idleBehaviour;
    Enemies enemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.gameObject.GetComponentInChildren<BehaviourCollider>().PatrolState();
        idleBehaviour = animator.GetBehaviour<IdleBehaviour>();
        enemy = animator.gameObject.GetComponent<Enemies>();
        enemy.isMoving = true;
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(enemy.RaycastToWall())
        {
            idleBehaviour.seeWall = true;
            animator.SetBool("IsPatrolling", false);
            enemy.isMoving = false;
        }

        if(timer > maxTime)
        {
            animator.SetBool("IsPatrolling", false);
            enemy.isMoving = false;
        }


        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        

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
