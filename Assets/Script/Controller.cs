using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public bool farm = false;
    public bool factory = false;
    public bool store = false;
    public bool rumah = false;
    public bool change = false;
    public bool prolog = false;
    public bool anak = false;
    public bool jawaban = false;


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
            store = !store;

        if (Input.GetKeyDown(KeyCode.F))
            rumah = !rumah;

        if (Input.GetKeyDown(KeyCode.Q))
            change = !change;

        if (Input.GetKeyDown(KeyCode.W))
            prolog = !prolog;

        if (Input.GetKeyDown(KeyCode.Z))
            jawaban = !jawaban;

        if (gameObject.GetComponent<SerialHandler>().serial_is_open) {
			if (gameObject.GetComponent<SerialHandler>().farm_is_swiped)
				farm = !farm;
			if (gameObject.GetComponent<SerialHandler>().factory_is_touched)
				factory = !factory;
            if (gameObject.GetComponent<SerialHandler>().store_is_touched)
                store = !store;
            if (gameObject.GetComponent<SerialHandler>().rumah_is_swiped)
                rumah = !rumah;
            if (gameObject.GetComponent<SerialHandler>().prolog_is_touched)
                prolog = !prolog;
            //if (gameObject.GetComponent<SerialHandler>().change_is_touched)
             //   change = !change;
        }
    }
}
