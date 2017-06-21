using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour {

	public Vector2 speed = new Vector2(10,10);
	public Vector2 direction = new Vector2 (0, -1);
	public float timeSpan = 10f;
	private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;

	// Use this for initialization
	void Start () {
		if(gameObject != null)
			Destroy (gameObject, timeSpan);
	}
	
	// Update is called once per frame
	void Update () {
		movement = new Vector2 (
			speed.x * direction.x,
			speed.y * direction.y);
	}

	void FixedUpdate()
	{
		if (GameController.gameState == GameController.GameState.Play) {
			if (rigidbodyComponent == null)
				rigidbodyComponent = GetComponent<Rigidbody2D> ();
			rigidbodyComponent.velocity = movement;
		}
	}
}
