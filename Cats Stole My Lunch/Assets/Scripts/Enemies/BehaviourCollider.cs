using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BehaviourCollider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_Text behaviourText;
    [SerializeField] Animator animator;
    [SerializeField] Enemies enemy;

    [Header("Collider Size")]
    [SerializeField] BoxCollider2D behaviourCollider;
    [SerializeField] Vector2 idleStateCollider;
    [SerializeField] Vector2 attackingStateCollider;

    private void Start()
    {
        
    }

    private void Update()
    {

    }
    public void IdleState()
    {
        behaviourText.text = "IDLE STATE";

        //change Behaviour Collider to Idle State
        behaviourCollider.size = idleStateCollider;

    }
    public void PatrolState()
    {
        behaviourText.text = "PATROL STATE";

        //temporary will remove and edit later
        behaviourCollider.size = attackingStateCollider;
    }

    public void AttackingState()
    {
        behaviourText.text = "ATTACKING STATE";
    }

    public void ChangeText(string Text)
    {
        behaviourText.text = Text;
    }
}
