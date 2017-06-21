﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {
	private Rigidbody2D rb2d;
	public float scrollSpeed = -10f;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.gameState == GameController.GameState.Play) {
			rb2d.velocity = new Vector2 (0, scrollSpeed);
		}
	}
}
