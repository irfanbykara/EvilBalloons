using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public LevelManager levelManager;
    public SpawnScript spawnScript; // Reference to the SpawnScript

    public void LoadNextLevel()
    {
        if (levelManager.currentLevel < levelManager.totalLevels)
        {
            levelManager.currentLevel++;
            levelManager.enemySpeed *= 1.2f;
            levelManager.enemiesToSpawn = 4 + levelManager.currentLevel;  // Adjust the number of enemies based on the current level
            levelManager.spawnDelay -= 0.5f;  // Adjust spawn delay for added difficulty

            // Call the Initialize function in SpawnScript to start spawning enemies with the updated properties
            spawnScript.Initialize();
        }
        else
        {
            // Handle game completion or show a message that there are no more levels
        }
    }
}
