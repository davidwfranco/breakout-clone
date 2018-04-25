using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    // Declaring all the variables to the script behavior
	public static GameController instance;
	public Camera cam;
    public Vector2 upperCorner;
    public Vector2 targeCamtWidth;
    //private float maxWidth;
    //private float maxHeigth;
    public bool gameOver;
    public GameObject gameLooseText;
    public GameObject gameWinText;
    private int score;
    public Text scoreText;
    public int lives;
    public Text livesText;
    public float initBallSpeed;
    public float initPlayerSpeed;
    public int powerUpFallSpeed;
    private LevelMaker lMaker;
    public GameObject[] block;
    public GameObject player;
    public GameObject ball;
    private GameObject initBall;
    private GameObject newBall;

    //Some embelishment to show this variable as a slider on Inspector
    [Range(0, 100)]
    public float poweUpChancePerc;

    [Range(1.05f, 2.0f)]
    public float objStopFriction;

    public float cameraShakeDuration;
    public float cameraShakeIntensity;
    public float cameraShakeDecreaseFactor;

	// Awake is called when the script instance is being loaded.
	void Awake()
	{
        //Check if this is the only instance of this object and if not, kills this instance
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Set the camera of the game to the "cam" variable
        if (cam == null)
        {
            cam = Camera.main;
        }

        //Initiate and a set some variables usefull to identify the screen borders
        upperCorner = new Vector2(Screen.width, Screen.height);
        targeCamtWidth = GameController.instance.cam.ScreenToWorldPoint(upperCorner);
        // maxWidth = targeCamtWidth.x;
        // maxHeigth = targeCamtWidth.y;

        //Initiate the lmaker variable that will contain the blocks configuration on the screen
        lMaker = new LevelMaker();
        //Using the information contained on the LevelMaker class to instantiate the block array on the screen
        for (int arrayLin = 0; arrayLin < lMaker.blockTypeArray.GetLength(0); arrayLin++)
        {
            for(int arrayCol = 0; arrayCol < lMaker.blockTypeArray.GetLength(1); arrayCol++){
                switch (lMaker.blockTypeArray[arrayLin, arrayCol])
                {
                    case 1:
                        Instantiate(block[0], new Vector2((float)lMaker.blockPosArray[arrayLin, arrayCol, 0],
                                (float)lMaker.blockPosArray[arrayLin, arrayCol, 1]), Quaternion.identity);
                        continue;
                    case 2:
                        Instantiate(block[1], new Vector2((float)lMaker.blockPosArray[arrayLin, arrayCol, 0],
                                (float)lMaker.blockPosArray[arrayLin, arrayCol, 1]), Quaternion.identity);
                        continue;
                    case 3:
                        Instantiate(block[2], new Vector2((float)lMaker.blockPosArray[arrayLin, arrayCol, 0],
                                (float)lMaker.blockPosArray[arrayLin, arrayCol, 1]), Quaternion.identity);
                        continue;
                    default:
                        continue;
                }
            }
        }
        initBall = Instantiate(ball,new Vector2 (player.transform.position.x,
            (player.transform.position.y + (player.transform.localScale.y/2) + ball.transform.localScale.y/2 + 0.1f)),
            Quaternion.identity) as GameObject;
	}

	// Update is called once per frame
	void Update ()
	{
        if (!gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!gameOver && Input.GetKeyDown(KeyCode.S))
        {
            cam.GetComponent<CameraShake>().ShakeCamera(cameraShakeDuration, cameraShakeIntensity, cameraShakeDecreaseFactor);
        }

    	//Restart the Game
		if (gameOver && Input.GetKeyDown("space"))
		{
			//Make unity reload the scene currenctly active
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

        if (!gameOver && Input.GetKeyDown(KeyCode.N))
        {
            CreateNewBall();
        }
        //Endgame if all the blocks have been cleaned
        if (GameObject.FindGameObjectsWithTag("Blocks").Length == 0)
        {
            Endgame("win");
        }
	}

    public void Scored()
    {
		if (!gameOver) {
            score++;
			scoreText.text = "Score: " + score.ToString();
		} else {
			return;
		}
    }

    public void LoseLife()
    {
        lives -= 1;
        if (lives >= 0)
        {
            livesText.text = "Lives: " + lives.ToString();
        }
        else
        {
            Endgame("loose");
        }
    }

    public void Endgame(string endCondition)
	{
        gameOver = true;

        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);

//        BroadcastMessage("CleanLevel");

        string[] tags = new string[] {"PowerUp", "Ball", "Player"};

        foreach (string tag in tags)
        {
            GameObject[] aliveObjects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in aliveObjects)
            {
                obj.SendMessage("CleanLevel");
            }
        }

        if (endCondition == "win")
        {
            gameWinText.SetActive(true);
        }
        else
        {
            gameLooseText.SetActive(true);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void CreateNewBall() {

        Vector2 newBallPos = GameObject.FindGameObjectsWithTag("Ball")[0].transform.position;
        newBall =  Instantiate(ball, newBallPos, Quaternion.identity) as GameObject;
    }
}
