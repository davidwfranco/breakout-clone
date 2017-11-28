using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Sent when an incoming collider makes contact with this object's
	// collider (2D physics only).
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.collider.CompareTag("Ball"))
		{
			Destroy(gameObject);
		}
	}
}
