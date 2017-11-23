using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rdb2d;
	private Vector2 rawPosition;
	private Vector2 targetPotision;
	private float targetWidth;
	private float playerWidth;
	private float maxW;
	private GameController gControl;

	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();
		playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
		maxW = GameController.instance.maxWidth;
		gControl = GameController.instance;
	}
	
	// Update is called once per physics timestamp
	void FixedUpdate () {
		if (!GameController.instance.gameOver) {
			rawPosition = GameController.instance.cam.ScreenToWorldPoint (Input.mousePosition);
			targetPotision = new Vector2 (rawPosition.x, rdb2d.position.y);

			// targetWidth = Mathf.Clamp (targetPotision.x, -maxW + (playerWidth/2), 
			// 							maxW - (playerWidth/2));
			// targetPotision = new Vector2 (targetWidth, targetPotision.y);

			rdb2d.MovePosition (targetPotision);
		} else {
			rdb2d.MovePosition (new Vector2 (0.0f, rdb2d.position.y));
		}
	}
}
