using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballTestRC : MonoBehaviour {
	RaycastHit2D[] hit;
	public float moveSpeed;
	float ballXSpeed;
	float ballYSpeed;
	private Vector2[] directions;
	GameObject lastcol;
	Vector2 upLeft = new Vector2(-1,1);
	Vector2 upRight = new Vector2(1,1);
	Vector2 downRight = new Vector2(1,-1);
	Vector2 downLeft = new Vector2(-1,-1);

	// Use this for initialization
	void Start () {
		ballXSpeed = Random.Range(-moveSpeed, moveSpeed);
		ballYSpeed = Random.Range(-moveSpeed, moveSpeed);
		directions = new Vector2[] {Vector2.up, Vector2.right, Vector2.down, Vector2.left, 
			upLeft, upRight, downRight, downLeft};
		Debug.Log(directions.GetLength(0));
	}

	// Update is called once per frame
	void FixedUpdate () {

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

		this.transform.position = new Vector2 (this.transform.position.x + (moveSpeed * ballXSpeed), 
			                                       this.transform.position.y + (moveSpeed * ballYSpeed));
	}
}
