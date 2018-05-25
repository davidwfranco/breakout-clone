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
    public bool gameOver;
    private LevelMaker lMaker;  

    [Header("End Game Panels")]
    public GameObject gameLooseText;
    public GameObject gameWinText;

    [Header("UI")]
    public Text livesText;
    public Text scoreText;
    public int lives;
    private int score;

    [Header("Block and PowerUp Settings")]
    public GameObject[] block;
    [Range(0, 100)]
    public float powerUpChancePerc;
    public int powerUpFallSpeed;
    
    [Header("Ball Initial Settings")]
    public GameObject ball;
    public float initBallSpeed;
    
    private GameObject initBall;
    private GameObject newBall;

    [Header ("Paddle Initial Settings")]
    public GameObject player;   
    [Range(5f,15f)]
    public float initPlayerSpeed;
    [Range(1.01f, 2.0f)]
    public float paddleStopFactor;

    [Header("CameShake Settings")]
    public float cameraShakeDuration;
    public float cameraShakeIntensity;
    public float cameraShakeDecreaseFactor;
    public string cameraShakeDirection;

    [Header("Test Functionalities")]
    public bool safeFloor;
    public bool unbreakableBlocks;    

    // Observer Pattern Variables
    Subject sub = new Subject();

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
            cam.GetComponent<CameraShake>().ShakeCamera(cameraShakeDuration, cameraShakeIntensity, cameraShakeDecreaseFactor, cameraShakeDirection);
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

    public void Notify(string ev, GameObject obj) {
        sub.Notify(ev, obj);
    }
    
    public void AddObs(string subj, Observer obs) {
        sub.AddObservers(subj, obs);
    }
    
    public void RemoveObs(string subj, Observer obs) {
        sub.RemoveObserver(subj, obs);
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
