using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockController : MonoBehaviour {

	private float chance;
	public float powerUpChance;
	public GameObject[] powerUps;
	private int hits = 0;
	public int blockLife;


	// Use this for initialization
	void Start () {
		powerUpChance = GameController.instance.poweUpChancePerc/100f;   
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Collider2D>().CompareTag("Ball"))
		{
			BallHit(other);			
		}
	}

	public void BallHit(Collider2D ball)
	{
		hits ++;
		if (hits >= blockLife)
		{
			GameController.instance.Scored();
			Vector2 powerUpPos = ball.transform.position;
			Destroy(gameObject);
			chance = Random.Range(0f, 1f);
			Debug.Log("chance: " + chance + "puchance: " + powerUpChance);
			if ( chance <= powerUpChance)
			{
				Instantiate(powerUps[Random.Range(0,powerUps.Length)], powerUpPos, Quaternion.identity);
			}
		}

	}
}