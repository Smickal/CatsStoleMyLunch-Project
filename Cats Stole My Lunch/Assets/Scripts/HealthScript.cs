using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float healthPoint;
    [SerializeField] HPCounter hpCounter;

    float currHP;


    private void Start()
    {
        currHP = healthPoint;
    }

    public void TakeDamage(float damage)
    {
        currHP -= damage;
        if(currHP <= 0)
        {
            Destroy(gameObject, 1f);
            
        }
    }

    public float GetCurrentHP()
    {
        return currHP;
    }

    public float GetMaxHP()
    {
        return healthPoint;
    }

    public void SetCurrentHPToDisplay()
    {
        hpCounter.SetCurrentText(currHP.ToString());
    }
}
