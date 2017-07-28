using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAnimationController : MonoBehaviour {

	public float TargetSec;
	float elapsedTime;

	public bool moving;
	float target;

	Vector3 startRot;

	// Use this for initialization
	void Start () {
		startRot = this.transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {
			elapsedTime += Time.deltaTime;
			target = elapsedTime / TargetSec;
			this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Lerp (0 + startRot.z, -360 + startRot.z, target)));
			
			if (elapsedTime > TargetSec) {
				elapsedTime = 0;
				StopTime ();
			}
		}

	}

	public void StartTime() {
		moving = true;
	}

	public void StopTime() {
		moving = false;
	}
}
