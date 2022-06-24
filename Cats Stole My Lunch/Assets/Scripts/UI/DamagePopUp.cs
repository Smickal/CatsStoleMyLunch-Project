using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    GameObject enemy;
    Vector3 offset = new Vector3(0, 3f, 0f);
    float randomizeXpoints = 3f;

    Vector3 startScale;

    TextMeshPro textMesh;
    float time = 0;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        startScale = transform.localScale;
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
        transform.position += offset;

        transform.position += new Vector3(Random.Range(-randomizeXpoints, randomizeXpoints), 0f, 0f);

    }


    private void Update()
    {
        if(enemy)
        {
            UpdateTextRotation();
        }
    }

    private void UpdateTextRotation()
    {

        if (enemy.transform.localScale.x < 0f)
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(-1f, 1f, 0);
        else
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0);
    }


    public void Setup(GameObject enemy, float damage)
    {
        this.enemy = enemy;
        textMesh.text = damage.ToString();
    }

}
