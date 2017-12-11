using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour {
	public int frameRate = 60;
	// Use this for initialization
	void Awake () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = frameRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.targetFrameRate != frameRate)
		{
			Application.targetFrameRate = frameRate;	
		}
	}
}
