using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{

    public string gameplaySceneName = "SampleScene"; // Name of your gameplay scene

    private void Start()
    {
        //Debug.Log("Hello World!");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
        gameObject.SetActive(false);

    }
}
