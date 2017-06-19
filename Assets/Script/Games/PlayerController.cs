using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int moving = 0; 

	// Update is called once per frame
	void Update () {
		if(Input.GetKey("right"))
		{
			moving += 1;
		}

		if(Input.GetKey("left"))
		{
			moving += -1;
		}

	}
}