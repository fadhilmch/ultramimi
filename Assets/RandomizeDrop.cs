using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeDrop : MonoBehaviour {
	public int milkCount = 20;
	public int obstacleCount = 10;

	private Vector2[] spawnCoordinateP2;
	private Vector2[] spawnCoordinateP1;

	// Use this for initialization
	void Start () {
		spawnCoordinateP2 = new Vector2[]
		{
			new Vector2(-4.48, 0),
			new Vector2(-6.2,0),
			new Vector2(-7.92,0)
		};	

		spawnCoordinateP2 = new Vector2[]
		{
			new Vector2(-13.55, 0),
			new Vector2(-15.2,0),
			new Vector2(-16.92,0)
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
