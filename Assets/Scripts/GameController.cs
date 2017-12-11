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
    public int poweUpChancePerc;
    public int powerUpSpeed;

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

        upperCorner = new Vector2(Screen.width, Screen.height);
        targeCamtWidth = GameController.instance.cam.ScreenToWorldPoint(upperCorner);
        maxWidth = targeCamtWidth.x;
        maxHeigth = targeCamtWidth.y;
        lives = 3;
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
	}

    public void Scored()
    {
		if (!gameOver) {
            score++;
			scoreText.text = "Score: " + score.ToString();
            if (score >= 55)
            {
                Endgame("win");
            }
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
