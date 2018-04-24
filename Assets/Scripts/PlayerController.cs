using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	RaycastHit2D[] hit;
	Vector2[] directions;
	private Vector2 rawPosition;
	private Vector2 targetPosition;
	private float targetWidth;
	private GameController gControl;
	private float moveSpeed;
	private float moveHDir;
	private float wallPos;
	private int btnPress;

	// Use this for initialization
	void Start () {
		gControl = GameController.instance;
		moveSpeed = gControl.initPlayerSpeed;
		directions = new Vector2[2] {Vector2.right, Vector2.left};
	}
	
	// Update is called once per physics timestamp
	void FixedUpdate () {
		if (!gControl.gameOver) {
			foreach (Vector2 dir in directions) {
				hit = Physics2D.RaycastAll(transform.position, dir);
				Debug.DrawRay(transform.position, dir);

				if (hit[1].collider != null) {
					if (hit[1].distance <= (transform.localScale.x/2 + 0.1f))
					{
						wallPos = hit[1].collider.transform.position.x;
						if ((wallPos > this.transform.position.x && moveHDir < 0) || 
							(wallPos < this.transform.position.x && moveHDir > 0)) {
							moveSpeed = gControl.initPlayerSpeed;
						} else {
							moveSpeed = 0;
						}
					} else {
						// Keyboard control
						if (Input.GetAxisRaw("Horizontal") != 0)
						{
							moveHDir = Input.GetAxisRaw("Horizontal");
							Debug.Log("Hor Dir = " + moveHDir + " / Move Speed = " + moveSpeed);
							targetPosition = new Vector2((transform.position.x + (moveSpeed * moveHDir)), transform.position.y);
							transform.position = targetPosition;
						} else {
							if (moveSpeed > 0)
							{
								moveSpeed /= 1.1f;	
							}
							Debug.Log("Hor Dir = " + Input.GetAxisRaw("Horizontal") + " / Move Speed = " + moveSpeed);
							targetPosition = new Vector2((transform.position.x + (moveSpeed * moveHDir)), transform.position.y);
							transform.position = targetPosition;
						} 
					} 
				}
			}
			
			

			// Mouse Controller
 			//rawPosition = gControl.cam.ScreenToWorldPoint (Input.mousePosition);
			//targetPotision = new Vector2 (rawPosition.x, transform.position.y);
			
			
		} else {
			transform.position = new Vector2 (0.0f, transform.position.y);
		}
	}

	public void Enlarge(float ratio) {
		if ((transform.localScale.x * (1f + ratio)) <= gControl.targeCamtWidth.x)
		{
			transform.localScale = new Vector2(transform.localScale.x * (1f + ratio), transform.localScale.y);	
		}
	}
	
	public void Contract(float ratio) {
		transform.localScale = new Vector2(transform.localScale.x * (1f - ratio), transform.localScale.y);
	}

	void CleanLevel()
	{
		Destroy(gameObject);
	}	
}
