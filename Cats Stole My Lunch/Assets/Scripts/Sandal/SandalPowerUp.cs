using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandalPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    Sandal sandal;

    private void Awake()
    {
        sandal = GetComponentInChildren<Sandal>();
    }
}
