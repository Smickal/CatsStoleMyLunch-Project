using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Throw")]
    bool activateThrow = false;
    [SerializeField] Transform playerDir;
    [SerializeField] float throwForce;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject sandal;
    [SerializeField] int maxSandalSpawned = 2;
    [SerializeField] SandalCooldown sd;
    float sandalDamage;
    float sandalCldwnTimer;
    int currentSandalSpawned = 0;
    bool isCooldownOver = true;

    Vector2 mousePos;

    [Header("Point")]
    [SerializeField] GameObject pointerSpawn;
    public GameObject pointPrefab;
    private GameObject[] points;
    public int numberOfPoints;
    public float rangeOfPoints;
    float force = 12f;

    [Header("PlayerScript")]
    [SerializeField] Player player;

    Vector2 dir;

    private void Awake()
    {
        
        pointerSpawn.transform.position = transform.position;

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, pointerSpawn.transform);
        }

        sandalDamage = player.sandalDamage;
        sandalCldwnTimer = player.sandalCooldown;


        force = 0.3125f * throwForce;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            activateThrow = !activateThrow;
        }

        if (activateThrow)
        {
            AimAtMouse();
            ThrowSandal();
        }
        else
        {
            ResetSandalPosition();
            player.EnablePlayerMovement();
        }
        TurnOnOrOffAssistPoint();

        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = PointPosition(i * rangeOfPoints);
        }

        if(!isCooldownOver)
        {
            if(sd.GetTimer() >= sandalCldwnTimer)
            {
                isCooldownOver = true;
            }
        }
    }

    void TurnOnOrOffAssistPoint()
    {
        if(activateThrow)
        {
            pointerSpawn.SetActive(true);
        }
        else
        {
            pointerSpawn.SetActive(false);
        }
    }

    void ResetSandalPosition()
    {
        transform.right = Vector3.zero;
    }

    private void AimAtMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = mousePos - transform.position;
        transform.right = dir;
    }

    private void ThrowSandal()
    {
        if (Input.GetButtonDown("Fire1") && currentSandalSpawned < maxSandalSpawned && isCooldownOver)
        {
            isCooldownOver = false;
                      
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - transform.position;
            if (CheckIfPlayerDirAndThowDir(dir.x, playerDir.localScale.x))
            {
                sd.StartTimer(sandalCldwnTimer);

                FindObjectOfType<AudioManager>().PlaySound("SandalThrow_SFX");

                GameObject sandalIns = Instantiate(sandal, transform.position, transform.rotation);
                sandalIns.GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
                sandalIns.GetComponent<Sandal>().SetDamage(sandalDamage);

                if (playerDir.localScale.x > 0)
                {
                    sandalIns.GetComponent<Sandal>().rotationSpeed = -rotationSpeed;
                }
                else
                {
                    sandalIns.GetComponent<Sandal>().rotationSpeed = rotationSpeed;
                }

                currentSandalSpawned++;
            }
        }
    }

    bool CheckIfPlayerDirAndThowDir(float mousePos, float playerDir)
    {
        if (mousePos > 0 && playerDir > 0 || mousePos < 0 && playerDir < 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    Vector2 PointPosition(float t)
    {
        Vector2 currPosOfPoint = (Vector2)transform.position + (dir.normalized * force * t) + 0.5f * Physics2D.gravity * (t*t);
        return currPosOfPoint;
    }

    public int GetSandalLeftInPocket()
    {
        return maxSandalSpawned - currentSandalSpawned;
    }

    public void IncreaseSandalInPocket()
    {
        currentSandalSpawned--;
    }

}
