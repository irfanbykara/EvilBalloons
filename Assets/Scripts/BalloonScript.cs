using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;


public class BalloonScript : MonoBehaviour
{
    private Transform cameraTransform;
    private static GameObject aim;
    private static GameObject shootButton;
    private static Image restartbuttonImage;
    private GameObject restartButton;
    private Break_Ghost breakGhostComponent;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    private GameObject spawnScriptObject;
    private SpawnScript spawnScript;

    private GameObject levelManagerObject;
    private LevelManager levelManager;
    [SerializeField]
    private float destroyYDistanceThreshold = 4.0f; // Adjust the threshold as needed


    private static GameObject dyingSoundObject;

    private static int globalDamageCounter;


    void Start()
    {

        levelManagerObject = GameObject.FindWithTag("levelManagerTag");

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

        if (levelManagerObject != null)
        {
            // Access the SpawnScript component attached to the GameObject
            levelManager = levelManagerObject.GetComponent<LevelManager>();
        }
        else
        {
            Debug.LogError("GameObject with the name 'LevelManagerScript' not found.");
        }


        Vector3 bloodPosition = new Vector3(0, 0, 0);
        
        // Define the specific rotation you want using Euler angles
        Vector3 specificRotation = new Vector3(180, 180, 180); // Replace xAngle, yAngle, and zAngle with your desired rotation values

        // Convert Euler angles to a Quaternion
        Quaternion rotation = Quaternion.Euler(specificRotation);
        


        globalDamageCounter = 0;
        _audioSource = gameObject.GetComponent<AudioSource>();

        breakGhostComponent = gameObject.transform.GetComponent<Break_Ghost>();
        restartButton = GameObject.FindWithTag("restartButton");
        restartbuttonImage = restartButton.GetComponent<Image>();

        dyingSoundObject = GameObject.FindWithTag("dyingSoundObject");
        
        aim = GameObject.FindWithTag("aim");

        shootButton = GameObject.FindWithTag("shootButton");


        // Find the AR camera in the scene
        cameraTransform = Camera.main.transform;
    }


    // Update is called once per frame
    void Update()

      {

        Vector3 relativePosition = cameraTransform.position - transform.position;

        // Normalize the direction vector

        // Move the object towards the camera
       transform.Translate(relativePosition * levelManager.enemySpeed * Time.deltaTime * -1);

       transform.Translate(Vector3.up * Time.deltaTime * levelManager.enemySpeed);
        // Calculate the z-difference between the camera and the balloon
        float yDifference = Mathf.Abs(cameraTransform.position.y - transform.position.y);

        float distance = Vector3.Distance(cameraTransform.position, transform.position);
        // Check if the balloon has flown above the destroyDistanceThreshold
        if (yDifference > destroyYDistanceThreshold)
        {
            if (spawnScript == null)
            {
                Debug.LogError("SpawnScript is not properly assigned.");
            }


            if (spawnScript != null)
            {
                int escapedEnemyNumber = spawnScript.EscapedEnemyNumber;
                spawnScript.EscapedEnemyNumber = escapedEnemyNumber + 1;
                int currentEnemyNumber = spawnScript.EnemyNumber;
                spawnScript.EnemyNumber = currentEnemyNumber - 1;


            }

            Destroy(gameObject); // Destroy the balloon
        }

        _audioSource.volume = 1 / (distance*distance) ;

        if (distance > 3 && distance < 4)
        {
            breakGhostComponent.play_anim("attack");
        }
        else if (distance < 1.2)
        {

            globalDamageCounter++;
            //GameObject bloodEffect = Instantiate(blood, bloodPosition, rotation);
           // float destroyDelay = 1.0f; // Adjust the delay as needed
           // Destroy(bloodEffect, destroyDelay);

        }
        if (globalDamageCounter > 35)
        {
            EndGame(dyingSoundObject.transform.GetComponent<AudioSource>());

        }

    }
    public static void DestroyObjectsWithBalloonScript()
    {
        // Find all objects with BalloonScript attached
        BalloonScript[] balloonScripts = FindObjectsOfType<BalloonScript>();

        // Destroy each object with BalloonScript
        foreach (BalloonScript balloonScript in balloonScripts)
        {
            Destroy(balloonScript.gameObject);
        }
    }

     public static void EndGame(AudioSource audiosource)
    {
        audiosource.Play();
        shootButton.SetActive(false);
        aim.SetActive(false);
        restartbuttonImage.enabled = true;
        // The distance is below the threshold, stop the game
        // The distance is below the thredebugshold, stop the game
        DestroyObjectsWithBalloonScript();

    }
    public static int GlobalDamageCounter
    {
        get { return globalDamageCounter; }
    }

}
