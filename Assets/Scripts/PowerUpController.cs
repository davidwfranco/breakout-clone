using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

	private Rigidbody2D rdb2d;
	private int vSpeed;

	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();
		vSpeed = GameController.instance.powerUpSpeed;
		rdb2d.velocity = Vector2.down * vSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
