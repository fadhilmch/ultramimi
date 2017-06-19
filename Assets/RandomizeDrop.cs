using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeDrop : MonoBehaviour {
	public int milkCount = 20;
	public int obstacleCount = 10;
	public int counterP1 = 60;
	public int counterP2 = 60;
	public float spawnTime = 1.6f;
	public GameObject milk;

	private Vector3[] spawnCoordinateP2;
	private Vector3[] spawnCoordinateP1;
	private Transform[] spawnPoints;
	// Use this for initialization
	void Start () {
		spawnCoordinateP2 = new Vector3[]
		{
<<<<<<< HEAD
			new Vector3(6.93f, 5.73f,0),
			new Vector3(5.36f,5.73f,0),
			new Vector3(3.55f,5.73f,0)
=======
			new Vector2(6.93f, 0),
			new Vector2(5.36f,0),
			new Vector2(3.55f,0)
>>>>>>> e37c7ca181022af36423c8700a63f722ab639044
		};	
	
		spawnCoordinateP1 = new Vector3[]
		{
<<<<<<< HEAD
			new Vector3(-3.25f, 5.73f,0),
			new Vector3(-4.99f,5.73f,0),
			new Vector3(-6.7f,5.73f,0)
=======
			new Vector2(-3.25f, 0),
			new Vector2(-4.99f,0),
			new Vector2(-6.7f,0)
>>>>>>> e37c7ca181022af36423c8700a63f722ab639044
		};

		InvokeRepeating ("SpawnMilkP1", spawnTime + Random.Range(0,0.4f), spawnTime + Random.Range(0,0.2f));
		InvokeRepeating ("SpawnMilkP2", spawnTime + Random.Range(0,0.4f), spawnTime + Random.Range(0,0.2f));
	}
	
	// Update is called once per frame
	void SpawnMilkP1 () {
		if (counterP1 > 1) {
			Instantiate (milk, spawnCoordinateP1 [Random.Range (0, 3)], new Quaternion ());
			counterP1--;
		}
	}

	void SpawnMilkP2 () {
		if (counterP2 > 1) {
			Instantiate (milk, spawnCoordinateP2 [Random.Range (0, 3)], new Quaternion ());
			counterP2--;
		}
	}
}
