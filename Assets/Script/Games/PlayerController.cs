using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int movingPlayer1 = 0; 
	public int movingPlayer2 = 0;

	void Start () {
		movingPlayer1 = 0; 
		movingPlayer2 = 0;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("right"))
		{
			movingPlayer1 += 1;
		}

		if(Input.GetKeyDown("left"))
		{
			movingPlayer1 += -1;
		}

		if(Input.GetKeyDown("A"))
		{
			movingPlayer2 += 1;
		}

		if(Input.GetKeyDown("D"))
		{
			movingPlayer2 += -1;
		}



	}
}