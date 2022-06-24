using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuContainer;
    [SerializeField] GameObject settingContainer;


    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject panel;


    public void SettingButtonPressed()
    {
        SetMainMenu(false);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void BackFromSettingPressed()
    {
        SetMainMenu(true);
    }


    private void SetMainMenu(bool temp)
    {
        if(temp)
        {
            mainMenuContainer.SetActive(true);
            settingContainer.SetActive(false);
        }
        else
        {
            mainMenuContainer.SetActive(false);
            settingContainer.SetActive(true);
        }
    }

    public void DisableInGameMenu()
    {
        menuPanel.SetActive(false);
    }
    public void EnableInGameMenu()
    {
        menuPanel.SetActive(true);
    }
}
