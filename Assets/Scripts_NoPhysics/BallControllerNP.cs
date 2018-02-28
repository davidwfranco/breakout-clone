using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerNP : MonoBehaviour {
	private GameControllerNP gControll;
	public GameObject player;
	private bool gameOn = false;
	private float ballSpeedY = 0;
	private float ballSpeedX = 0;


	// Use this for initialization
	void Start () {
		gControll = GameControllerNP.instance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Activate gravity for some frames to preven the ball of been stuck going sideways
		if (!gControll.gameOver)
		{
			//Stuck the ball to the player ate the begining of the game
			if (!gameOn)
			{
				transform.position = new Vector2 (player.transform.position.x, 
						(player.transform.position.y + (player.transform.localScale.y/2) + this.transform.localScale.y/2 + 0.1f));
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )
				{
					gameOn = true;
					ballSpeedY = gControll.initBallSpeed;
					ballSpeedX = Random.Range(-0.3f, 0.3f);
				}
			} else {
				//Everything else that happens when the Game has begining and the ball is not sticking to the player
				transform.position = new Vector2(transform.position.x + ballSpeedX, transform.position.y + ballSpeedY);
			}
		} else {
			this.CleanLevel();
		}
	}

 	// Create a function that receives the ball position, the player position and the player width
	// with this it returns the collision pos
	private float ballCollision( Vector2 ballPos, Vector2 playerPos, float playerWidth)
	{
		return (ballPos.x - playerPos.x) / playerWidth;
	}

	// Sent when an incoming collider makes contact with this object's
	// collider (2D physics only).
	void OnCollisionEnter2D(Collision2D other)
	{

	}

	// Sent when another object enters a trigger collider attached to this
	// object (2D physics only).
	void OnTriggerEnter2D(Collider2D other)
	{

	}
	
	// Sent when another object leaves a trigger collider attached to
	// this object (2D physics only).
	void OnTriggerExit2D(Collider2D other)
	{

	}

	public void SlowDown(int ballSpeedDownPerc)
	{
		// if ((ballSpeed - (ballSpeed * (ballSpeedDownPerc/100f))) > 2.1f)
		// {
		// 	Debug.Log("Before ballS = " + ballSpeed);
		// 	ballSpeed *= (1 - (ballSpeedDownPerc/100f));	
		// 	Debug.Log("After ballS = " + ballSpeed);
		// }
		
	}
	
	public void Accelerate(int ballAccelPerc)
	{
		// ballSpeed *= (1 + (ballAccelPerc/100f));
	}

	public void CleanLevel()
	{
		Destroy(gameObject);
	}
}
