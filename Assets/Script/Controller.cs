using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*----------------------------------------------------------------------------------------------------------------------
    Touch:
    0 - Prolog-
    1 - Suhu Farm*
    2 - Factory-
    3 - Suhu Factory-
    4 - Beruang
    5 - Gajah
    6 - Store
    7 - Singa
    8 - Games 1
    9 - Games 2
    10 - P2 Kanan
    11 - P2 Kiri
    12 - P1 Kanan
    13 - P1 Kiri
    14 - 
    15 - 
    16 - Farm Atas
    17 - Farm Bawah
    18 - Rumah Kiri
    19 - Rumah Kanan

    Swipe:
    0 - Farm atas 
    1 - Farm bawah
    2 - Rumah Kiri
    3 - Rumah Kanan
    4 - 
    5 - 

    Blow:
    0 - Anak


----------------------------------------------------------------------------------------------------------------------*/
public class Controller : MonoBehaviour {

    public bool factoryTemp = false;
    public bool beruang = false;
    public bool gajah = false;
    public bool singa = false;
    public bool games1 = false;
    public bool games2 = false;
    public bool p1kiri = false;
    public bool p1kanan = false;
    public bool p2kiri = false;
    public bool p2kanan = false;
    public bool farmAtas = false;
    public bool farmBawah = false;
    public bool rumahKiri = false;
    public bool rumahKanan = false;
    public bool farm = false;
    public bool factory = false;
    public bool store = false;
    public bool rumah = false;
    public bool change = false;
    public bool prolog = false;
    public bool anak = false;
    public bool bendera = false;
    public bool lastAnak = false;
    public bool stateAnak = false;
    public bool tapActive = false;
    public bool sesuatu = false;
    public bool sesuatu1 = false;

    private float tFarm = 0f;
    private float tFactory = 0f;
    private float tStore = 0f;
    private float trumah = 0f;
    private float tchange = 0f;
    private float tprolog = 0f;
    private float tbendera = 0f;
    private float tgames = 0f;





    private SerialHandler serialHandler;
	public GameObject serHandler;


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
            farm = !farm;

        if (Input.GetKeyDown(KeyCode.S))
            factory = !factory;

        if (Input.GetKeyDown(KeyCode.D))
            store = !store;

        if (Input.GetKeyDown(KeyCode.F))
            rumah = !rumah;

        if (Input.GetKeyDown(KeyCode.Q))
            change = !change;

        if (Input.GetKeyDown(KeyCode.W))
            prolog = !prolog;

        if (Input.GetKeyDown(KeyCode.E))
            bendera = !bendera;
        if (Input.GetKeyDown(KeyCode.M))
            games2 = !games2;
        if (Input.GetKeyDown(KeyCode.N))
            games1 = !games1;

        if (Input.GetKeyDown(KeyCode.L))
            factoryTemp = !factoryTemp;

        if (Input.GetKeyDown(KeyCode.P))
        {
            anak = true;
        }

        if (farm == true)
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
            if (tFactory > 50f)
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
        /*
        if (prolog == true && bendera == false)
        {
            tprolog += Time.deltaTime;
            if (tprolog > 10f)
            {
                prolog = false;
                tprolog = 0f;
            }
        }

        if (bendera == true)
        {
            tbendera += Time.deltaTime;
            if (tbendera > 10f)
            {
                prolog = false;
                tbendera = 0f;
            }
        }
        */


        if (SerialHandler.serial_is_open)
        {

            if (SerialHandler.farm_is_swiped)
                farm = !farm;
            if (SerialHandler.rumah_is_swiped)
                rumah = !rumah;


            if (SerialHandler.prolog_is_touched)
                prolog = !prolog;
            if (SerialHandler.bendera_is_touched)
                bendera = !bendera;
            if (SerialHandler.factory_is_touched)
                factory = !factory;
            if (SerialHandler.factoryTemp_is_touched)
                factoryTemp = !factoryTemp;
            if (SerialHandler.beruang_is_touched)
                beruang = !beruang;
            if (SerialHandler.gajah_is_touched)
                gajah = !gajah;
            if (SerialHandler.store_is_touched)
                store = !store;
            if (SerialHandler.singa_is_touched)
                singa = !singa;
            if (SerialHandler.games1_is_touched)
                games1 = !games1;
            if (SerialHandler.games2_is_touched)
                games2 = !games2;
            if (SerialHandler.p2kanan_is_touched)
                p2kanan = !p2kanan;
            if (SerialHandler.p2kiri_is_touched)
                p2kiri = !p2kiri;
            if (SerialHandler.p1kanan_is_touched)
                p1kanan = !p1kanan;
            if (SerialHandler.p1kiri_is_touched)
                p2kiri = !p1kiri;
            if (SerialHandler.sesuatu1_is_touched)
                sesuatu1 = !sesuatu1;
            if (SerialHandler.sesuatu_is_touched)
                sesuatu = !sesuatu;
            if (SerialHandler.farmAtas_is_touched)
                farmAtas = !farmAtas;
            if (SerialHandler.farmBawah_is_touched)
                farmBawah = !farmBawah;
            if (SerialHandler.rumahKiri_is_touched)
                rumahKiri = !rumahKiri;
            if (SerialHandler.rumahKanan_is_touched)
                rumahKanan = !rumahKanan;

            if (SerialHandler.anak_is_tiuped)
                anak = !anak;
        }
    }

}
    
