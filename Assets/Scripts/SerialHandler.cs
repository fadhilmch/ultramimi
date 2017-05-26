using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using System.Text;

public class SerialHandler : MonoBehaviour {
	
	/**/
	[Tooltip("The serial port where the Arduino is connected")]
	public static string port = "COM3";
	/* The baudrate of the serial port. */
	[Tooltip("The baudrate of the serial port")]
	public static int baudrate = 9600;

	public bool farm = false;
	public bool factory = false;
	public bool asi = false;
	public bool telo = false;
	public bool let = false;


	public SerialPort serialPort = new SerialPort(port,baudrate);


	char buff;

	string intToBin(byte n)
	{
		
		string output = "";
		for (int i = 7; i > 0; i--) {
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
		byte[] cmd = {0x6A};
		serialPort.Write (cmd, 0, cmd.Length);
		serialPort.BaseStream.Flush();

		string temp = serialPort.ReadLine();

		byte []arr = System.Text.Encoding.ASCII.GetBytes(temp);


		Debug.Log (temp);
		bool[] binNumb;
		for (int i = 0; i < 7; i++) {
			// Mask each bit in the byte and store it
			Debug.Log(intToBin(arr[i]));

		}

	}
}
