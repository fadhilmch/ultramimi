using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awan : MonoBehaviour {

    public Vector2 Speed;
    public Transform LeftPos;
    public Transform RightPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Speed*Time.deltaTime);

        if (this.transform.position.x <= LeftPos.position.x)
        {
            this.transform.position = new Vector3(RightPos.position.x, this.transform.position.y, this.transform.position.z);
        }
	}
}
