using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	private Rigidbody2D rdb2d;
	public float vSpeed;

	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();

		rdb2d.velocity = new Vector2(0, vSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
