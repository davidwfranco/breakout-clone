using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesController : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.CompareTag("Ball"))
		{
			GetComponent<Animation>().Play();		
		}
	}
}
