using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public bool farm = false;
    public bool factory = false;
    public bool nutrisi = false;
    public bool rumah = false;
    public bool change = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            farm = !farm;

        if (Input.GetKeyDown(KeyCode.S))
            factory = !factory;

        if(Input.GetKeyDown(KeyCode.D))
            nutrisi = !nutrisi;

        if (Input.GetKeyDown(KeyCode.F))
            rumah = !rumah;

        if (Input.GetKeyDown(KeyCode.Q))
            change = !change;
        
    }
}
