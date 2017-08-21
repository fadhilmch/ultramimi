using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour {
	public float speed = 5;
	void FixedUpdate(){
		transform.Translate (0, speed * -1 * Time.deltaTime, 0);
	}
}
