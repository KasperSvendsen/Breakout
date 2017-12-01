using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class GameEngine : MonoBehaviour
{
    public static SerialPort sp = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
    public KeyCode moveLeft = KeyCode.LeftArrow;
	public KeyCode moveRight = KeyCode.RightArrow;
	public KeyCode L1 = KeyCode.Joystick1Button4;
	public KeyCode L2 = KeyCode.Joystick1Button6;
	public KeyCode R1 = KeyCode.Joystick1Button5;
	public KeyCode R2 = KeyCode.Joystick1Button7;
	public float speed = 10.0f;
	public float boundX = 5.0f;
	private Rigidbody2D rb2d;
	public enum InputDevice { Keyboard, PS4Controller };
	public enum OutputDevice { None, Vibrators, SoundPitch, SoundFrequency };
	public InputDevice inputDevice;
	public OutputDevice outputDevice;
	public int bricks = 10;
	public float spacing = 1.0f;
	public Transform green_brick;
	public Transform blue_brick;
	public Transform red_brick;
	public Transform yellow_brick;
	public int leftWallX; 
	public int topWallY;
	public int width; 
	public int height;
	public int points;
	public int time; 

	void Start()
	{
        OpenConnection();
        leftWallX = (int) GameObject.FindGameObjectWithTag("leftWall").transform.position.x;
		topWallY = (int) GameObject.FindGameObjectWithTag("topWall").transform.position.y;
		width = leftWallX * 2;
		height =  topWallY * 2;
		rb2d = GetComponent<Rigidbody2D>();
		generateBricks (1);
		switch (outputDevice)
		{
		case OutputDevice.None:
			Debug.Log("No output device selected");
			break;
		case OutputDevice.Vibrators:
			Debug.Log("Vibrators selected");
			break;
		case OutputDevice.SoundPitch:
			Debug.Log("Sound pitch selected");
			break;
		case OutputDevice.SoundFrequency:
			Debug.Log("Sound frequency selected");
			break;
		}
	}


	// Update is called once per frame
	void Update()
	{
		time = (int)Time.realtimeSinceStartup;
		var vel = rb2d.velocity;

		if(inputDevice == InputDevice.PS4Controller) 
		{
			float controllerSpeed = (Input.GetAxis("Horizontal"));
			vel.x = controllerSpeed*10;
			if(Input.GetKey(L1)){
				Debug.Log("L1 Pressed!");
				audioOutput ("Left");
			}
			else if(Input.GetKey(L2)){
				Debug.Log("L2 Pressed!");
				audioOutput ("Right");
			}
			else if(Input.GetKey(R1)){
				Debug.Log("R1 Pressed!");
				hapticOutput ("Left");
			}
			else if(Input.GetKey(R2)){
				Debug.Log("R2 Pressed!");
				hapticOutput ("Right");
			}

		}

		else if (inputDevice == InputDevice.Keyboard) {
           
			if (Input.GetKey (moveRight)) {
				vel.x = speed;
			} else if (Input.GetKey (moveLeft)) {
				vel.x = -speed;
			} else if (!Input.anyKey) {
				vel.x = 0;
			}
		}
		rb2d.velocity = vel;

		var pos = transform.position;
		if (pos.x > boundX)
		{
			pos.x = boundX;
		}

		else if (pos.x < -boundX)
		{
			pos.x = -boundX;
		}

		transform.position = pos;
	}
	void generateBricks(int bricks){
		float startX = (width/2)+ 0.3f;
		float startY = 3+0.3f;
		for(int x = 0; x < 11; x++){
			//ny = Instantiate(BrickPrefab, new Vector3(startX, startY, 0), Quaternion.Euler(0,0,0)).gameObject;
			//ny.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color",Color.blue);
			Instantiate (green_brick, new Vector3(startX,startY,0), Quaternion.Euler(0,0,0));
			startX += 1.14f;
		}
		startX = (width/2)+ 0.3f;
		for(int x = 0; x < 11; x++){
			Instantiate (blue_brick, new Vector3(startX,2.7f,0), Quaternion.Euler(0,0,0));
			startX += 1.14f;
		}
		startX = (width/2)+ 0.3f;
		for(int x = 0; x < 11; x++){
			Instantiate (red_brick, new Vector3(startX,2.1f,0), Quaternion.Euler(0,0,0));
			startX += 1.14f;
		}
		startX = (width/2)+ 0.3f;
		for(int x = 0; x < 11; x++){
			Instantiate (yellow_brick, new Vector3(startX,1.5f,0), Quaternion.Euler(0,0,0));
			startX += 1.14f;

		}
	}
	void hapticOutput(string Side){
		if(Side == "Left"){
			Debug.Log ("I got the left side!, haptic");
            Debug.Log("You pressed 1");
            if (sp.IsOpen)
            {
                sp.Write("1");
            }

        }
		else if(Side == "Right"){
			Debug.Log ("I got the right side!, haptic");
            Debug.Log("You pressed 2");
            if (sp.IsOpen)
            {
                sp.Write("2");
            }
        }
	}
	void audioOutput(string Side){
		if(Side == "Left"){
			Debug.Log ("I got the left side!, audio");
            Debug.Log("You pressed 1");
            if (sp.IsOpen)
            {
                sp.Write("3");
            }
        }
		else if(Side == "Right"){
			Debug.Log ("I got the right side!, audio");
            Debug.Log("You pressed 1");
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
                Debug.Log("Closing port, because it was already open!");
            }
            else
            {
                sp.Open();  // opens the connection
                sp.ReadTimeout = 100;  // sets the timeout value before reporting error
                Debug.Log("Port Opened!");
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



}
