﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text;
using UnityEngine.SceneManagement;
public class SensorCode : System.Object
{
    //run generator in util folder, edit the size first!
    public static int T0 = 0, T1 = 1, T2 = 2, T3 = 3, T4 = 4, T5 = 5, T6 = 6, T7 = 7, T8 = 8, T9 = 9, T10 = 10, T11 = 11, S0 = 12, S1 = 13, S2 = 14, S3 = 15, S4 = 16, S5 = 17, B0 = 13, size = 19;
}

public class DataSensor : System.Object
{
    private bool sensor = false;
    private bool lastSensor = false;
    private bool startTimer = false;
	private bool startSwapTimer = false;
    public bool isDown = false;
    public bool isUp = false;
    public bool isChanged = false;
	private bool swipeFromZero = false;

	public float responseTime = 0f;
	private float responseTimer = 0f;
	private float timerSwipe = 0f;
	private const float timerSwipeOut = 1f;


    public void updateData()
    {
        isChanged = false;
        isDown = sensor;
        if (startTimer == true)
        {

            updateTimer();
        }
        else
        {
            if (sensor && !lastSensor)
                isChanged = true;
            else
                isChanged = false;

            lastSensor = sensor;

            if (isChanged)
            {
                if (sensor)
                {
                    isDown = true;
                    startTimer = true;
                }
                else
                    isDown = false;
            }
            isUp = !isDown;
        }
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

	public void checkEmulatedSwipe(string input, int index)
	{
		sensor = false;
		if (!startSwapTimer) {
			if (input [index * 2 + 7] == '1') {
				startSwapTimer = true;
				swipeFromZero = true;
			} else if (input [index * 2 + 8] == '1') {
				startSwapTimer = true;
				swipeFromZero = false;
			}
		} else {
			if (timerSwipe > timerSwipeOut) {
				timerSwipe = 0;
				startSwapTimer = false;
				Debug.Log ("timeout");
			} else {
				timerSwipe += Time.deltaTime;
				if (input [index * 2 + 7] == '1' && !swipeFromZero) {
					sensor = true;
				} else if (input [index * 2 + 8] == '1' && swipeFromZero) {
					sensor = true;
				}
			}
		}
	}


    public void checkBlow(string input, int index)
    {
        if (input[index] == '0')
            sensor = false;
        else
            sensor = true;
    }

    private bool updateTimer()
    {
        responseTimer -= Time.deltaTime;
        if (responseTimer < 0)
        {
            responseTimer = responseTime;
            startTimer = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum TouchSensor
{
    Prolog,
    PrologSub,
    Player1Kanan,
    Factory,
    Store,
    Games,
    Games2,
    FarmBawah,
    FarmAtas,
    RumahKiri,
    RumahKanan,
    Player1Kiri = 7,
    Player2Kanan = 5,
    Player2Kiri = 4,
    Size = 11
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
	public static DataSensor[] emulatedSwipeSensor = new DataSensor[(int)SwipeSensor.Size];
    public static DataSensor[] blowSensor = new DataSensor[(int)BlowSensor.Size];

    [Tooltip("The serial port where the Arduino is connected (usually COM1-COM9")]
    [SerializeField]
    private string port = "COM7";
    public static string port_static;

    [Tooltip("The baudrate of the serial port")]
    [SerializeField]
    private int baudrate = 9600;
    public static int baudrate_static;

    public SerialPort serialPort = new SerialPort();
    public static bool serial_is_open = true;
    private bool state_is_calibrate = false;
    private static bool serial_is_proper = false;
    private bool changeSceneStart = false;
    private float changeSceneTimer = 0f;
	[SerializeField] private const float changeSceneTime = 4f;
    char buff;

    // 0x0A is new line
    byte[] cmd = { 0x6A };
    byte[] cmdCal = { 0x6B };

    private string[] portNameList;
    private int portNameLength = 0;
    private int portNameIndex = 0;
    private bool start = true;
    private int sensorNum = (int)TouchSensor.Size;
    private int sensorIndex = 0;
	private bool transitionState = false;

    private GameObject sensorPlacement;

    private float timer = 0f;
	[SerializeField] private const float timerCount = 5f;
	private float transitionTimer = 0f;
	[SerializeField] private const float transitionTimerCount = 15f;

	public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity) {
		DateTime initialTime = DateTime.Now;
		DateTime nowTime;
		TimeSpan diff = default(TimeSpan);

		string dataString = null;

		do {
			try {
				dataString = serialPort.ReadLine();
			}
			catch (TimeoutException) {
				dataString = null;
			}

			if (dataString != null)
			{
				callback(dataString);
				yield return null;
			} else
				yield return new WaitForSeconds(0.05f);

			nowTime = DateTime.Now;
			diff = nowTime - initialTime;

		} while (diff.Milliseconds < timeout);

		if (fail != null)
			fail();
		yield return null;
	}

    private bool checkTimer()
    {
        timer += Time.deltaTime;
        if (timer > timerCount)
        {
            timer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool getSensorTrig(int index)
    {
        if (index < (int)TouchSensor.Size)
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
                index = index - (int)SwipeSensor.Size;
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
        if (index < (int)TouchSensor.Size)
        {
            return touchSensor[index].isDown;
        }
        else
        {
            index = index - (int)TouchSensor.Size;
            if (index < (int)SwipeSensor.Size)
            {
				return (swipeSensor[index].isDown || emulatedSwipeSensor[index].isDown);
            }
            else
            {
                index = index - (int)SwipeSensor.Size;
                if (index < (int)BlowSensor.Size)
                {
                    return blowSensor[index].isDown;
                }
                else
                {
                    return false;
                }
            }
        }
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

    //not yet used but will be used for plug and play
    private bool checkSerialProper()
    {
        if (portNameIndex < portNameLength)
        {
            serialPort.PortName = portNameList[portNameIndex];
            serialPort.Open();
            return false;
        }
        else
        {
            portNameList = SerialPort.GetPortNames();
            portNameLength = portNameList.Length;
            portNameIndex = 0;
            return false;
        }
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
                Debug.Log("Openning up new comm at " + serialPort.PortName);
                serialPort.Open();
                serialPort.ReadTimeout = 33;
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
		Cursor.visible = false;
        baudrate_static = baudrate;
        port_static = port;
        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < (int)TouchSensor.Size; i++)
            touchSensor[i] = new DataSensor();
        for (int i = 0; i < (int)SwipeSensor.Size; i++)
            swipeSensor[i] = new DataSensor();
		for (int i = 0; i < (int)SwipeSensor.Size; i++)
			emulatedSwipeSensor[i] = new DataSensor();
        for (int i = 0; i < (int)BlowSensor.Size; i++)
            blowSensor[i] = new DataSensor();
        
        touchSensor[(int)TouchSensor.Player1Kiri].responseTime = 0.05f;
        touchSensor[(int)TouchSensor.Player1Kanan].responseTime = 0.05f;
        touchSensor[(int)TouchSensor.Player2Kiri].responseTime = 0.05f;
        touchSensor[(int)TouchSensor.Player2Kanan].responseTime = 0.05f;
        

        // Initialize serial protocol
        serialPort.PortName = port_static;
        serialPort.BaudRate = baudrate_static;
        serialPort.Parity = Parity.None;
        serialPort.StopBits = StopBits.One;
        serialPort.DataBits = 8;

        portNameList = SerialPort.GetPortNames();
        portNameLength = portNameList.Length;

        for (int i = 0; i < portNameLength; i++)
            Debug.Log(portNameList[i]);
        if (portNameLength > 0)
        {
            serial_is_proper = openSerialCommunication();
        }
        Debug.Log(serial_is_proper);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSceneStart)
        {
			Debug.Log ("Changing Scne");
          
            if (changeSceneTimer > changeSceneTime)
            {
				//Buat ganti scene, ngejawab ardu
                serialPort.Write(cmdCal, 0, cmdCal.Length);
                serialPort.BaseStream.Flush();
                changeSceneStart = false;
                changeSceneTimer = 0;
				sensorIndex = 0;
                Debug.Log("ChangeScene3");
                state_is_calibrate = !state_is_calibrate;
				if (state_is_calibrate) {
					SceneManager.LoadScene ("Calibration");
					transitionState = true;
					Debug.Log ("to calib");
					changeSceneStart = false;
				}
                else
                {
                    SceneManager.LoadScene("GABUNG");
					Debug.Log ("to MainScene");
					changeSceneStart = false;
					serialPort.BaseStream.Flush ();
                }
            }
            else
            {
				changeSceneTimer += Time.deltaTime;
            }
        }
        if (serial_is_proper)
        {
            if (state_is_calibrate)
            {
				if (transitionState) {
					if (transitionTimer == 0f) {
						/*
						serialPort.Write (cmdCal, 0, cmdCal.Length);
						serialPort.BaseStream.Flush ();
						*/
					}
					if (transitionTimer > transitionTimerCount) {
						transitionState = false;
						transitionTimer = 0;
						CalibrationHandler.static_sensorPlacement [sensorIndex].SetActive (true);
					} else {
						transitionTimer += Time.deltaTime;
					}
				} else {
					if (checkTimer ()) {
						timer = 0;
						Debug.Log (cmdCal [0]);

						if (sensorIndex == 0) {
							
						} else if (sensorIndex < sensorNum) {
							CalibrationHandler.static_sensorPlacement [sensorIndex].SetActive (true);
							CalibrationHandler.static_sensorPlacement [sensorIndex - 1].SetActive (false);
						} else {
							CalibrationHandler.static_sensorPlacement [sensorIndex - 1].SetActive (false);
							changeSceneStart = true;
							Debug.Log ("ChangeScene1");
						}
						serialPort.Write (cmdCal, 0, cmdCal.Length);
						serialPort.BaseStream.Flush ();
						Debug.Log ("sensor index " + sensorIndex);
						sensorIndex++;
					}
				}
            }
            else
            {
                serial_is_open = serialPort.IsOpen;
                serialPort.Write(cmd, 0, cmd.Length);

                serialPort.BaseStream.Flush();
                string temp = "";
                for (int i = 0; i < 10; i++)
                    temp += (char)0;

                try
                {
                    temp = serialPort.ReadLine();
                }
                catch
                {
                    Debug.Log("timeout");
                }

                byte[] arr = System.Text.Encoding.ASCII.GetBytes(temp);
                string fulltemp = "| ";
                for (int i = 0; i < 10; i++)
                    fulltemp += intToBinaryString(arr[i]) + " | ";
                Debug.Log(fulltemp);

                string dataTouch = intToBinaryString(arr[2]) + intToBinaryString(arr[3]);
                dataTouch = dataTouch.Substring(8, 7) + dataTouch.Substring(0, 4);

                string dataSwipe = intToBinaryString(arr[5]);
                string dataBlow = intToBinaryString(arr[7]);
                string dataCalibrate = intToBinaryString(arr[9]);
				string dataEmulatedSwipe = "";
				for (int i = 0; i < 2; i++) {
					dataEmulatedSwipe += (emulatedSwipeSensor [i].isDown ? '1' : '0');
				}
				Debug.Log(dataTouch.Substring(0,7) + " " + dataTouch.Substring(7, 4) + " " + dataSwipe.Substring(0,2) + " " + dataEmulatedSwipe + " " + dataBlow + " " + dataCalibrate);
				if (temp [0] != (char)0) {
					Debug.Log ("masuk cuy");
					for (int i = 0; i < (int)TouchSensor.Size; i++) {
						touchSensor [i].checkTouch (dataTouch, i);
						touchSensor [i].updateData ();
						ScreensaverTimer.resetTimer ();
					}

					for (int i = 0; i < (int)SwipeSensor.Size; i++) {
						swipeSensor [i].checkSwipe (dataSwipe, i);
						swipeSensor [i].updateData ();
						emulatedSwipeSensor [i].checkEmulatedSwipe (dataTouch, i);
						emulatedSwipeSensor [i].updateData ();
						ScreensaverTimer.resetTimer ();
					}

					for (int i = 0; i < (int)BlowSensor.Size; i++) {
						blowSensor [i].checkBlow (dataBlow, i);
						blowSensor [i].updateData ();
						ScreensaverTimer.resetTimer ();
					}

					if (dataCalibrate [0] == '1') {
						changeSceneStart = true;
						Debug.Log ("ChangeScene2");
						ScreensaverTimer.resetTimer ();
					}
				}
            }
        }
        else
        {
            if (portNameLength > 0)
            {
                serial_is_proper = openSerialCommunication();
            }
        }
    }
    void OnApplicationQuit()
    {
        serialPort.Close();
        Debug.Log("Port closed!");
    }
}
