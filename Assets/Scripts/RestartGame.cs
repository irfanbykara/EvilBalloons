using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public SpawnScript spawnScript; // Assign the SpawnScript in the Unity Editor

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Restart()
    {
        // Reset relevant variables and game state
        // Set scores, health, positions, etc. back to initial values

        // Reload the current scene to start from the beginning
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        spawnScript.Initialize();
    }


}
