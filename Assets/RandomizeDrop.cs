using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeDrop : MonoBehaviour {
	public int milkCount = 20;
	public int obstacleCount = 10;
	public int counter = 60;

	private Vector2[] spawnCoordinateP2;
	private Vector2[] spawnCoordinateP1;

	// Use this for initialization
	void Start () {
		spawnCoordinateP2 = new Vector2[]
		{
			new Vector2(6.93f, 0),
			new Vector2(5.36f,0),
			new Vector2(3.55f,0)
		};	

		spawnCoordinateP1 = new Vector2[]
		{
			new Vector2(-3.25f, 0),
			new Vector2(-4.99f,0),
			new Vector2(-6.7f,0)
		};


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
