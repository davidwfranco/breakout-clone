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
	
	Vector3 originalPos;
	
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
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
				
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				camTransform.localPosition = originalPos;
				isActive = false;
			}
		}
	}

	public void ShakeCamera(float duration, float intensity, float decFactor) {
		isActive = true;
		
		shakeDuration = duration;
		shakeAmount = intensity;
		decreaseFactor = decFactor;
	}
}