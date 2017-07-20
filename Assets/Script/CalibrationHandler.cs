using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class CalibrationHandler : MonoBehaviour {

	[Tooltip("The serial port where the Arduino is connected (usually COM1-COM9")]
	[SerializeField]
	public static string port = "COM3";

	[Tooltip("The baudrate of the serial port")]
	[SerializeField]
	public static int baudrate = 9600;

	public SerialPort serialPort = new SerialPort(port, baudrate);
	public static bool serial_is_open = false;
	public static bool state_is_calibrate = false;
	private static bool serial_is_proper = false;
	char buff;

	// 0x0A is new line
	byte[] cmd = { 0x6A, 0x0A };

	byte[] cmdCal = { 0x6A, 0x01, 0x0A };

	private string[] portNameList;
	private int portNameLength = 0;
	private int portNameIndex = 0;
	private bool start = true;

	private float timer = 0f;
	private const float timerCount = 5f;

	private bool checkTimer()
	{
		timer += Time.deltaTime;
		return false;
	}

	private string intToBinaryString(byte n)
	{

		string output = "";
		for (int i = 0; i < 8; i++)
		{
			if ((n >> i) % 2 == 1)
			{
				output += "1";
			}
			else
			{
				output += "0";
			}

		}
		return output;
	}

	private bool openSerialCommunication()
	{
		if (serialPort != null)
		{
			Debug.Log("test");
			if (serialPort.IsOpen)
			{
				serialPort.Close();
				Debug.Log("Closing port, because it was already open!");
				return false;
			}
			else
			{
				serialPort.Open();
				serialPort.ReadTimeout = 50;
				return true;

			}
		}
		else
		{
			if (serialPort.IsOpen)
			{
				print("Port is already open");
				return true;

			}
			else
			{
				print("Port == null");
				return false;
			}
		}
	}


	void Start()
	{

		// Initialize serial protocol
		serialPort.Parity = Parity.None;
		serialPort.StopBits = StopBits.One;
		serialPort.DataBits = 8;

		portNameList = SerialPort.GetPortNames();
		portNameLength = portNameList.Length;

		for (int i = 0; i < portNameLength; i++)
			Debug.Log(portNameList[i]);
		if ( portNameLength > 0)
		{
			serial_is_proper = openSerialCommunication();
		}
		Debug.Log(serial_is_proper);
	}

	// Update is called once per frame
	void Update()
	{
		if (serial_is_proper) {
			serial_is_open = serialPort.IsOpen;
			serialPort.Write (cmd, 0, cmd.Length);
			serialPort.BaseStream.Flush ();

			string temp = serialPort.ReadLine ();

			byte[] arr = System.Text.Encoding.ASCII.GetBytes (temp);

			string dataTouch = intToBinaryString (arr [2]) + intToBinaryString (arr [3]);
			dataTouch = dataTouch.Substring (0, 7) + dataTouch.Substring (8, 7);

			string dataSwipe = intToBinaryString (arr [5]);
			string dataBlow = intToBinaryString (arr [7]);
			string dataCalibrate = intToBinaryString (arr [9]);

			if (dataCalibrate [0] == '1') {
				serialPort.Write (cmdCal , 0, cmdCal.Length);
				serialPort.BaseStream.Flush ();

				//loadscene calibrate
			}
		} else {
			//exit calibration
		}




	}
	void OnApplicationQuit()
	{
		serialPort.Close();
		Debug.Log("Port closed!");
	}
}
