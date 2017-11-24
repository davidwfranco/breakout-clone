using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rdb2d;
	private Vector2 rawPosition;
	private Vector2 targetPotision;
	private float targetWidth;
	private GameController gControl;

	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();
		gControl = GameController.instance;
	}
	
	// Update is called once per physics timestamp
	void FixedUpdate () {
		if (!gControl.gameOver) {
			//keyboard control
			if (Input.GetKey("left")) 
			{
				rdb2d.velocity = new Vector2(-5, 0);
			}
			else if (Input.GetKey("right")) 
			{
				rdb2d.velocity = new Vector2(5, 0);				
			}
			else 
			{
				rdb2d.velocity = Vector2.zero;
			}

			//mouse pointer control
			// rawPosition = gControl.cam.ScreenToWorldPoint (Input.mousePosition);
			// targetPotision = new Vector2 (rawPosition.x, rdb2d.position.y);

			// rdb2d.MovePosition (targetPotision);
		} else {
			rdb2d.MovePosition (new Vector2 (0.0f, rdb2d.position.y));
		}
	}
}
