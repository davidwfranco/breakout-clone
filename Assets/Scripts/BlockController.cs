using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

	private Rigidbody2D rdb2d;
	private float chance;
	private float powerUpChance;
	public GameObject[] powerUps;


	// Use this for initialization
	void Start () {
		rdb2d = GetComponent<Rigidbody2D>();
		powerUpChance = GameController.instance.poweUpChancePerc/100f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Collider2D>().CompareTag("Ball"))
		{
			Vector2 powerUpPos = other.transform.position;
			Destroy(gameObject);
			chance = Random.Range(0f, 1f);
			if ( chance <= powerUpChance)
			{
				Instantiate(powerUps[Random.Range(0,powerUps.Length)], powerUpPos, Quaternion.identity);
			}
		}
	}
}