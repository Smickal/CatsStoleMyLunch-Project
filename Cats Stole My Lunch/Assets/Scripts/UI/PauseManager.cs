using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject pausePanel;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] Button resumeButton;
    [SerializeField] GameObject pauseButtonContainer;

    [Header("Setting")]
    [SerializeField] Button settingButton;
    [SerializeField] GameObject settingContainer;
    [SerializeField] Button backFromSetting;

    float defaulTimeScale;

    private void Awake()
    {
        pausePanel.SetActive(false);
        defaulTimeScale = Time.timeScale;
        //DisablePauseButton();
    }
    
    public void EnablePausePanel()
    {
        text.text = "GAME PAUSED";
        resumeButton.gameObject.SetActive(true);


        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGamePressed()
    {
        pausePanel.SetActive(false);
        Time.timeScale = defaulTimeScale;
    }

    public void RestartGamePressed()
    {
        Time.timeScale = defaulTimeScale;
    }


    public void SettingButtonPressed()
    {
        settingContainer.gameObject.SetActive(true);
        pauseButtonContainer.gameObject.SetActive(false);

    }

    public void BackFromSettingButtonPressed()
    {
        settingContainer.gameObject.SetActive(false);
        pauseButtonContainer.gameObject.SetActive(true);
    }

    public void DisablePauseButton()
    {
        pauseButton.gameObject.SetActive(false);
    }

    public void EnablePauseButton()
    {
        pauseButton.gameObject.SetActive(true);
    }
}
