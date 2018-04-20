using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	private GameController gControll;
	private GameObject player;
	private RaycastHit2D[] hit;
	private float ballSpeed;
	private float xDirection = 0;
	private float yDirection;
	private Vector2[] directions;
	private GameObject lastcol;
	private Vector2 upLeft = new Vector2(-1,1);
	private Vector2 upRight = new Vector2(1,1);
	private Vector2 downRight = new Vector2(1,-1);
	private Vector2 downLeft = new Vector2(-1,-1);
	private bool gameOn;
	private float frameCount;
	private bool isPlayerSticky = false;
	//private Animator anim;

	// Use this for initialization
	void Start () {
		gControll = GameController.instance;
		directions = new Vector2[8] {Vector2.up, Vector2.right, Vector2.down, Vector2.left,
			upLeft, upRight, downRight, downLeft};
		gameOn = false;

		ballSpeed = gControll.initBallSpeed;

 		xDirection = Random.Range(-1, 2);
		yDirection = 1;

		player = GameObject.FindGameObjectsWithTag("Player")[0];
		if (GameObject.FindGameObjectsWithTag("Ball").Length > 1) {
			gameOn = true;
		}

		//anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!gControll.gameOver)
		{
			//Stuck the ball to the player ate the begining of the game
			if (!gameOn) {
				transform.position = new Vector2 (player.transform.position.x,
						(player.transform.position.y + (player.transform.localScale.y/2) + this.transform.localScale.y/2 + 0.1f));
				
				if (Input.GetKeyDown(KeyCode.Space) /*|| Input.GetMouseButtonDown(0)*/ ) {
					gameOn = true;

					xDirection = Random.Range(-1, 2);

					while (xDirection == 0) {
						xDirection = Random.Range(-1, 2);
					}

					yDirection = 1;
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
									if (yDirection > 0) {
										yDirection *= -1;
									}
								} else if (direction == Vector2.down) {
									if (yDirection < 0) {
										yDirection *= -1;
									}
								} else if (direction == Vector2.right) {
									if (xDirection > 0) {
										xDirection *= -1;
									}
								} else if (direction == Vector2.left) {
									if (xDirection < 0) {
										xDirection *= -1;
									}
								} else if (direction == upLeft) {
									if (xDirection < 0 && yDirection > 0) {
										xDirection *= -1;
										yDirection *= -1;
									}
								} else if (direction == upRight) {
									if (xDirection > 0 && yDirection > 0) {
										xDirection *= -1;
										yDirection *= -1;
									}
								} else if (direction == downRight) {
									if (xDirection > 0 && yDirection < 0) {
										xDirection *= -1;
										yDirection *= -1;
									}
								} else if (direction == downLeft) {
									if (xDirection < 0 && yDirection < 0) {
										xDirection *= -1;
										yDirection *= -1;
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
							} else if (hit[0].collider.CompareTag("Player")) {
								
								// If the object of collision 
								float collisionPos = ballCollision(this.transform.position,
									hit[0].collider.transform.position, hit[0].collider.transform.localScale.x );
								xDirection = (collisionPos * 2);
								
								//anim.SetTrigger("HitPlayer");
							}
						} 
					}
				}

				//Activate gravity for some frames to preven the ball of been stuck going sideways
				if (yDirection == 0) {
					if (frameCount > 300) {
						yDirection += Random.Range(-0.1f, 0.1f);
						frameCount = 0;
					} else {
						frameCount++;
					}
				}

				//Move player based on the result of the coditions above
				transform.position = new Vector2(transform.position.x + (xDirection * ballSpeed),
												transform.position.y + (yDirection * ballSpeed));
			}
		} else {
			this.CleanLevel();
		}
	}

 	// Create a function that receives the ball position, the player position and the player width
	// with this it returns the collision pos
	private float ballCollision( Vector2 ballPos, Vector2 playerPos, float playerWidth) {
		return (ballPos.x - playerPos.x) / playerWidth;
	}

	public void SlowDown(float ratio) {
		if ((Mathf.Abs(xDirection) * (1.0f - ratio) > 0.05f) || (Mathf.Abs(yDirection) * (1.0f - ratio) > 0.05f))
		{
			xDirection *= 1 - ratio;
			yDirection *= 1 - ratio;
		}
	}

	public void Accelerate(float ratio) {
		if ((Mathf.Abs(xDirection) * (1.0f - ratio) < 0.05f) || (Mathf.Abs(yDirection) * (1.0f - ratio) > 0.05f))
		{
			xDirection *= 1 - ratio;
			yDirection *= 1 - ratio;
		}
	}

	public void CleanLevel() {
		Destroy(gameObject);
	}

	public void StickToPlayer() {
		isPlayerSticky = true;
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
