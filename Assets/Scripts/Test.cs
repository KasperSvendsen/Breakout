using UnityEngine;
using System.Collections;
using System.IO.Ports;
public class Test : MonoBehaviour
{
    public static SerialPort sp = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
    public int count = 0;
    public string message, message1;
    public int message2;

    void Start()
    {
        OpenConnection();
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("You pressed 1");
            if (sp.IsOpen)
            {
                sp.Write("1");
            }
        }

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("You pressed 2");
            if (sp.IsOpen)
            {
                sp.Write("2");
            }
        }

        if (Input.GetKeyDown("3"))
        {
            Debug.Log("You pressed 3");
            if (sp.IsOpen)
            {
                sp.Write("3");
            }
        }

        if (Input.GetKeyDown("4"))
        {
            Debug.Log("You pressed 4");
            if (sp.IsOpen)
            {
                sp.Write("4");
            }
        }
    }

    public void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                message = "Closing port, because it was already open!";
            }
            else
            {
                sp.Open();  // opens the connection
                sp.ReadTimeout = 100;  // sets the timeout value before reporting error
                message = "Port Opened!";
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }
    void OnApplicationQuit()
    {
        sp.Close();
    }

}