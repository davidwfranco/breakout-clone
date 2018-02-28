using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNP : MonoBehaviour {

	private Vector2 rawPosition;
	private Vector2 targetPotision;
	private float targetWidth;
	private GameControllerNP gControl;
	private float moveSpeed;
	private float moveHDir;

	// Use this for initialization
	void Start () {
		gControl = GameControllerNP.instance;
		moveSpeed = gControl.initPlayerSpeed;
	}
	
	// Update is called once per physics timestamp
	void FixedUpdate () {
		if (!gControl.gameOver) {
			// Keyboard control
			moveHDir = Input.GetAxisRaw("Horizontal");
			targetPotision = new Vector2((transform.position.x + (moveSpeed * moveHDir)), transform.position.y);

			// Mouse Controller
 			//rawPosition = gControl.cam.ScreenToWorldPoint (Input.mousePosition);
			//targetPotision = new Vector2 (rawPosition.x, transform.position.y);
			
			transform.position = targetPotision;

		} else {
			transform.position = new Vector2 (0.0f, transform.position.y);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Collider2D>().CompareTag("Boundaries")) {
			moveSpeed = 0;
		}
	}

	void CleanLevel()
	{
		Destroy(gameObject);
	}	
}
