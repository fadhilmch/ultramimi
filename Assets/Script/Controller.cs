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
    public bool bendera = false;
    public bool lastAnak = false;
    public bool stateAnak = false;
    public bool tapActive = false;

    private float tFarm = 0f;
    private float tFactory = 0f;
    private float tStore = 0f;
    private float trumah = 0f;
    private float tchange = 0f;
    private float tprolog = 0f;
    private float tbendera = 0f;





    private SerialHandler serialHandler;
	public GameObject serHandler;

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

        if (Input.GetKeyDown(KeyCode.E))
            bendera = !bendera;

        if (Input.GetKeyDown(KeyCode.P))
        {
            anak = true;
        }
        
        if(farm == true)
        {
            tFarm += Time.deltaTime;
            if (tFarm > 25f)
            {
                farm = false;
                tFarm = 0f;
            }
        }

        if (factory == true)
        {
            tFactory += Time.deltaTime;
            if (tFactory > 20f)
            {
                factory = false;
                tFactory = 0f;
            }
        }

        if (store == true)
        {
            tStore += Time.deltaTime;
            if (tStore > 25f)
            {
                store = false;
                tStore = 0f;
            }
        }

        if (rumah == true)
        {
            trumah += Time.deltaTime;
            if (trumah > 25f)
            {
                rumah = false;
                trumah = 0f;
            }
        }

        if (change == true)
        {
            tchange += Time.deltaTime;
            if (tchange > 120f)
            {
                change = false;
                tchange = 0f;
            }
        }

        if (prolog == true&&bendera==false)
        {
            tprolog += Time.deltaTime;
            if (tprolog > 20f)
            {
                prolog = false;
                tprolog = 0f;
            }
        }

        if (bendera == true)
        {
            tbendera += Time.deltaTime;
            if (tbendera > 20f)
            {
                prolog = false;
                tbendera = 0f;
            }
        }
        /*
        if (serHandler.GetComponent<SerialHandler>().serial_is_open) {
			if (serHandler.GetComponent<SerialHandler>().farm_is_swiped)
				farm = !farm;
			if (serHandler.GetComponent<SerialHandler>().factory_is_touched)
				factory = !factory;
            if (serHandler.GetComponent<SerialHandler>().store_is_touched)
                store = !store;
            if (serHandler.GetComponent<SerialHandler>().rumah_is_swiped)
                rumah = !rumah;
            if (serHandler.GetComponent<SerialHandler>().prolog_is_touched)
                prolog = !prolog;
            if (serHandler.GetComponent<SerialHandler>().change_is_touched)
                change = !change;
            if (serHandler.GetComponent<SerialHandler>().jawaban_is_touched)
                jawaban = !jawaban;
            if (serHandler.GetComponent<SerialHandler>().bendera_is_touched)
                bendera = !bendera;
            if (serHandler.GetComponent<SerialHandler>().anak_is_tiuped)
            {
                anak = !anak;
            }
        
            
        }
        */


    }
}
