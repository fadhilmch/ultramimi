using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public bool farm = false;
    public bool factory = false;
    public bool nutrisi = false;
    public bool rumah = false;
    public bool change = false;

	private SerialHandler serialHandler;
	public GameObject gameObject;

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
        
		if (gameObject.GetComponent<SerialHandler>().serial_is_open) {
			if (gameObject.GetComponent<SerialHandler>().farm_is_swiped)
				farm = !farm;
			if (gameObject.GetComponent<SerialHandler>().factory_is_touched)
				factory = !factory;
		}
    }
}
