using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int totalLevels = 10;  // Adjust this to the total number of levels in your game
    public int enemiesToSpawn = 4;  // Default number of enemies to spawn
    public float spawnDelay = 4.0f;  // Default spawn delay
    public float enemySpeed = 0.04f;

}
