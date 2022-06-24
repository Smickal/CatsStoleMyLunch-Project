using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeathPanel : MonoBehaviour
{

    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] Button resumeButton;
    float defaultTimeScale;

    private void Awake()
    {
        defaultTimeScale = Time.timeScale;
    }
    public void EnableDeathPanel()
    {
        text.text = "YOU DIED!";
        resumeButton.gameObject.SetActive(false);


        panel.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void DisableDeathPanel()
    {
        panel.SetActive(false);
        Time.timeScale = defaultTimeScale;
    }
}
