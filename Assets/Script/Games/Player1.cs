using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	private PlayerController controller;
	private Vector2[] player1Coordinate;
	private Vector2[] player2Coordinate;


	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();

		player1Coordinate = new Vector2[] {
			new Vector2 (3.55f, -3.18f),
			new Vector2 (5.36f, -3.18f),
			new Vector2 (6.93f, -3.18f)
		};

		player2Coordinate = new Vector2[] {
			new Vector2(-3.25f, -3.18f),
			new Vector2(-4.99f,-3.18f),
			new Vector2(-6.7f,-3.18f)
		};


	}

	// Update is called once per frame
	void Update () {
		controller.movingPlayer1 = controller.movingPlayer1 > 1 ? 1 : controller.movingPlayer1;
		controller.movingPlayer1 = controller.movingPlayer1 < -1 ? -1 : controller.movingPlayer1;

		transform.position = player1Coordinate [controller.movingPlayer1+1];
		print (transform.position);

	}

}
