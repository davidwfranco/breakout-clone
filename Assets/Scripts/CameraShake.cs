using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	private float shakeDuration;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount;
	private float decreaseFactor;
	private bool isActive;
	private GameController gControll;
	private Vector3 direction;
	private Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void Start()
	{
		originalPos = camTransform.localPosition;
		gControll = GameController.instance;
	}

	void Update()
	{
		if (isActive) {
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + direction * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				camTransform.localPosition = originalPos;
				isActive = false;
			}
		}
	}

	public void ShakeCamera(float duration, float intensity, float decFactor, string dir) {
		isActive = true;
		
		shakeDuration = duration;
		shakeAmount = intensity;
		decreaseFactor = decFactor;
		switch (dir) {
			case "up":
				direction = Vector2.up;
				break;
			case "down":
				direction = Vector2.down;
				break;
			case "left":
				direction = Vector2.left;
				break;
			case "right":
				direction = Vector2.right;
				break;
			default:
				direction = Random.insideUnitSphere;
				break;
		}
		
	}
}