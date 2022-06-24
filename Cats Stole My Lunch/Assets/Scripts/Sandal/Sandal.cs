using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sandal : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] public float rotationSpeed = 1000f;
    [SerializeField] TextMeshProUGUI velocity;
    bool activateRotation = true;

    [Header("PowerUp")]
    [SerializeField] bool isAPowerUp = false;
    [SerializeField] GameObject parent;


    Rigidbody2D rb;
    Vector2 prevPos;

    int currentVelocity;
    float sandalDamage;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(activateRotation)
            transform.localEulerAngles += transform.forward * rotationSpeed;

        prevPos = transform.position;


        //Debug.Log(Mathf.RoundToInt(Vector2.Distance(transform.position, prevPos)/ Time.fixedDeltaTime));
        //Debug.Log(rb.velocity);
        currentVelocity = (int)Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y));
        //Debug.Log(currentVelocity);

    }

    public int GetCurrentVelocity()
    {
        return currentVelocity;
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
            FindObjectOfType<AudioManager>().PlaySound("SandalPick_SFX");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<Hand>().IncreaseSandalInPocket();
            FindObjectOfType<AudioManager>().PlaySound("SandalPick_SFX");
            Destroy(parent);
        }
    }

    public void SetDamage(float damage)
    {
        sandalDamage = damage;
    }

    public float GetDamage()
    {
        return sandalDamage;
    }
}