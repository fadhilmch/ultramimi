using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCall : MonoBehaviour {
    public bool trigger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(trigger)
        {

            GameObject.Find("Factory").GetComponent<Factory>().animator.SetInteger("AnimState", 2);
        }
        /*
        if (GameObject.Find("Factory").GetComponent<Controller>().factoryTemp == true)
        {
            GetComponent<Animator>().SetInteger("AnimState", 0);
        }*/
    }
}
