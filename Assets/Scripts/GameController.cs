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
    public GameObject gameOverText;
    private int score = 0;
    public Text scoreText;    

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
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Restart the Game
		if (gameOver && Input.GetKeyDown("space")) 
		{
			//Make unity reload the scene currenctly active
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
    public void Endgame() 
	{
        gameOver = true;
        gameOverText.SetActive(true);
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
}
