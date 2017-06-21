using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamRoad : MonoBehaviour {
	private float groundVerticalLength;
	public int roadCount = 1;
	public float loopHeight = -18.77f;
	private float seamCooldown;

	// Use this for initialization
	private void awake() 
	{
		
	}


	// Update is called once per frame
	void Update () {
		if (transform.position.y < loopHeight) {
			RepositionBackground();
		} 
	}

	void RepositionBackground()
	{
		Debug.Log ("Reposition");
		Vector3 groundOffset = new Vector3 (transform.position.x, 0f, transform.position.z);
		transform.position = groundOffset;
	}
}
