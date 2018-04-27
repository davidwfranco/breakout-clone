using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockController : MonoBehaviour {

	private float chance;
	private float powerUpChance;
	public GameObject[] powerUps;
	private int hits = 0;
	public int blockLife;
	private GameController gControll;
	public GameObject blockBreakParticle;
	private bool swapColor;

	// Use this for initialization
	void Start () {
		gControll = GameController.instance;
		powerUpChance = gControll.poweUpChancePerc/100;
	}

	public void BallHit() {
		hits ++;
		if (hits >= blockLife)
		{
			gControll.Scored();
			
			gControll.cam.GetComponent<CameraShake>().ShakeCamera(0.05f, 0.07f, 1f, "random");
			
			Instantiate (blockBreakParticle, this.transform.position, Quaternion.identity);
			
			Vector2 powerUpPos = transform.position;
			chance = Random.Range(0f, 1f);
			if ( chance <= powerUpChance)
			{
				Instantiate(powerUps[Random.Range(0,powerUps.Length)], powerUpPos, Quaternion.identity);
			}
			
			Destroy(gameObject);
		}
	}

	public IEnumerator ChangeColor() {
		this.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(.1f);
		this.GetComponent<SpriteRenderer>().color = Color.white;		
	}
}