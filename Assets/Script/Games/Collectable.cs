using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	public int point = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target){
		
		if (target.gameObject.tag == "Player" && GameController.gameState == GameController.GameState.Play) {
			target.gameObject.GetComponent<Player> ().totalScore += point;
			Destroy (gameObject);
		}
	}
}
