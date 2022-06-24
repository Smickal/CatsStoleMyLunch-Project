using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject transitionGameObjects;


    public Animator transition;
    public float transitionTime = 1f;

    [Header("Manager")]
    [SerializeField] MainMenuButtonManager mmManager;
    [SerializeField] PauseManager pManager;

    private void Awake()
    {
        transitionGameObjects.SetActive(true);
    }

    public void ResetCurrentScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void PlayGamePressed()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int level)
    {
        transition.SetTrigger("Start");


        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
        transition.SetTrigger("End");

        if(level > 0)
        {
            mmManager.DisableInGameMenu();
            pManager.EnablePauseButton();
        }
        else
        {
            mmManager.EnableInGameMenu();
            pManager.DisablePauseButton();
        }

    }

    public void LoadNextScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(scene));
    }


}
