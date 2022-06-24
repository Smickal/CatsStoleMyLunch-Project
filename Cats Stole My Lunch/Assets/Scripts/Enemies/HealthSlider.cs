using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider healthSlider;
    HealthScript healthScript;
    private void Awake()
    {
        healthScript = GetComponent<HealthScript>();
        healthSlider.maxValue = healthScript.GetMaxHP();  
    }

    private void Update()
    {
        healthSlider.value = healthScript.GetCurrentHP();
    }


}
