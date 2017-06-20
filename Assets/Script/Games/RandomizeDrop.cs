using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeDrop : MonoBehaviour {
	public enum ObjectType{Milk = 0, Tree = 1};
	public enum ObjectSide{Left = 0, Right = 1};
	public int obstacleCount = 10;
	public int counter = 60;
	public float spawnTime = 1.1f;
	public GameObject sampleObject;
	public ObjectType objectType = ObjectType.Milk;
	public ObjectSide objectSide = ObjectSide.Left;

	private Vector3[] spawnCoordinate;
	private float spawnCoolDown;
	private int minIndex, maxIndex;
	// Use this for initialization
	void Start () {
		if (objectType == ObjectType.Milk) {
			minIndex = 3 * (int)objectSide;
			maxIndex = 3 * ((int)objectSide + 1);
		} else {
			minIndex = 6 + (2 * (int)objectSide);
			maxIndex = 6 + (2 * ((int)objectSide+1));
		}

		spawnCoordinate = new Vector3[]
		{
			new Vector3(-3.25f, 5.73f,0.68f),
			new Vector3(-4.99f,5.73f,0.68f),
			new Vector3(-6.7f,5.73f,0.68f),
			new Vector3(6.93f, 5.73f,0.68f),
			new Vector3(5.36f,5.73f,0.68f),
			new Vector3(3.55f,5.73f,0.68f),
			new Vector3(-2.00f,5.73f,0.68f),
			new Vector3(-8.33f,5.73f,0.68f),
			new Vector3(8.33f, 5.73f,0.68f),
			new Vector3(2.05f,5.73f,0.68f)
		};

		spawnCoolDown = spawnTime;
	}

	void Update()
	{
		if (spawnCoolDown > 0) {
			spawnCoolDown -= Time.deltaTime;
		} else {
			spawnCoolDown = spawnTime;
			spawnObject(sampleObject);
		}
	}
	
	// Update is called once per frame
	void spawnObject (GameObject sample) {
		Instantiate (sample, spawnCoordinate [Random.Range (minIndex, maxIndex)], new Quaternion ());
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
