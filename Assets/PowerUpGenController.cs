using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenController : MonoBehaviour {

	public GameObject[] powerUps;
	private float powerUpChance;
	private float chance;

	// Use this for initialization
	void Start () {
		powerUpChance = GameController.instance.poweUpChancePerc/100f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiatePowerUp (Vector2 powerUpPos)
	{
		chance = Random.Range(0f, 1f);
		if ( chance <= powerUpChance)
		{
			Instantiate(powerUps[Random.Range(0,powerUps.Length)], powerUpPos, Quaternion.identity);
		}
	}
}
