using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int[] movingPlayer = {0,0}; 


	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("right") || SerialHandler.p2kanan_is_touched)
		{
			movingPlayer[1] += 1;
		}

		if(Input.GetKeyDown("left") || SerialHandler.p2kiri_is_touched)
		{
			movingPlayer[1] += -1;
		}

		if(Input.GetKeyDown(KeyCode.A) || SerialHandler.p1kiri_is_touched)
		{
			movingPlayer[0] += 1;
		}

		if(Input.GetKeyDown(KeyCode.D) || SerialHandler.p1kanan_is_touched)
		{
			movingPlayer[0] += -1;
		}

		movingPlayer[0] = Mathf.Clamp (movingPlayer[0], -1, 1);
		movingPlayer[1] = Mathf.Clamp (movingPlayer[1], -1, 1);
	}
}