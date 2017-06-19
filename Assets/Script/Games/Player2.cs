﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {
	
	private PlayerController controller;
	private Vector2[] player1Coordinate;
	private Vector2[] player2Coordinate;


	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();

		player1Coordinate = new Vector2[] {
			new Vector2 (6.93f, -3.29f),
			new Vector2 (5.36f, -3.29f),
			new Vector2 (3.55f, -3.29f)
		};

		player2Coordinate = new Vector2[] {
			new Vector2(-3.25f, -3.29f),
			new Vector2(-4.99f,-3.29f),
			new Vector2(-6.7f,-3.29f)
		};

			
	}
	
	// Update is called once per frame
	void Update () {
		controller.movingPlayer2 = controller.movingPlayer1 > 1 ? 1 : controller.movingPlayer2;
		controller.movingPlayer2 = controller.movingPlayer1 < -1 ? -1 : controller.movingPlayer2;

		transform.position = player2Coordinate [controller.movingPlayer2+1];
		print (transform.position);

		}

}
