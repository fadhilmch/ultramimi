using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class SerialHandler : MonoBehaviour {
	
	/**/
	[Tooltip("The serial port where the Arduino is connected")]
	public static string port = "COM1";
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
	// Use this for initialization
	void Start () {
		
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
		byte[] cmd = {0xAA};
		serialPort.Write (cmd, 0, cmd.Length);
		serialPort.BaseStream.Flush();

		string temp = serialPort.ReadLine();

		byte []arr = System.Text.Encoding.ASCII.GetBytes(temp);

		Debug.Log (arr);


	}
}
