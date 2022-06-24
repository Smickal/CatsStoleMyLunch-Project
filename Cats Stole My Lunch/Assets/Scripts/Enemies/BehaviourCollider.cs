using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BehaviourCollider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_Text behaviourText;



    public void IdleState()
    {
        behaviourText.text = "IDLE STATE";

    }
    public void PatrolState()
    {
        behaviourText.text = "PATROL STATE";
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
