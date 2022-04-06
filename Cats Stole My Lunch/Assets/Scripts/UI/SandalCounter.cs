using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SandalCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Hand hand;
    [SerializeField] Text sandalCountText;
    void Start()
    {
        hand = FindObjectOfType<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplaySandalCounter();
    }

    void DisplaySandalCounter()
    {
        sandalCountText.text = hand.GetSandalLeftInPocket().ToString();
    }
}
