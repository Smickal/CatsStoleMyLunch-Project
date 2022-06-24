using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SandalCooldown : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image circleFill;
    float timer, currentTimer;

    bool isTimerStart = false;

    private void Update()
    {
        BeginCoolDown();
    }

    private void BeginCoolDown()
    {
        if (isTimerStart)
        {
            currentTimer += Time.deltaTime;
            circleFill.fillAmount = currentTimer / timer * 1f;

            if (currentTimer >= timer) isTimerStart = false;
        }
    }

    public void StartTimer(float time)
    {
        timer = time;
        currentTimer = 0;
        isTimerStart = true;
    }

    public float  GetTimer()
    {
        return currentTimer;
    }

}
