using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using System.Text;

public class SerialHandler : MonoBehaviour {

    private const int prologTouch = 0;
    private const int benderaTouch = 1;
    private const int factoryTouch = 2;
    private const int factoryTempTouch = 3;
    private const int beruangTouch = 4;
    private const int gajahTouch = 5;
    private const int storeTouch = 6;
    private const int singaTouch = 7;
    private const int games1Touch = 8;
    private const int games2Touch = 9;
    private const int p2kananTouch = 10;
    private const int p2kiriTouch = 11;
    private const int p1kananTouch = 12;
    private const int p1kiriTouch = 13;
    private const int sesuatu1Touch = 14;
    private const int sesuatuTouch = 15;
    private const int farmAtasTouch = 16;
    private const int farmBawahTouch = 17;
    private const int rumahKiriTouch = 18;
    private const int rumahKananTouch = 19;

    private const int farmSwipe = 0;
    private const int rumahSwipe = 1;

    private const int anakTiup = 0;
    public static string outputSensor;

	/**/
	[Tooltip("The serial port where the Arduino is connected (usually COM1-COM9")]
	public static string port = "COM6";
	/* The baudrate of the serial port. */
	[Tooltip("The baudrate of the serial port")]
	public static int baudrate = 9600;

	public static bool serial_is_open = false;
	public static bool farm_is_swiped = false;
	public static bool rumah_is_swiped= false;


    public static bool prolog_is_touched = false;
    public static bool bendera_is_touched = false;
    public static bool factory_is_touched = false;
    public static bool factoryTemp_is_touched = false;
    public static bool beruang_is_touched = false;
    public static bool gajah_is_touched = false;
    public static bool store_is_touched = false;
    public static bool singa_is_touched = false;
    public static bool games1_is_touched = false;
    public static bool games2_is_touched = false;
    public static bool p2kanan_is_touched = false;
    public static bool p2kiri_is_touched = false;
    public static bool p1kanan_is_touched = false;
    public static bool p1kiri_is_touched = false;
    public static bool sesuatu1_is_touched = false;
    public static bool sesuatu_is_touched = false;
    public static bool farmAtas_is_touched = false;
    public static bool farmBawah_is_touched = false;
    public static bool rumahKiri_is_touched = false;
    public static bool rumahKanan_is_touched = false;
    public static bool[] array_is_touched = new bool[9];
    public static bool anak_is_tiuped = false;


    public bool prolog  = false;
    public bool bendera  = false;
    public bool factory  = false;
    public bool factoryTemp  = false;
    public bool beruang  = false;
    public bool gajah  = false;
    public bool store  = false;
    public bool singa  = false;
    public bool games1  = false;
    public bool games2  = false;
    public bool p2kanan  = false;
    public bool p2kiri  = false;
    public bool p1kanan  = false;
    public bool p1kiri  = false;
    public bool sesuatu1  = false;
    public bool sesuatu  = false;
    public bool farmAtas  = false;
    public bool farmBawah  = false;
    public bool rumahKiri  = false;
    public bool rumahKanan  = false;

    public bool farm = false;
	public bool rumah = false;
    public bool anak = false;


    public bool lastProlog = false;
    public bool lastBendera = false;
    public bool lastFactory = false;
    public bool lastFactoryTemp = false;
    public bool lastBeruang = false;
    public bool lastGajah = false;
    public bool lastStore = false;
    public bool lastSinga = false;
    public bool lastGames1 = false;
    public bool lastGames2 = false;
    public bool lastP2kanan = false;
    public bool lastP2kiri = false;
    public bool lastP1kanan = false;
    public bool lastP1kiri = false;
    public bool lastSesuatu1 = false;
    public bool lastSesuatu = false;
    public bool lastFarmAtas = false;
    public bool lastFarmBawah = false;
    public bool lastRumahKiri = false;
    public bool lastRumahKanan = false;

    private bool lastFarm = false;
	private bool lastRumah = false;
	private bool lastAnak = false;
    
	public SerialPort serialPort = new SerialPort(port,baudrate);
	char buff;

    private void updateArrayTouch()
    {
        array_is_touched[0] = prolog_is_touched;
        array_is_touched[1] = beruang_is_touched;
        array_is_touched[2] = store_is_touched;
        array_is_touched[3] = p2kiri_is_touched;
        array_is_touched[4] = p2kanan_is_touched;
        array_is_touched[5] = p1kanan_is_touched;
        array_is_touched[6] = p1kiri_is_touched;
        array_is_touched[7] = rumahKanan_is_touched;
        array_is_touched[8] = rumahKiri_is_touched;

    }

	bool checkState(bool now, bool past)
	{
		if (now && !past) {
			return true;
		}
		else 
			return false;
	}

	int checkTouch(string input, int index)
	{
		if (input [index] == '0')
			return 0;
		else
			return 1;
	}

	int checkSwipe(string input, int index)
	{
		if (input [2 * index + 1] == '0' && input [2 * index] == '0')
			return 0;
		else if (input [2 * index + 1] == '1' && input [2 * index] == '0')
			return 1;
		else if (input [2 * index + 1] == '0' && input [2 * index] == '1')
			return 1;
		else
			return 2;	
	}

	int checkTiup(string input, int index)
	{
		if (input [index] == '0')
			return 0;
		else
			return 1;
	}

	string intToBin(byte n)
	{
		
		string output = "";
		for (int i = 0; i < 8; i++) {
			if ((n >> i) % 2 == 1) {
				output += "1";
			} else {
				output += "0";
			}

		}
		return output;
	}
	// Use this for initialization
	void Start () {
		// Initialize serial protocol
		serialPort.Parity = Parity.None;
		serialPort.StopBits = StopBits.One;
		serialPort.DataBits = 8;
		if(serialPort != null)
		{
			if(serialPort.IsOpen)
			{
				serialPort.Close();
				Debug.Log("Closing port, because it was already open!");
			}
			else
			{
				serialPort.Open();
				serialPort.ReadTimeout = 50;
				Debug.Log("Port Opened!");
			}
		}
		else
		{
			if(serialPort.IsOpen)
			{
				print("Port is already open");
			}
			else
			{
				print("Port == null");
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		serial_is_open = serialPort.IsOpen;

        lastProlog = prolog;
        lastBendera = bendera;
        lastFactory = factory;
        lastFactoryTemp = factoryTemp;
        lastBeruang = beruang;
        lastGajah = gajah;
        lastStore = store;
        lastSinga = singa;
        lastGames1 = games1;
        lastGames2 = games2;
        lastP2kanan = p2kanan;
        lastP2kiri = p2kiri;
        lastP1kanan = p1kanan;
        lastP1kiri = p1kiri;
        lastSesuatu1 = sesuatu1;
        lastSesuatu = sesuatu;
        lastFarmAtas = farmAtas;
        lastFarmBawah = farmBawah;
        lastRumahKiri = rumahKanan;
        lastRumahKanan = rumahKiri;

        lastFarm = farm;
        lastRumah = rumah;
        lastAnak = anak;
        try
        {
            byte[] cmd = { 0x6A };
            serialPort.Write(cmd, 0, cmd.Length);
            serialPort.BaseStream.Flush();

            string temp = serialPort.ReadLine();

            byte[] arr = System.Text.Encoding.ASCII.GetBytes(temp);


            //Debug.Log (temp);


            string dataTouch =   intToBin(arr[4]) + intToBin(arr[3]) + intToBin(arr[2]);
           dataTouch = dataTouch.Substring(0, 7) + dataTouch.Substring(8, 7) + dataTouch.Substring(16, 6);
            string dataSwipe = intToBin(arr[6]);
            string dataTiup = intToBin(arr[8]);



            prolog = checkTouch(dataTouch, prologTouch) == 1;
            bendera = checkTouch(dataTouch, benderaTouch) == 1;
            factory = checkTouch(dataTouch, factoryTouch) == 1;
            factoryTemp = checkTouch(dataTouch, factoryTempTouch) == 1;
            beruang = checkTouch(dataTouch, beruangTouch) == 1;
            gajah = checkTouch(dataTouch, gajahTouch) == 1;
            store = checkTouch(dataTouch, storeTouch) == 1;
            singa = checkTouch(dataTouch, singaTouch) == 1;
            games1 = checkTouch(dataTouch, games1Touch) == 1;
            games2 = checkTouch(dataTouch, games2Touch) == 1;
            p2kanan = checkTouch(dataTouch, p2kananTouch) == 1;
            p2kiri = checkTouch(dataTouch, p2kiriTouch) == 1;
            p1kanan = checkTouch(dataTouch, p1kananTouch) == 1;
            p1kiri = checkTouch(dataTouch, p1kiriTouch) == 1;
            sesuatu1 = checkTouch(dataTouch, sesuatu1Touch) == 1;
            sesuatu = checkTouch(dataTouch, sesuatuTouch) == 1;
            farmAtas = checkTouch(dataTouch, farmAtasTouch) == 1;
            farmBawah = checkTouch(dataTouch, farmBawahTouch) == 1;
            rumahKiri = checkTouch(dataTouch, rumahKiriTouch) == 1;
            rumahKanan = checkTouch(dataTouch, rumahKananTouch) == 1;

            farm = checkSwipe(dataSwipe, farmSwipe) == 1;
            rumah = checkSwipe(dataSwipe, rumahSwipe) == 1;

            anak = checkTiup(dataTiup, anakTiup) == 1;
        


        //Debug.Log("Swipe " + dataSwipe);
        //Debug.Log("farm " + farm);
        //Debug.Log("factory " + factory);
        //Debug.Log("store " + store);
        //Debug.Log("prolog " + prolog);
        //Debug.Log("rumah " + rumah);
        //Debug.Log("change " + change);
        //Debug.Log("bendera " + bendera);
        //Debug.Log("jawaban " + jawaban);
        //Debug.Log("anak " + anak);
        Debug.Log("swipe " + dataSwipe);
        Debug.Log("touch " + dataTouch);
        Debug.Log("raw " + temp);
        }
        catch
        {

        }
        farm_is_swiped = checkState ( farm, lastFarm );
        rumah_is_swiped = checkState ( rumah, lastRumah );


        prolog_is_touched = checkState ( prolog, lastProlog );
        bendera_is_touched = checkState ( bendera, lastBendera );
        factory_is_touched = checkState ( factory, lastFactory );
        factoryTemp_is_touched = checkState ( factoryTemp, lastFactoryTemp );
        beruang_is_touched = checkState ( beruang, lastBeruang );
        gajah_is_touched = checkState ( gajah, lastGajah );
        store_is_touched = checkState ( store, lastStore );
        singa_is_touched = checkState ( singa, lastSinga );
        games1_is_touched = checkState ( games1, lastGames1 );
        games2_is_touched = checkState ( games2, lastGames2 );
        p2kanan_is_touched = checkState ( p2kanan, lastP2kanan );
        p2kiri_is_touched = checkState ( p2kiri, lastP2kiri );
        p1kanan_is_touched = checkState ( p1kanan, lastP1kanan );
        p1kiri_is_touched = checkState ( p1kiri, lastP1kiri );
        sesuatu1_is_touched = checkState ( sesuatu1, lastSesuatu1 );
        sesuatu_is_touched = checkState ( sesuatu, lastSesuatu );
        farmAtas_is_touched = checkState ( farmAtas, lastFarmAtas );
        farmBawah_is_touched = checkState ( farmBawah, lastFarmBawah );
        rumahKiri_is_touched = checkState ( rumahKiri, lastRumahKiri );
        rumahKanan_is_touched = checkState ( rumahKanan, lastRumahKanan );

        anak_is_tiuped = checkState ( anak , lastAnak );

   
        
    }
}
