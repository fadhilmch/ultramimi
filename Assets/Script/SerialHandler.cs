using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text;

public class DataSensor : System.Object
{
    public bool sensor;
    public bool sensorIsTriggered;
    public bool lastSensor;
}


public class SerialHandler : MonoBehaviour
{

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

    public DataSensor[] touchSensor = new DataSensor[(int)TouchSensor.Size];
    public DataSensor[] swipeSensor = new DataSensor[(int)SwipeSensor.Size];
    public DataSensor[] blowSensor = new DataSensor[(int)BlowSensor.Size];

    [Tooltip("The serial port where the Arduino is connected (usually COM1-COM9")]
    [SerializeField]
    public static string port = "COM6";

    [Tooltip("The baudrate of the serial port")]
    [SerializeField]
    public static int baudrate = 9600;

    public SerialPort serialPort = new SerialPort(port, baudrate);
    public bool serial_is_open = false;
    char buff;


    bool checkState(bool now, bool past)
    {
        if (now && !past)
        {
            return true;
        }
        else
            return false;
    }

    int checkTouch(string input, int index)
    {
        if (input[index] == '0')
            return 0;
        else
            return 1;
    }

    int checkSwipe(string input, int index)
    {
        if (input[2 * index + 1] == '0' && input[2 * index] == '0')
            return 0;
        else if (input[2 * index + 1] == '1' && input[2 * index] == '0')
            return 1;
        else if (input[2 * index + 1] == '0' && input[2 * index] == '1')
            return 1;
        else
            return 2;
    }

    int checkBlow(string input, int index)
    {
        if (input[index] == '0')
            return 0;
        else
            return 1;
    }

    string intToBin(byte n)
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

    void Initialization()
    {
        // Assign all value to false
        for (int i = 0; i < (int)TouchSensor.Size; i++)
        {
            touchSensor[i].sensor = false;
            touchSensor[i].lastSensor = false;
            touchSensor[i].sensorIsTriggered = false;
        }

        for (int i = 0; i < (int)SwipeSensor.Size; i++)
        {
            swipeSensor[i].sensor = false;
            swipeSensor[i].lastSensor = false;
            swipeSensor[i].sensorIsTriggered = false;
        }

        for (int i = 0; i < (int)BlowSensor.Size; i++)
        {
            blowSensor[i].sensor = false;
            blowSensor[i].lastSensor = false;
            blowSensor[i].sensorIsTriggered = false;
        }


    }

    // Use this for initialization
    void Start()
    {
        Initialization();

        // Initialize serial protocol
        serialPort.Parity = Parity.None;
        serialPort.StopBits = StopBits.One;
        serialPort.DataBits = 8;

        if (serialPort != null)
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

    // Update is called once per frame
    void Update()
    {
        serial_is_open = serialPort.IsOpen;


        byte[] cmd = { 0x6A };
        serialPort.Write(cmd, 0, cmd.Length);
        serialPort.BaseStream.Flush();

        // Store the received message in temp variable
        string temp = serialPort.ReadLine();

        // Encode string to array
        byte[] arr = System.Text.Encoding.ASCII.GetBytes(temp);

        string dataTouch = intToBin(arr[2]) + intToBin(arr[3]) + intToBin(arr[4]);
        dataTouch = dataTouch.Substring(0, 7) + dataTouch.Substring(8, 7) + dataTouch.Substring(16, 6);

        string dataSwipe = intToBin(arr[6]);

        string dataBlow = intToBin(arr[8]);
        //GameController1 test;

        for (int i = 0; i < (int)TouchSensor.Size; i++)
        {
            touchSensor[i].sensor = checkTouch(dataTouch, i) == 1;
            touchSensor[i].sensorIsTriggered = checkState(touchSensor[i].sensor, touchSensor[i].lastSensor);
        }

        for (int i = 0; i < (int)SwipeSensor.Size; i++)
        {
            swipeSensor[i].sensor = checkSwipe(dataSwipe, i) == 1;
            swipeSensor[i].sensorIsTriggered = checkState(swipeSensor[i].sensor, swipeSensor[i].lastSensor);
        }

        for (int i = 0; i < (int)TouchSensor.Size; i++)
        {
            blowSensor[i].sensor = checkBlow(dataBlow, i) == 1;
            blowSensor[i].sensorIsTriggered = checkState(blowSensor[i].sensor, blowSensor[i].lastSensor);
        }

    }
}
