using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerNP : MonoBehaviour {
	private GameControllerNP gControll;
	public GameObject player;
	private RaycastHit2D[] hit;
	private Rigidbody2D rb2D;
	private float moveSpeed;
	private float ballXSpeed;
	private float ballYSpeed;
	private Vector2[] directions;
	private GameObject lastcol;
	private Vector2 upLeft = new Vector2(-1,1);
	private Vector2 upRight = new Vector2(1,1);
	private Vector2 downRight = new Vector2(1,-1);
	private Vector2 downLeft = new Vector2(-1,-1);
	private bool gameOn;


	// Use this for initialization
	void Start () {
		gControll = GameControllerNP.instance;
		rb2D = this.GetComponent<Rigidbody2D>();
		directions = new Vector2[] {Vector2.up, Vector2.right, Vector2.down, Vector2.left, 
			upLeft, upRight, downRight, downLeft};
		gameOn = false;
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
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(22220) )
				{
					gameOn = true;
					
					ballXSpeed = Random.Range(-gControll.initBallSpeed, gControll.initBallSpeed);
					ballYSpeed = Random.Range(-gControll.initBallSpeed, gControll.initBallSpeed);
					
					//ballYSpeed = gControll.initBallSpeed;
					//ballXSpeed = -gControll.initBallSpeed;
				}
			} else {
				//Everything else that happens when the Game has begining and the ball is not sticking to the player
				foreach (Vector2 direction in directions)
				{
					hit = Physics2D.RaycastAll(transform.position, direction);
					Debug.DrawRay(transform.position, direction);

					if (hit[1].collider != null)
					{
						if (hit[1].distance <= (transform.localScale.x/4 * 3))
						{	
							if (lastcol != hit[1].transform.gameObject)
							{
								lastcol = hit[1].transform.gameObject;	
								
								if (direction == Vector2.up)
								{
									if (ballYSpeed > 0)
									{							
										ballYSpeed *= -1;
									}
								} else if (direction == Vector2.down) {
									if (ballYSpeed < 0)
									{							
										ballYSpeed *= -1;
									}
								} else if (direction == Vector2.right) {
									if (ballXSpeed > 0)
									{							
										ballXSpeed *= -1;
									}
								} else if (direction == Vector2.left) {
									if (ballXSpeed < 0)
									{							
										ballXSpeed *= -1;
									}
								} else if (direction == upLeft) {
									if (ballXSpeed < 0 && ballYSpeed > 0)
									{							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								} else if (direction == upRight) {
									if (ballXSpeed > 0 && ballYSpeed > 0)
									{							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								} else if (direction == downRight) {
									if (ballXSpeed > 0 && ballYSpeed < 0)
									{							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								} else if (direction == downLeft) {
									if (ballXSpeed < 0 && ballYSpeed < 0)
									{							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								}
							}
						}
					}
				}
				transform.position = new Vector2(transform.position.x + ballXSpeed, transform.position.y + ballYSpeed);
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
