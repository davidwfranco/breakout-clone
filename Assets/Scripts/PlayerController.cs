using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	RaycastHit2D[] hit;
	Vector2[] directions;
	private Vector2 rawPosition;
	private Vector2 targetPosition;
	private float targetWidth;
	private GameController gControl;
	private float moveSpeed;
	private float moveHDir;
	private float wallPos;
	private int btnPress;
	private bool hitLeft;
	private bool hitRight;
	private Animator anim;
	
	//Coroutine mvmt
	private IEnumerator coroutineMove;
	private Vector3 oldPos;
	private Vector3 newPos;
	

	// Use this for initialization
	void Start () {
		gControl = GameController.instance;
		directions = new Vector2[2] {Vector2.right, Vector2.left};
		hitLeft = false;
		hitRight = false;
		anim = GetComponentInChildren<Animator>();
		coroutineMove = moveRoutine(Vector3.zero);
		moveSpeed = gControl.initPlayerSpeed;
	}

	// Update is called once per physics timestamp
	void FixedUpdate () {
		if (!gControl.gameOver) {
			if(
				//Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved
				Input.GetMouseButton(0) 
			){
				StopCoroutine(coroutineMove);
				coroutineMove = moveRoutine(Input.mousePosition);
				StartCoroutine(coroutineMove);
			}

			//OLD MVMT
			/* 			
			foreach (Vector2 dir in directions) {
				hit = Physics2D.RaycastAll(transform.position, dir);
				Debug.DrawRay(transform.position, dir);

				if (hit[1].collider != null) {

					// Keyboard control
					if (Input.GetAxisRaw("Horizontal") != 0) {
						moveHDir = Input.GetAxisRaw("Horizontal");

						if (hit[1].distance <= (transform.localScale.x * 0.55f)) {

							if (dir == Vector2.left) {
								hitLeft = true;
							} else {
								hitRight = true;
							}

							wallPos = hit[1].collider.transform.position.x;

							// Condition that guarantee that the paddle do not pass the borders of the screen
							if ((wallPos > this.transform.position.x && moveHDir < 0) ||
								(wallPos < this.transform.position.x && moveHDir > 0)) {
									moveSpeed = gControl.initPlayerSpeed;
							} else {
								moveSpeed = 0;
							}
						} else {
							if (dir == Vector2.left) {
								hitLeft = false;
							} else {
								hitRight = false;
							}

							if (!hitRight && !hitLeft)
							{
								moveSpeed = gControl.initPlayerSpeed;
							}
						}
					} else {
						moveSpeed /= gControl.paddleStopFactor;
					}
				}
			}

			// Mouse Controller
 			//rawPosition = gControl.cam.ScreenToWorldPoint (Input.mousePosition);
			//targetPotision = new Vector2 (rawPosition.x, transform.position.y);

			targetPosition = new Vector2((transform.position.x + (moveSpeed * moveHDir)), transform.position.y);
			//targetPosition = new Vector2((transform.position.x + (moveSpeed * moveHDir) * Time.deltaTime), transform.position.y); 
		*/
		} else {
			targetPosition = new Vector2 (0.0f, transform.position.y);
		}

		//transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
	}

    private IEnumerator moveRoutine(Vector3 touch) {
        float pos = (Mathf.Abs(Camera.main.ScreenToWorldPoint(touch).x - transform.position.x));
        Debug.Log("Touch Pos - " + Mathf.Abs(Camera.main.ScreenToWorldPoint(touch).x) + 
				": Paddle X - "+Mathf.Abs(transform.position.x) + 
				"=" + (Mathf.Abs(Camera.main.ScreenToWorldPoint(touch).x - transform.position.x))); 
        while(pos>0.01) {
            pos = (Mathf.Abs(Camera.main.ScreenToWorldPoint(touch).x - transform.position.x));
            //Debug.Log(pos);
            oldPos = transform.position;
            newPos = new Vector3(Mathf.Lerp(transform.position.x, Camera.main.ScreenToWorldPoint(touch).x, Time.deltaTime * moveSpeed), transform.position.y, transform.position.z);
            transform.position = new Vector3(Mathf.Clamp(newPos.x, -2f, 2.3f), newPos.y, newPos.z);

            yield return new WaitForEndOfFrame();
        }
    }
	public void Enlarge(float ratio) {
		if ((transform.localScale.x * (1f + ratio)) <= gControl.targeCamtWidth.x)
		{
			transform.localScale = new Vector2(transform.localScale.x * (1f + ratio), transform.localScale.y);
		}
	}

	public void Contract(float ratio) {
		transform.localScale = new Vector2(transform.localScale.x * (1f - ratio), transform.localScale.y);
	}

	public void Wobble() {
		anim.SetTrigger("StartWobble");
	}

	void CleanLevel()
	{
		Destroy(gameObject);
	}
}
