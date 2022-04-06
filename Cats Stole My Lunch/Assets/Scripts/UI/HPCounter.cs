using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HPCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TextMeshProUGUI hpText;
    public void SetCurrentText(string text)
    {
        hpText.text = text;
    }
}
