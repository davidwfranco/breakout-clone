using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	private Rigidbody2D rdb2d;
	public float ballSpeed;

	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();

		rdb2d.velocity = new Vector2(Random.Range(-2,2), ballSpeed);
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
		if (other.collider.CompareTag("Player"))
		{
			float resBallCollision = ballCollision(transform.position, other.transform.position, ((BoxCollider2D)other.collider).size.x);
			Vector2 newDirection = new Vector2(resBallCollision,1).normalized;
			rdb2d.velocity = newDirection * ballSpeed;
		}
	}

	// Sent when another object enters a trigger collider attached to this
	// object (2D physics only).
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Collider2D>().CompareTag("Floor"))
		{	
			Destroy(gameObject);
			GameController.instance.Endgame();
		}
	}
}
