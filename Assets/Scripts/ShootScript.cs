using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour { 

    public LevelManager levelManager;  // Reference to the LevelManager script
    public GameObject arCamera;
    public GameObject smoke;
    private AudioSource _audioSource;
    private Image nextLevelImage;
    private GameObject nextLevelButton;

    private GameObject successSoundObject;
    private GameObject shootButton;
    private GameObject aim;
    private Image aimImage;
    private Image shootButtonImage;

    private Text scoreText;
    // Start is called before the first frame update
    private GameObject spawnScriptObject;
    private SpawnScript spawnScript;
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    void Start()
    {
        successSoundObject = GameObject.FindWithTag("successSound");

        nextLevelButton = GameObject.FindWithTag("nextLevelButton");
        nextLevelImage = nextLevelButton.GetComponent<Image>();

        aim = GameObject.FindWithTag("aim");
        aimImage = aim.GetComponent<Image>();

        shootButton = GameObject.FindWithTag("shootButton");
        shootButtonImage = shootButton.GetComponent<Image>();

        spawnScriptObject = GameObject.Find("SpawnScript");

        if (spawnScriptObject != null)
        {
            // Access the SpawnScript component attached to the GameObject
            spawnScript = spawnScriptObject.GetComponent<SpawnScript>();
        }
        else
        {
            Debug.LogError("GameObject with the name 'SpawnScript' not found.");
        }



        _audioSource = GetComponent<AudioSource>();

    }


    // Check if the hit object is an enemy
    private bool IsEnemy(Transform hitTransform)
    {
        return hitTransform.name == "enemy_1(Clone)" || hitTransform.name == "enemy_2(Clone)" || hitTransform.name == "enemy_3(Clone)";
    }

    public void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (IsEnemy(hit.transform))
            {
                if (!hitEnemies.Contains(hit.transform.gameObject))
                {
                  


                    // Mark the enemy as hit
                    hitEnemies.Add(hit.transform.gameObject);

                    Break_Ghost breakGhostComponent = hit.transform.GetComponent<Break_Ghost>();

                    if (breakGhostComponent != null)
                    {
                        breakGhostComponent.break_Ghost();
                    }

                    GameObject smokeEffect = Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));

                    if (spawnScript == null)
                    {
                        Debug.LogError("SpawnScript is not properly assigned.");
                    }


                    if (spawnScript != null)
                    {
                        int currentEnemyNumber = spawnScript.EnemyNumber;
                        spawnScript.EnemyNumber = currentEnemyNumber - 1;

                        int totalKill = spawnScript.TotalKill;
                        spawnScript.TotalKill = totalKill + 1;

                        
                    }
                    else
                    {
                        Debug.LogError("spawnScript is not properly assigned.");
                    }

                    // Destroy the smoke effect after a delay (e.g., 2 seconds)
                    float destroyDelay = 1.0f; // Adjust the delay as needed
                    Destroy(smokeEffect, destroyDelay);
                    Destroy(hit.transform.gameObject, destroyDelay / 2);

                }

            }

            }

            else
            {
                if (_audioSource == null)
                {
                    Debug.Log("null");
                }
                else
                {

                    _audioSource.Play();

                }

            }
        }
    }

