using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private PlayerController controller;
	private Vector2 positionLeft;
	private Vector2 positionMid;
	private Vector2 positionRight;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch(controller.moving){
		case -1:
			transform.position = new Vector2 (-1, 0);

			break;
		
		case 0:
			transform.position = new Vector2 (0, 0);
			break;

		case 1:
			transform.position = new Vector2 (1, 0);
			break;
			
			print (transform.position);
		}
	}
}
