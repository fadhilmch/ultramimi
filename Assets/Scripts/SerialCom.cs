/* ArduinoConnector by Alan Zucconi
 * http://www.alanzucconi.com/?p=2979
 */
using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

public class ArduinoConnector : MonoBehaviour {

    /* The serial port where the Arduino is connected. */
    [Tooltip("The serial port where the Arduino is connected")]
    public string port = "COM4";
    /* The baudrate of the serial port. */
    [Tooltip("The baudrate of the serial port")]
    public int baudrate = 9600;
    public SerialPort serialPort;

    public void Open () {
        // Opens the serial port
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
        //this.serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
    }

    public void WriteToArduino(string message)
    {
        // Send the request
        serialPort.WriteLine(message);
        serialPort.BaseStream.Flush();
    }

    public string ReadFromArduino(int timeout = 0)
    {
        serialPortReadTimeout = timeout;
        try
        {
            return serialPort.ReadLine();
        }
        catch (TimeoutException)
        {
            return null;
        }
    }
    

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            // A single read attempt
            try
            {
                dataString = serialPort.ReadLine();
            }
            catch (TimeoutException)
            {
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

    public void Close()
    {
        serialPort.Close();
        Debug.Log("Port Closed!");
    }
}