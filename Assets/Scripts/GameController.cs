using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public static GameController instance;
	public Camera cam;
    public Vector2 upperCorner;
    public Vector2 targeCamtWidth;
    public float maxWidth;
    public float maxHeigth;
    public bool gameOver;
    public GameObject gameLooseText;
    public GameObject gameWinText;
    private int score;
    public Text scoreText;
    private int lives;
    public Text livesText;
    public float initBallSpeed;
    public float poweUpChancePerc;
    public int powerUpFallSpeed;
    private LevelMaker lMaker;
    public GameObject[] block;

	// Awake is called when the script instance is being loaded.
	void Awake()
	{
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (cam == null)
        {
            cam = Camera.main;
        }
        
        lMaker = new LevelMaker();
        upperCorner = new Vector2(Screen.width, Screen.height);
        targeCamtWidth = GameController.instance.cam.ScreenToWorldPoint(upperCorner);
        maxWidth = targeCamtWidth.x;
        maxHeigth = targeCamtWidth.y;
        lives = 3;

        for (int arrayLin = 0; arrayLin < lMaker.blockTypeArray.GetLength(0); arrayLin++)
        {
            for(int arrayCol = 0; arrayCol < lMaker.blockTypeArray.GetLength(1); arrayCol++){
                switch (lMaker.blockTypeArray[arrayLin, arrayCol])
                {
                    case 1:
                        Instantiate(block[0], new Vector2((float)lMaker.blockPosArray[arrayLin, arrayCol, 0], (float)lMaker.blockPosArray[arrayLin, arrayCol, 1]), Quaternion.identity);
                        continue;
                    case 2:
                        Instantiate(block[1], new Vector2((float)lMaker.blockPosArray[arrayLin, arrayCol, 0], (float)lMaker.blockPosArray[arrayLin, arrayCol, 1]), Quaternion.identity);        
                        continue;
                    case 3:
                        Instantiate(block[2], new Vector2((float)lMaker.blockPosArray[arrayLin, arrayCol, 0], (float)lMaker.blockPosArray[arrayLin, arrayCol, 1]), Quaternion.identity);        
                        continue;
                    default:
                        continue;
                }
            }
        }
	}

	// Update is called once per frame
	void Update ()
	{
        if (!gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	
    	//Restart the Game
		if (gameOver && Input.GetKeyDown("space"))
		{
			//Make unity reload the scene currenctly active
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}
