using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	private Rigidbody2D rdb2d;
	public float ballSpeed;
	public GameObject player;
	private bool gameOn = false;

	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Activate gravity for some frames to preven the ball of been stuck going sideways
		if (rdb2d.velocity.y > -2 && rdb2d.velocity.y < 2)
		{
			rdb2d.gravityScale = 3;
		}
		else
		{
			rdb2d.gravityScale = 0;
		}

		//Stuck the ball to the player ate the begining of the game
		if (!gameOn)
		{
			rdb2d.transform.position = new Vector2 (player.transform.position.x, (player.transform.position.y + 0.4f));
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rdb2d.velocity = new Vector2(Random.Range(-3,3), ballSpeed);
				gameOn = true;
			}
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
	void OnCollisionExit2D(Collision2D other)
	{
		// If the ball collides with the player it bounces the ball upwards in a different horizontal position
		// depending on how faz it hits from the half point
		if (other.collider.CompareTag("Player"))
		{
			float resBallCollision = ballCollision(transform.position, other.transform.position, ((CapsuleCollider2D)other.collider).size.x);
			Vector2 newDirection = new Vector2(resBallCollision,1).normalized;
			rdb2d.velocity = newDirection * ballSpeed;
		}
		// If it hits a block, add score and speed up the ball
		else if (other.collider.CompareTag("Blocks"))
		{
			GameController.instance.Scored();
			ballSpeed += 1;
			if (rdb2d.velocity.y > 0)
			{
				rdb2d.velocity = new Vector2(transform.position.x, ballSpeed);	
			}
			else 
			{
				rdb2d.velocity = new Vector2(transform.position.x, -ballSpeed);	
			}
		}
	}

	// Sent when another object enters a trigger collider attached to this
	// object (2D physics only).
	void OnTriggerEnter2D(Collider2D other)
	{
		// Destroy the ball on collision with the ground
		if (other.GetComponent<Collider2D>().CompareTag("Floor"))
		{	
			Destroy(gameObject);
			GameController.instance.Endgame();
		}
	}
}
