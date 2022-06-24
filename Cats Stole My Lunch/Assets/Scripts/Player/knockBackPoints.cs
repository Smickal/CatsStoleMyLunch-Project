using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockBackPoints : MonoBehaviour
{
    public Transform[] knocbackPoints;
    private void Awake()
    {
        knocbackPoints = new Transform[transform.childCount];
        for (int i = 0; i < knocbackPoints.Length; i++)
        {
            knocbackPoints[i] = transform.GetChild(i);
            
        }
    }
}
