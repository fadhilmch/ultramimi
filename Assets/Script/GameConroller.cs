using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using System.Text;



public class GameConroller : MonoBehaviour {



	public GameObject farmObject;
	public GameObject storeObject;

	// Serial Communication Variables
	public static string port = "COM6"; 
	public static int baudrate = 9600;
	private bool serialOpen = false;
	public SerialPort serialPort = new SerialPort(port,baudrate);
	char buff;
<<<<<<< HEAD
    private string temp;
	//Scene 1
	/*public GameObject prologObject;
=======
    string temp = "";

    //Scene 1
    /*public GameObject prologObject;
>>>>>>> 91779ba81c631334cbadfe407d5691e0d333b4b6
	public GameObject farmObject;
	public GameObject factoryObject;
	public GameObject storeObject;
	public GameObject rumahObject;

	//Scene Games 2
	public GameObject player1KiriObject;
	public GameObject player1KananObject;
	public GameObject player2KiriObject;
	public GameObject player2KananObject;
*/
    private enum SensorType{
		Touch,
		Swipe,
		Blow
	}

	private enum TouchID{
		Prolog = 0,
		Farm = 1,
		Factory = 2,
		Store = 3,
		Rumah = 4,
		Player1Kiri = 5,
		Player1Kanan = 6,
		Player2Kiri = 7,
		Player2Kanan = 8,
		Size
	};

	private enum SwipeID{
		Anak = 0,
		Size
	};

	private enum BlowID{
		Farm = 0,
		Rumah = 1,
		Size
	};

	private enum State{
		Prolog,
		Farm,
		Factory,
		Store,
		Rumah,
		Player1Kiri,
		Player1Kanan,
		Player2Kiri,
		Player2Kanan,
		Size
	}
		

	private bool[] state = new bool[(int)State.Size];
	private bool[] lastState = new bool[(int)State.Size];
	private bool[] sensorValue = new bool[(int)State.Size];
	private bool[] objectState = new bool[(int)State.Size];


	bool CheckStatus (bool status, bool objectState){
		
		if (status)
			objectState = !objectState;
		return objectState;
	}

	void StateControl(GameObject Object, bool value, bool objectState){
		if (CheckStatus (value, objectState)) {
			Object.GetComponent<Animator> ().SetInteger ("AnimState", 1);
		} else {
			Object.GetComponent<Animator> ().SetInteger ("AnimState", 0);
		}
	}

	void UpdateValue(bool state, SensorType sensor, string data, int sensorID){
		if (sensor == SensorType.Touch)
			state = CheckTouch (data, sensorID) == 1;
		else if (sensor == SensorType.Swipe)
			state = CheckSwipe (data, sensorID) == 1;
		else
			state = CheckTiup (data, sensorID) == 1;
	}

	bool CheckState(bool now, bool past)
	{
		if (now && !past) {
			return true;
		}
		else 
			return false;
	}

	int CheckTouch(string input, int index)
	{
		if (input [index] == '0')
			return 0;
		else
			return 1;
	}

	int CheckSwipe(string input, int index)
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

	int CheckTiup(string input, int index)
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


	//Serial Communication itilization
	void InitializeSerial(){
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
				Debug.Log("Port is already open");
			}
			else
			{
				Debug.Log("Port == null");
			}
		}
	}


	// Use this for initialization
	void Start () {
		InitializeSerial ();
		for (int i = 0; i < state.Length; i++) {
			state[i] = false;
			sensorValue[i] = false;
			objectState[i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
        serialOpen =serialPort==null? false : serialPort.IsOpen;

		for (int i = 0; i < state.Length; i++) {
			lastState = state;
		}

		ManualControl ();
		StateControl (farmObject, sensorValue [(int)State.Farm], objectState [(int)State.Farm]);
		StateControl (storeObject, sensorValue [(int)State.Store], objectState [(int)State.Store]);

        
		byte[] cmd = {0x6A};

       

        if(serialPort != null)
        {
            serialPort.Write(cmd, 0, cmd.Length);
            serialPort.BaseStream.Flush();
            temp = serialPort.ReadLine();
        }
		

		byte []arr = System.Text.Encoding.ASCII.GetBytes(temp);

		string dataTouch = intToBin (arr[2]);
		string dataSwipe = intToBin (arr[4]);
		string dataTiup = intToBin (arr [6]);

		for (int i = 0; i < (int)TouchID.Size; i++) {
			UpdateValue (state [i], (int)SensorType.Touch, dataTouch, i);
			print (i);
		}

		for (int i = 0; i < (int)BlowID.Size; i++) {
			UpdateValue (state [i + (int)TouchID.Size], SensorType.Blow, dataTiup, i);
		}

		for (int i = 0; i < (int)SwipeID.Size; i++) {
			UpdateValue (state [i + (int)TouchID.Size + (int)BlowID.Size], SensorType.Swipe, dataSwipe, i);
		}

		for (int i = 0; i < state.Length; i++){
			sensorValue[i] = CheckState(state[i],lastState[i]);			
		}

		StateControl (farmObject, sensorValue [(int)State.Farm], objectState [(int)State.Farm]);


	}
			

	void ManualControl(){
		print (objectState[(int)State.Farm]);
		if (Input.GetKeyDown(KeyCode.A))
			objectState[(int)State.Farm] = !objectState[(int)State.Farm];

		if (Input.GetKeyDown(KeyCode.S))
			objectState[(int)State.Factory] = !objectState[(int)State.Factory];

		if(Input.GetKeyDown(KeyCode.D))
			objectState[(int)State.Store] = !objectState[(int)State.Store];

		if (Input.GetKeyDown(KeyCode.F))
			objectState[(int)State.Rumah] = !objectState[(int)State.Rumah];

		if (Input.GetKeyDown(KeyCode.P))
		{
			objectState[(int)State.Farm] = true;//anak
		}
	}
}
