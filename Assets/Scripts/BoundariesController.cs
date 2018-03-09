using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesController : MonoBehaviour {
	public void Wobble() {
		gameObject.GetComponentInChildren<Animation>().Play();
	}
}
