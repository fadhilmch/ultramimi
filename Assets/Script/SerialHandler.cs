using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using System.Text;

public class SerialHandler : MonoBehaviour {

    private const int prologTouch = 0;
    private const int benderaTouch = 1;
    private const int factoryTouch = 2;
    private const int jawabanTouch = 3;
    private const int storeTouch = 4;
    private const int changeTouch = 5;

    private const int farmSwipe = 0;
    private const int rumahSwipe = 1;

    private const int anakTiup = 0;
    

	/**/
	[Tooltip("The serial port where the Arduino is connected")]
	public static string port = "COM6";
	/* The baudrate of the serial port. */
	[Tooltip("The baudrate of the serial port")]
	public static int baudrate = 9600;
	public bool serial_is_open = false;

	public bool farm_is_swiped = false;
	public bool rumah_is_swiped= false;

	public bool prolog_is_touched  = false;
    public bool change_is_touched = false;
    public bool factory_is_touched = false;
    public bool store_is_touched = false;
    public bool jawaban_is_touched = false;
    public bool bendera_is_touched = false;

    public bool anak_is_tiuped = false;

    private bool farm = false;
	private bool factory = false;
	private bool store = false;
	private bool rumah = false;
	private bool prolog = false;
    private bool jawaban = false;
    private bool bendera = false;
    private bool anak = false;
    private bool change = false;

	private bool lastFarm = false;
	private bool lastFactory = false;
	private bool lastStore = false;
	private bool lastRumah = false;
	private bool lastprolog = false;
    private bool lastBendera = false;
    private bool lastJawaban = false;
    private bool lastAnak = false;
    private bool lastChange = false;

	public SerialPort serialPort = new SerialPort(port,baudrate);
	char buff;

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
		lastFarm = farm;
		lastFactory = factory;
		lastStore = store;
		lastprolog = prolog;
		lastRumah = rumah;
		lastAnak = anak;
        lastChange = change;
        lastJawaban = jawaban;
        lastBendera = bendera;

		byte[] cmd = {0x6A};
		serialPort.Write (cmd, 0, cmd.Length);
		serialPort.BaseStream.Flush();

		string temp = serialPort.ReadLine();

		byte []arr = System.Text.Encoding.ASCII.GetBytes(temp);
        
 
		//Debug.Log (temp);

		
		string dataTouch = intToBin (arr[2]);
		string dataSwipe = intToBin (arr[4]);
		string dataTiup = intToBin (arr [6]);

		farm = checkSwipe (dataSwipe, farmSwipe)==1;
		anak = checkTiup (dataTiup, anakTiup) == 1;
		factory = checkTouch (dataTouch, factoryTouch)==1;
		store = checkTouch (dataTouch, storeTouch)==1;
		prolog = checkTouch (dataTouch, prologTouch) == 1;
		rumah = checkSwipe (dataSwipe, rumahSwipe) == 1;
        change = checkTouch(dataTouch, changeTouch) == 1;
        bendera = checkTouch(dataTouch, benderaTouch) == 1;
        jawaban = checkTouch(dataTouch, jawabanTouch) == 1;


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

        anak_is_tiuped = checkState (anak, lastAnak);
		farm_is_swiped = checkState (farm, lastFarm);
		factory_is_touched = checkState (factory, lastFactory);
		store_is_touched = checkState (store, lastStore);
		prolog_is_touched = checkState (prolog, lastprolog);
		rumah_is_swiped = checkState (rumah, lastRumah);
        change_is_touched = checkState(change, lastChange);
        bendera_is_touched = checkState(bendera, lastBendera);
        jawaban_is_touched = checkState(jawaban, lastJawaban);
	}
}
