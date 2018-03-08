using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockController : MonoBehaviour {
	private GameControllerNP gControll;
	private float chance;
	public float powerUpChance;
	public GameObject[] powerUps;
	private int hits = 0;
	public int blockLife;
	private Vector2 powerUpPos;


	// Use this for initialization
	void Start () {
		gControll = GameControllerNP.instance;
		powerUpChance = gControll.poweUpChancePerc/100;
		powerUpPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void BallHit () {
		hits++;
		if (hits >= blockLife) {
			gControll.Scored();
	 		chance = Random.Range(0f, 1f);
	 		if (chance <= powerUpChance)
	 		{
	 			Instantiate(powerUps[Random.Range(0,powerUps.Length)], powerUpPos, Quaternion.identity);
	 		}
			Destroy(gameObject);	
		}
	}
}