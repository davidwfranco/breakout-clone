using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNP : MonoBehaviour {

	private Vector2 rawPosition;
	private Vector2 targetPotision;
	private float targetWidth;
	private GameControllerNP gControl;
	public float moveSpeed;
	private float moveHDir;

	// Use this for initialization
	void Start () {
		gControl = GameControllerNP.instance;
	}
	
	// Update is called once per physics timestamp
	void FixedUpdate () {
		if (!gControl.gameOver) {
			// Keyboard control
			/* moveHDir = Input.GetAxisRaw("Horizontal");
			rdb2d.velocity = new Vector2((moveSpeed * moveHDir), 0); */

			// Mouse Controller
 			rawPosition = gControl.cam.ScreenToWorldPoint (Input.mousePosition);
			targetPotision = new Vector2 (rawPosition.x, transform.position.y);

			transform.position = targetPotision;

		} else {
			transform.position = new Vector2 (0.0f, transform.position.y);
		}
	}

	void CleanLevel()
	{
		Destroy(gameObject);
	}	
}
