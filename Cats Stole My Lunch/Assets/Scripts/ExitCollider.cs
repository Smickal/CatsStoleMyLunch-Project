using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCollider : MonoBehaviour
{
    // Start is called before the first frame update
    SceneManagement sceneManagement;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sceneManagement = FindObjectOfType<SceneManagement>();
            //sceneManagement.LoadNextScene();
            sceneManagement.ReturnToMainMenu();
        }
    }

}
