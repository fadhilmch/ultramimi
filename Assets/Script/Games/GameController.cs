using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public enum GameState
	{
		Stop = 0,
		Play = 1,
		Finished = 2
	}
	public static GameState gameState = GameState.Stop;
	public float countDown = 3f;

	private float counter;
	// Use this for initialization
	void Start () {
		counter = countDown;
	}
	
	// Update is called once per frame
	void Update () {
		if (counter < 0f) {
			gameState = GameState.Play;
			GameObject.Find ("Systems/Start/Countdown").SetActive (false);
		} else {
			counter -= Time.deltaTime;
		}
	}
}
