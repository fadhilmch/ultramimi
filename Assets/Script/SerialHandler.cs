using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text;

public class SensorCode : System.Object
{
	//run generator in util folder, edit the size first!
	public static int T0 = 0, T1 = 1, T2 = 2, T3 = 3, T4 = 4, T5 = 5, T6 = 6, T7 = 7, T8 = 8, T9 = 9, T10 = 10, T11 = 11, S0 = 12, S1 = 13, S2 = 14, S3 = 15, S4 = 16, S5 = 17, B0 = 13, size = 19;
}

public class DataSensor : System.Object
{
	private bool sensor = false;
	private bool lastSensor = false;

	public bool isDown = false;
	public bool isUp = false;
	public bool isChanged = false;



	public void updateData()
	{
		if (sensor && !lastSensor)
			isChanged = true;
		else
			isChanged = false;

		lastSensor = sensor;

		if (isChanged)
		{
			if (sensor)
				isDown = true;
			else
				isDown = false;
		}
		isUp = !isDown;
	}

	public void checkTouch(string input, int index)
	{
		if (input[index] == '0')
			sensor = false;
		else
			sensor = true;
	}

	public void checkSwipe(string input, int index)
	{
		if (input[2 * index + 1] == '0' && input[2 * index] == '0')
			sensor = false;
		else if (input[2 * index + 1] == '1' && input[2 * index] == '0')
			sensor = true;
		else if (input[2 * index + 1] == '0' && input[2 * index] == '1')
			sensor = true;
		else
			sensor = false;
	}

	public void checkBlow(string input, int index)
	{
		if (input[index] == '0')
			sensor = false;
		else
			sensor = true;
	}
}

public enum TouchSensor
{
	Prolog,
	TempFarm,
	Factory,
	TempFactory,
	Beruang,
	Gajah,
	Store,
	Singa,
	Games1,
	Games2,
	P2Kanan,
	P2Kiri,
	P1Kanan,
	P1Kiri,
	Sesuatu1,
	Sesuatu2,
	FarmAtas,
	FarmBawah,
	RumahKiri,
	RumahKanan,
	Size
};

public enum SwipeSensor
{
	Farm,
	Rumah,
	Size
};

public enum BlowSensor
{
	Anak,
	Size
};

public class SerialHandler : MonoBehaviour
{

	public static DataSensor[] touchSensor = new DataSensor[(int)TouchSensor.Size];
	public static DataSensor[] swipeSensor = new DataSensor[(int)SwipeSensor.Size];
	public static DataSensor[] blowSensor = new DataSensor[(int)BlowSensor.Size];

	[Tooltip("The serial port where the Arduino is connected (usually COM1-COM9")]
	[SerializeField]
	public static string port = "COM6";

	[Tooltip("The baudrate of the serial port")]
	[SerializeField]
	public static int baudrate = 9600;

	public SerialPort serialPort = new SerialPort(port, baudrate);
	public static bool serial_is_open = false;
	public static bool state_is_calibrate = false;
	char buff;

	// 0x0A is new line
	byte[] cmd = { 0x6A, 0x0A };
	byte[] cmdCalZero = { 0x6A, 0x00, 0x0A };
	byte[] cmdCalOne = { 0x6A, 0x01, 0x0A };

	private string[] portNameList;
	private float timer = 0f;
	private const float timerCount = 5f;

	private bool checkTimer()
	{
		timer += Time.deltaTime;
		return false;
	}

	public static bool getSensorTrig(int index)
	{
		if(index < (int)TouchSensor.Size)
		{
			return touchSensor[index].isChanged;
		}
		else
		{
			index = index - (int)TouchSensor.Size;
			if (index < (int)SwipeSensor.Size)
			{
				return swipeSensor[index].isChanged;
			}
			else
			{ 
				index = index - (int)TouchSensor.Size;
				if (index < (int)BlowSensor.Size)
				{
					return blowSensor[index].isChanged;
				}
				else
				{
					return false;
				}
			}
		}
	}

	public static bool getSensorDown(int index)
	{
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
				serialPort.Write(cmd, 0, cmd.Length);
				string temp = serialPort.ReadLine();
				byte[] arr = System.Text.Encoding.ASCII.GetBytes(temp);
				if (arr[0] != cmd[0])
				{
					serialPort.Close();
					return false;
				}
				else
				{
					Debug.Log("Port Opened!");
					return true;
				}

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

	private void retainSerialCommunication()
	{
		if (serialPort == null)
		{
			if (serialPort.IsOpen)
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
			if (serialPort.IsOpen)
			{
				print("Port is already open");
			}
			else
			{
				print("Port == null");
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
		if(portNameList.Length > 0)
			openSerialCommunication();
	}

	// Update is called once per frame
	void Update()
	{
		serial_is_open = serialPort.IsOpen;
		serialPort.Write(cmd, 0, cmd.Length);
		serialPort.BaseStream.Flush();

		string temp = serialPort.ReadLine();

		byte[] arr = System.Text.Encoding.ASCII.GetBytes(temp);

		string dataTouch = intToBinaryString(arr[2]) + intToBinaryString(arr[3]);
		dataTouch = dataTouch.Substring(0, 7) + dataTouch.Substring(8, 7) ;

		string dataSwipe = intToBinaryString(arr[5]);
		string dataBlow = intToBinaryString(arr[7]);
		string dataCalibrate = intToBinaryString(arr[9]);

		for (int i = 0; i < (int)TouchSensor.Size; i++)
		{
			touchSensor[i].checkTouch(dataTouch, i);
			touchSensor[i].updateData();
		}

		for (int i = 0; i < (int)SwipeSensor.Size; i++)
		{
			swipeSensor[i].checkSwipe(dataSwipe, i);
			swipeSensor[i].updateData();
		}

		for (int i = 0; i < (int)BlowSensor.Size; i++)
		{
			blowSensor[i].checkBlow(dataBlow, i);
			blowSensor[i].updateData();
		}

		retainSerialCommunication();
	}
}
