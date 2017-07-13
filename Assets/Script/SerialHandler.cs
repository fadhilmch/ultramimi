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
    public bool IsTriggered = false;
    private bool lastSensor = false;

    public void isToggled()
    {
        if (sensor && !lastSensor)
            IsTriggered = true;
        else
            IsTriggered = false;

        lastSensor = sensor;
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
    public static bool getSensorTrig(int index)
    {
        return true;
    }


    [Tooltip("The serial port where the Arduino is connected (usually COM1-COM9")]
    [SerializeField]
    public static string port = "COM6";

    [Tooltip("The baudrate of the serial port")]
    [SerializeField]
    public static int baudrate = 9600;

    public SerialPort serialPort = new SerialPort(port, baudrate);
    public static bool serial_is_open = false;
    byte[] cmd = { 0x6A };


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

    private void openSerialCommunication()
    {
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

        string dataTouch = intToBinaryString(arr[2]) + intToBinaryString(arr[3]) + intToBinaryString(arr[4]);
        dataTouch = dataTouch.Substring(0, 7) + dataTouch.Substring(8, 7) + dataTouch.Substring(16, 6);

        string dataSwipe = intToBinaryString(arr[6]);
        string dataBlow = intToBinaryString(arr[8]);

        for (int i = 0; i < (int)TouchSensor.Size; i++)
        {
            touchSensor[i].checkTouch(dataTouch, i);
            touchSensor[i].isToggled();
        }

        for (int i = 0; i < (int)SwipeSensor.Size; i++)
        {
            swipeSensor[i].checkSwipe(dataSwipe, i);
            swipeSensor[i].isToggled();
        }

        for (int i = 0; i < (int)BlowSensor.Size; i++)
        {
            blowSensor[i].checkBlow(dataBlow, i);
            blowSensor[i].isToggled();
        }

        retainSerialCommunication();
    }
}
