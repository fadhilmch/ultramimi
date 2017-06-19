using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeDrop : MonoBehaviour {
	public int milkCount = 20;
	public int obstacleCount = 10;
	public int counterP1 = 10;
	public int counterP2 = 60;
	public float spawnTime = 1.1f;
	public GameObject milk;

	private Vector3[] spawnCoordinateP2;
	private Vector3[] spawnCoordinateP1;
	private float spawnCoolDown;
	// Use this for initialization
	void Start () {
		spawnCoordinateP2 = new Vector3[]
		{
			new Vector3(6.93f, 5.73f,0),
			new Vector3(5.36f,5.73f,0),
			new Vector3(3.55f,5.73f,0)
		};	
	
		spawnCoordinateP1 = new Vector3[]
		{
			new Vector3(-3.25f, 5.73f,0),
			new Vector3(-4.99f,5.73f,0),
			new Vector3(-6.7f,5.73f,0)
		};

		spawnCoolDown = spawnTime;

		//InvokeRepeating ("SpawnMilkP2", spawnTime, spawnTime );
	}

	void Update()
	{
		if (spawnCoolDown > 0) {
			spawnCoolDown -= Time.deltaTime;
		} else {
			spawnCoolDown = spawnTime;
			SpawnMilkP1 ();
		}
		Debug.Log (spawnCoolDown);
	}
	
	// Update is called once per frame
	void SpawnMilkP1 () {
		Instantiate (milk, spawnCoordinateP1 [Random.Range (0, 3)], new Quaternion ());

	}


		public bool CanSpawn
	{
		get{
			return spawnCoolDown <= 0f;
		}
	}
	/*
	void SpawnMilkP2 () {
		if (counterP2 > 1) {
			Instantiate (milk, spawnCoordinateP2 [Random.Range (0, 3)], new Quaternion ());
			Debug.Log ("New P2 milk is Instantiated");
			counterP2--;
		}
	}*/
}
