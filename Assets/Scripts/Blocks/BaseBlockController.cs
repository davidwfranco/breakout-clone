using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockController : MonoBehaviour {

	private float chance;
	private float powerUpChance;
	public GameObject[] powerUps;
	private int hits = 0;
	public int blockLife;


	// Use this for initialization
	void Start () {
		powerUpChance = GameController.instance.poweUpChancePerc/100;
	}

	public void BallHit() {
		hits ++;
		if (hits >= blockLife)
		{
			GameController.instance.Scored();
			Vector2 powerUpPos = transform.position;
			chance = Random.Range(0f, 1f);
			if ( chance <= powerUpChance)
			{
				Instantiate(powerUps[Random.Range(0,powerUps.Length)], powerUpPos, Quaternion.identity);
			}
			Destroy(gameObject);
		}
	}
}