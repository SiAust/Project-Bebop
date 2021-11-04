using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour
{
    int currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(currentScene + 1 == SceneManager.sceneCountInBuildSettings ? 0 : currentScene + 1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CollisionHandler.collisionDisabled = !CollisionHandler.collisionDisabled;
            // Debug.Log("C pressed");
            // Debug.Log("collisionDisabled=" + CollisionHandler.collisionDisabled);
        }
    }
}
