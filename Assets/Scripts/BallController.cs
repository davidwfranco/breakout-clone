using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	private GameController gControll;
	private GameObject player;
	private RaycastHit2D[] hit;
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
	private float frameCount;
	private bool isPlayerSticky = false;


	// Use this for initialization
	void Start () {
		gControll = GameController.instance;
		directions = new Vector2[8] {Vector2.up, Vector2.right, Vector2.down, Vector2.left, 
			upLeft, upRight, downRight, downLeft};
		gameOn = false;
		
		ballXSpeed = Random.Range(-gControll.initBallSpeed, gControll.initBallSpeed);
		ballYSpeed = Random.Range(0, gControll.initBallSpeed);

		player = GameObject.FindGameObjectsWithTag("Player")[0];
		if (GameObject.FindGameObjectsWithTag("Ball").Length > 1) {
			gameOn = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!gControll.gameOver)
		{
			//Stuck the ball to the player ate the begining of the game
			if (!gameOn) {
				transform.position = new Vector2 (player.transform.position.x, 
						(player.transform.position.y + (player.transform.localScale.y/2) + this.transform.localScale.y/2 + 0.1f));
				if (Input.GetKeyDown(KeyCode.Space) /*|| Input.GetMouseButtonDown(22220)*/ ) {
					gameOn = true;
					
					ballXSpeed = Random.Range(-gControll.initBallSpeed, gControll.initBallSpeed);
					ballYSpeed = Random.Range(0, gControll.initBallSpeed);
				}
			} else {
				//Everything else that happens when the Game has begining and the ball is not sticking to the player
				foreach (Vector2 direction in directions) {
					//Check the distance to other objects to determine if it has to change the path it's moving
					hit = Physics2D.RaycastAll(transform.position, direction);
					// Debug.DrawRay(transform.position, direction);

					if (hit[0].collider != null) {
						if (hit[0].distance <= (transform.localScale.x/4 * 3) && !hit[0].collider.CompareTag("Floor")) {

							if (lastcol != hit[0].transform.gameObject) {
								lastcol = hit[0].transform.gameObject;	
								
								if (direction == Vector2.up) {
									if (ballYSpeed > 0) {							
										ballYSpeed *= -1;
									}
								} else if (direction == Vector2.down) {
									if (ballYSpeed < 0) {							
										ballYSpeed *= -1;
									}
								} else if (direction == Vector2.right) {
									if (ballXSpeed > 0) {							
										ballXSpeed *= -1;
									}
								} else if (direction == Vector2.left) {
									if (ballXSpeed < 0) {							
										ballXSpeed *= -1;
									}
								} else if (direction == upLeft) {
									if (ballXSpeed < 0 && ballYSpeed > 0) {							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								} else if (direction == upRight) {
									if (ballXSpeed > 0 && ballYSpeed > 0) {							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								} else if (direction == downRight) {
									if (ballXSpeed > 0 && ballYSpeed < 0) {							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								} else if (direction == downLeft) {
									if (ballXSpeed < 0 && ballYSpeed < 0) {							
										ballXSpeed *= -1;
										ballYSpeed *= -1;
									}
								}
							}
							
							if (hit[0].collider.CompareTag("Blocks")) {
								hit[0].transform.gameObject.SendMessage("BallHit");
							} else if (hit[0].collider.CompareTag("Player") && isPlayerSticky) {
								gameOn = false;
								isPlayerSticky = false;
							} else if (hit[0].collider.CompareTag("Boundaries")) {
								hit[0].transform.gameObject.SendMessage("Wobble");
							} 
						}
					}
				}
				
				//Activate gravity for some frames to preven the ball of been stuck going sideways
				if (ballYSpeed == 0) {
					if (frameCount > 300) {
						ballYSpeed += Random.Range(-0.1f, 0.1f);
						frameCount = 0;
					} else {
						frameCount++;
					}
				}				

				//Move player based on the result of the coditions above
				transform.position = new Vector2(transform.position.x + ballXSpeed, transform.position.y + ballYSpeed);
			}
		} else {
			this.CleanLevel();
		}
	}

 	// // Create a function that receives the ball position, the player position and the player width
	// // with this it returns the collision pos
	// private float ballCollision( Vector2 ballPos, Vector2 playerPos, float playerWidth) {
	// 	return (ballPos.x - playerPos.x) / playerWidth;
	// }

	public void SlowDown(float ratio) {
		if ((Mathf.Abs(ballXSpeed) * (1.0f - ratio) > 0.05f) || (Mathf.Abs(ballYSpeed) * (1.0f - ratio) > 0.05f))
		{
			// Debug.Log("Before = " + ballXSpeed + " / " + ballYSpeed);
			ballXSpeed *= 1 - ratio;
			ballYSpeed *= 1 - ratio;
			// Debug.Log("After = " + ballXSpeed + " / " + ballYSpeed);
		}		
	}
	
	public void Accelerate(float ratio) {
		if ((Mathf.Abs(ballXSpeed) * (1.0f - ratio) < 0.05f) || (Mathf.Abs(ballYSpeed) * (1.0f - ratio) > 0.05f))
		{
			// Debug.Log("Before = " + ballXSpeed + " / " + ballYSpeed);
			ballXSpeed *= 1 - ratio;
			ballYSpeed *= 1 - ratio;
			// Debug.Log("After = " + ballXSpeed + " / " + ballYSpeed);
		}		
	}

	public void CleanLevel() {
		Destroy(gameObject);
	}

	public void StickToPlayer() {
		isPlayerSticky = true;
	}

	public void SetGameOn(){
		Debug.Log("Set Game On.");
		gameOn = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Collider2D>().CompareTag("Floor")) {
			if (GameObject.FindGameObjectsWithTag("Ball").Length > 1) {
				Destroy(gameObject);
			} else {
				gControll.LoseLife();
				gameOn=false;
			}
		}
	}
}
