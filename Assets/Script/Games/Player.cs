using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public enum PlayerNum{player1 = 0, player2 = 1};
	public PlayerNum playerNum = PlayerNum.player1;
	public int totalScore = 0;
	private PlayerController controller;
	private Vector2[] playerCoordinate;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();

		playerCoordinate = new Vector2[] {
			new Vector2(-3.25f, -3.18f), //Player1
			new Vector2(-4.99f,-3.18f),
			new Vector2(-6.7f,-3.18f),
			new Vector2 (3.55f, -3.18f), //Player2
			new Vector2 (5.36f, -3.18f),
			new Vector2 (6.93f, -3.18f)
		};

	}

	// Update is called once per frame
	void Update () {
		transform.position = playerCoordinate [(3*(int)playerNum) + controller.movingPlayer[(int)playerNum]+1];
		print (transform.position);

	}

	void onTriggerEnter2D(Collider2D otherCollider)
	{
		//totalScore += otherCollider.gameObject.GetComponent<Collectable> ().point;
	}
}
