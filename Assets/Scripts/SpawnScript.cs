using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class SpawnScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] spawnPoints;
    public GameObject[] balloons;
    private GameObject restartButton;
    private GameObject nextLevelButton;
    private static GameObject dyingSoundObject;

    private GameObject successSoundObject;
    private GameObject shootButton;
    private GameObject youWon;
    private GameObject playAgainButton;
    private GameObject scoreBoard;


    private GameObject aim;
    private Image shootButtonImage;
    private Image aimImage;
    private Image playAgainButtonImage;
    private RawImage scoreBoardImage;

    private GameObject gameOver;
     
    private Image restartbuttonImage;
    private Image nextLevelImage;
    private Image youWonImage;

    private Text totalEnemyNumber;

    private Text escapedEnemyText;
    private int counter;

    private int enemyNumber;

    private int escapedEnemyNumber;
    private int killNumber;

    private Text levelText;
    private GameObject levelTextObject;
    private Text scoreText;

    public LevelManager levelManager;  // Reference to the LevelManager script

    void Start()
    {

        Initialize();
    }

    public void Initialize()
    {
        successSoundObject = GameObject.FindWithTag("successSound");

        aim = GameObject.FindWithTag("aim");
        scoreBoard = GameObject.FindWithTag("scoreBoard");

        shootButton = GameObject.FindWithTag("shootButton");
        youWon = GameObject.FindWithTag("youWon");
        playAgainButton = GameObject.FindWithTag("playAgain");
        playAgainButtonImage = playAgainButton.GetComponent<Image>();

        scoreBoardImage = scoreBoard.GetComponent<RawImage>();

        scoreBoardImage.enabled = true;
        playAgainButtonImage.enabled = false;
        youWonImage = youWon.GetComponent<Image>();
        aimImage = aim.GetComponent<Image>();
        shootButtonImage = shootButton.GetComponent<Image>();
        aimImage.enabled = true;
        shootButtonImage.enabled = true;

        GameObject levelTextObject = GameObject.FindGameObjectWithTag("levelTag");
        levelText = levelTextObject.GetComponentInChildren<Text>();
        levelText.text = "Level: " + levelManager.currentLevel;
        enemyNumber = 0;
        escapedEnemyNumber = 0;
        killNumber = 0;
        GameObject enemyNumberObject = GameObject.FindGameObjectWithTag("totalEnemiesTag");

        totalEnemyNumber = enemyNumberObject.GetComponentInChildren<Text>();

        totalEnemyNumber.text = enemyNumber.ToString();

        GameObject scoreObject = GameObject.FindGameObjectWithTag("scoreTag");

        scoreText = scoreObject.GetComponentInChildren<Text>();

        scoreText.text = enemyNumber.ToString();

        GameObject escapedEnemyObject = GameObject.FindGameObjectWithTag("escapedEnemyTag");

        escapedEnemyText = escapedEnemyObject.GetComponentInChildren<Text>();

        enemyNumber = 0;
        counter = 0;

        restartButton = GameObject.FindWithTag("restartButton");

        restartbuttonImage = restartButton.GetComponent<Image>();
        restartbuttonImage.enabled = false;
        youWonImage.enabled = false;
        nextLevelButton = GameObject.FindWithTag("nextLevelButton");

        nextLevelImage = nextLevelButton.GetComponent<Image>();
        nextLevelImage.enabled = false;

        dyingSoundObject = GameObject.FindWithTag("dyingSoundObject");

        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(levelManager.spawnDelay);
        if (counter < levelManager.enemiesToSpawn)
        {
            List<int> randoms = new List<int> { 0, 1, 2, 3, 4, 5};

            for (int i = 0; i < 3; i++)
            {
                enemyNumber++;
                totalEnemyNumber.text = "" + enemyNumber.ToString();

                int randomIndex = Random.Range(0, randoms.Count-1);
                int randomElement = randoms[randomIndex];

                randoms.Remove(randomElement);

                Instantiate(balloons[i], spawnPoints[randomElement].position, spawnPoints[i].rotation);
            }

            counter++;
            StartCoroutine(StartSpawning());


        }

    }

    // Update is called once per frame
    void Update()
    {
        if ((this.TotalKill + this.EscapedEnemyNumber) == levelManager.enemiesToSpawn * 3 && (this.TotalKill > levelManager.enemiesToSpawn * 3 / 4 * 3))
        {
            successSoundObject.GetComponent<AudioSource>().Play();
            shootButtonImage.enabled = false;
            aimImage.enabled = false;

            if (levelManager.currentLevel == levelManager.totalLevels)
            {
                scoreBoardImage.enabled = false;
                playAgainButtonImage.enabled = true;
                youWonImage.enabled = true;

                BalloonScript.EndGame(successSoundObject.GetComponent<AudioSource>());
                restartbuttonImage.enabled = false;

            }
            else
            {
                nextLevelImage.enabled = true;
                this.EscapedEnemyNumber = 0;
                this.EnemyNumber = 0;
                this.TotalKill = 0;

            }
        }

        else if ((this.TotalKill + this.EscapedEnemyNumber + this.EnemyNumber) == levelManager.enemiesToSpawn * 3 && this.EscapedEnemyNumber >= levelManager.enemiesToSpawn * 3 / 2)
        {
            BalloonScript.EndGame(dyingSoundObject.transform.GetComponent<AudioSource>());

        }


    }


    public int EnemyNumber // Public property to access enemyNumber
    {
        get
        {
            return enemyNumber;
        }
        set
        {
            enemyNumber = value;
            totalEnemyNumber.text = enemyNumber.ToString();
        }
    }

    public int TotalKill // Public property to access enemyNumber
    {
        get
        {
            return killNumber;
        }
        set
        {
            killNumber = value;
            scoreText.text = "" + killNumber.ToString();
        }
    }


    public int EscapedEnemyNumber // Public property to access enemyNumber
    {
        get
        {
            return escapedEnemyNumber;
        }
        set
        {
            escapedEnemyNumber = value;
            escapedEnemyText.text = "" + escapedEnemyNumber.ToString();
        }
    }



}
