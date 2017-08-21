using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerManager : MonoBehaviour {
	public Player playerTarget;
	public float playerLinesPosY = 5.0f;
	private float[] playerLines;
	public float initTimeSpawn = 0;
	public float delayTimeSpawn = 5;
	public GameObject[] objectSpawns;
    public GamePlay gamePlay;

	void Start(){
		playerLines = playerTarget.targetPosX;
		//InvokeRepeating ("SpawnThing", initTimeSpawn, delayTimeSpawn);
	}

    public void StartGame() {
        InvokeRepeating("SpawnThing", initTimeSpawn, delayTimeSpawn);
    }

	private void SpawnThing(){
        if (gamePlay.isGameOver) return;
		int indexObject = UnityEngine.Random.Range (0, objectSpawns.Length);
		int randLine = UnityEngine.Random.Range (0, playerLines.Length);
		GameObject cloneThings = Instantiate (objectSpawns [indexObject], 
			new Vector3 (playerLines[randLine], playerLinesPosY, 0), 
			Quaternion.identity);
		Destroy (cloneThings, 4);
	}
}
