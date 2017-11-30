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

	// Sent when another object leaves a trigger collider attached to
	// this object (2D physics only).
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Collider2D>().CompareTag("Ball"))
		{
			Destroy(gameObject);
		}	
	}
}