using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoints : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] waypoints;

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
            Debug.Log(waypoints[i].gameObject + " :" + waypoints[i].transform.position);
        }
    }

}
