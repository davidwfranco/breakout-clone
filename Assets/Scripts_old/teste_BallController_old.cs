using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController_teste : MonoBehaviour {
	private Rigidbody2D rdb2d;
	private float ballSpeed;
	public GameObject player;
	private bool gameOn = false;
	private Vector2 oldVelocity;
	public GameObject[] powerUps;

	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();
		ballSpeed = 5;
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
			rdb2d.transform.position = new Vector2 (0,-2);

			if (Input.GetKeyDown(KeyCode.Space))
			{
				rdb2d.velocity = new Vector2(Random.Range(-3,3)	, ballSpeed);
				gameOn = true;
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rdb2d.transform.position = new Vector2 (0,-2);
				rdb2d.velocity = Vector2.zero;
				rdb2d.velocity = new Vector2(Random.Range(-3,3)	, ballSpeed);
			}
		}
		
		oldVelocity = rdb2d.velocity;
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

		ContactPoint2D contact = other.contacts[0];
		Vector2 reflectedVelocity = Vector2.Reflect(oldVelocity, contact.normal);

		if (other.collider.CompareTag("Boundaries"))
		{
			rdb2d.velocity = reflectedVelocity;
		}
	}
	// Sent when another object enters a trigger collider attached to this
	// object (2D physics only).
	// solved the issue with multiple collisions thanks to this:
	// https://gamedev.stackexchange.com/a/115821
	Collider2D firstCollider;
	int collisionCount;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (firstCollider == null)
		{
			firstCollider = other;
			if (firstCollider.GetComponent<Collider2D>().CompareTag("Blocks"))
			{
				if (rdb2d.velocity.y > 0)
				{
					rdb2d.velocity = Vector2.zero;
					rdb2d.velocity = new Vector2(oldVelocity.x, -ballSpeed);	
				}
				else 
				{
					rdb2d.velocity = Vector2.zero;
					rdb2d.velocity = new Vector2(oldVelocity.x, ballSpeed);	
				}
			}
		}
		collisionCount ++;
	}
	void OnTriggerExit2D(Collider2D other)
	{
		collisionCount --;
		if (collisionCount == 0)
		{
			firstCollider = null;
		}
	}
}
