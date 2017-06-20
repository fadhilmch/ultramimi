using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamRoad : MonoBehaviour {
	private float groundVerticalLength;
	private BoxCollider2D groundCollider;
	public int roadCount = 1;

	private float seamCooldown;

	// Use this for initialization
	private void awake()
	{
		groundCollider = GetComponent<BoxCollider2D> ();
		groundVerticalLength = groundCollider.size.y;
	}


	// Update is called once per frame
	void Update () {
		Debug.Log (transform.position.y + " " + groundVerticalLength);
		if (transform.position.y < -18.77) {
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
