using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandal : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] public float rotationSpeed = 1000f;
    bool activateRotation = true;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(activateRotation)
            transform.localEulerAngles += transform.forward * rotationSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            activateRotation = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<Hand>().IncreaseSandalInPocket();
            Destroy(this.gameObject);
        }
    }
}
