using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class GameEngine : MonoBehaviour
{
    public static SerialPort sp = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
    public static int paddleCounter1;
    public static int paddleCounter2;
    public KeyCode moveLeft1 = KeyCode.LeftArrow;
	public KeyCode moveRight1 = KeyCode.RightArrow;
	public KeyCode moveLeft2 = KeyCode.A;
	public KeyCode moveRight2 = KeyCode.D;
	public KeyCode L1P1 = KeyCode.Joystick1Button4;
	public KeyCode L2P1 = KeyCode.Joystick1Button6;
	public KeyCode R1P1 = KeyCode.Joystick1Button5;
	public KeyCode R2P1 = KeyCode.Joystick1Button7;
    public KeyCode L1P2 = KeyCode.Joystick2Button4;
    public KeyCode L2P2 = KeyCode.Joystick2Button6;
    public KeyCode R1P2 = KeyCode.Joystick2Button5;
    public KeyCode R2P2 = KeyCode.Joystick2Button7;
    private int controllerNumber;
    public static string selectedOutput;
    public static string selectedInput;
    public float speed = 10.0f;
	public float boundX = 5.0f;
	private Rigidbody2D rb2d;
	public enum InputDevice { Keyboard1, Keyboard2, PS4Controller1, PS4Controller2};
	public enum OutputDevice { None, Vibrators, Sound, Combination };
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
	public static int outputEffectUsed = 0;

	void Start()
	{
        OpenConnection();
        leftWallX = (int) GameObject.FindGameObjectWithTag("leftWall").transform.position.x;
		topWallY = (int) GameObject.FindGameObjectWithTag("topWall").transform.position.y;
		width = leftWallX * 2;
		height =  topWallY * 2;
		rb2d = GetComponent<Rigidbody2D>();
        if (this.name == "Player01") {
            generateBricks();
        }
        switch (outputDevice)
        {
            case OutputDevice.None:
                Debug.Log("No output device selected");
                selectedOutput = "None";
                break;
            case OutputDevice.Vibrators:
                Debug.Log("Vibrators selected");
                selectedOutput = "Vibrator";
                break;
            case OutputDevice.Sound:
                Debug.Log("Sound pitch selected");
                selectedOutput = "Sound";
                break;
            case OutputDevice.Combination:
                Debug.Log("Sound frequency selected");
                selectedOutput = "Combination";
                break;
        }
    }


	// Update is called once per frame
	void Update()
	{
		time = (int)Time.realtimeSinceStartup;
		var vel = rb2d.velocity;

		if(inputDevice == InputDevice.PS4Controller1) 
		{
            controllerNumber = 1;           
            System.Array values = System.Enum.GetValues(typeof(KeyCode));
            foreach (KeyCode code in values)
            {
                if (Input.GetKeyDown(code)) { print(System.Enum.GetName(typeof(KeyCode), code)); }
            }
            
            selectedInput = "PS4Controller";
		    float controllerSpeed = (Input.GetAxis("hp1"));
			vel.x = controllerSpeed*10;            
            
            if(selectedOutput == "Vibrator")
            {
                if (Input.GetKeyDown(L2P1))
                {
                    Debug.Log("L2P1 Pressed!");
                    hapticOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }

                else if (Input.GetKeyDown(R2P1))
                {
                    Debug.Log("R2P1 Pressed!");
                    hapticOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }            
            }

            else if (selectedOutput == "Sound")
            {
                if (Input.GetKeyDown(L1P1))
                {
                    Debug.Log("L1P1 Pressed!");
                    audioOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }

                else if (Input.GetKeyDown(R1P1))
                {
                    Debug.Log("R1P1 Pressed!");
                    audioOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }

            }

            else if(selectedOutput == "Combination")
            {
                if (Input.GetKeyDown(L1P1))
                {
                    Debug.Log("L1P1 Pressed!");
                    hapticOutput("Left", controllerNumber);
                    audioOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }
                else if (Input.GetKeyDown(L2P1))
                {
                    Debug.Log("L2P1 Pressed!");
                    hapticOutput("Left", controllerNumber);                    
                    audioOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }
                else if (Input.GetKeyDown(R1P1))
                {
                    Debug.Log("R1P1 Pressed!");
                    audioOutput("Right", controllerNumber);
                    hapticOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }
                else if (Input.GetKeyDown(R2P1))
                {
                    Debug.Log("R2P1 Pressed!");
                    hapticOutput("Right", controllerNumber);
                    audioOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }
            }		
		}

        if (inputDevice == InputDevice.PS4Controller2)
        {
            controllerNumber = 2;        
            System.Array values = System.Enum.GetValues(typeof(KeyCode));
            foreach (KeyCode code in values)
            {
                if (Input.GetKeyDown(code)) { print(System.Enum.GetName(typeof(KeyCode), code)); }
            }

            selectedInput = "PS4Controller";
            float controllerSpeed = (Input.GetAxis("hp2"));
            vel.x = controllerSpeed * 10;

            if(selectedOutput == "Vibrator")
            {
                if (Input.GetKeyDown(L2P2))
                {
                    Debug.Log("L2P2 Pressed!");
                    hapticOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }

                else if (Input.GetKeyDown(R2P2))
                {
                    Debug.Log("R2P2 Pressed!");
                    hapticOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }
            }

            if(selectedOutput == "Sound")
            {
                if (Input.GetKeyDown(L1P2))
                {
                    Debug.Log("L1P2 Pressed!");
                    audioOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }

                else if (Input.GetKeyDown(R1P2))
                {
                    Debug.Log("R1P2 Pressed!");
                    audioOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }
            }

            if(selectedOutput == "Combination")
            {
                if (Input.GetKeyDown(L1P2))
                {
                    Debug.Log("L1P1 Pressed!");
                    hapticOutput("Left", controllerNumber);
                    audioOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }
                else if (Input.GetKeyDown(L2P2))
                {
                    Debug.Log("L2P1 Pressed!");
                    hapticOutput("Left", controllerNumber);
                    audioOutput("Left", controllerNumber);
                    outputEffectUsed++;
                }
                else if (Input.GetKeyDown(R1P2))
                {
                    Debug.Log("R1P1 Pressed!");
                    audioOutput("Right", controllerNumber);
                    hapticOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }
                else if (Input.GetKeyDown(R2P2))
                {
                    Debug.Log("R2P1 Pressed!");
                    hapticOutput("Right", controllerNumber);
                    audioOutput("Right", controllerNumber);
                    outputEffectUsed++;
                }
            }                    
        }

        else if (inputDevice == InputDevice.Keyboard1) {
           selectedInput = "Keyboard";
			if (Input.GetKey (moveRight1)) {
				vel.x = speed;
			} else if (Input.GetKey (moveLeft1)) {
				vel.x = -speed;
			} else if (!Input.anyKey) {
				vel.x = 0;
			}
		}
		else if (inputDevice == InputDevice.Keyboard2) {
           selectedInput = "Keyboard";
			if (Input.GetKey (moveRight2)) {
				vel.x = speed;
			} else if (Input.GetKey (moveLeft2)) {
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

	void generateBricks(){
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
	void hapticOutput(string Side, int number){
		if(Side == "Left"){
            if(number == 1)
            {
                Debug.Log("I got the left side!, haptic");
                if (sp.IsOpen)
                {
                    sp.Write("1");
                }
            }
            else if (number == 2)
            {
                Debug.Log("I got the left side!, haptic");
                if (sp.IsOpen)
                {
                    sp.Write("3");
                }
            }


        }
		else if(Side == "Right"){
            if(number == 1)
            {
                Debug.Log("I got the right side!, haptic");
                if (sp.IsOpen)
                {
                    sp.Write("2");
                }

            }
            else if (number == 2)
            {
                Debug.Log("I got the right side!, haptic");
                if (sp.IsOpen)
                {
                    sp.Write("4");
                }

            }
			
        }
	}
	void audioOutput(string Side, int number){
		if(Side == "Left"){
            if(number == 1)
            {
                Debug.Log("I got the left side!, audio");
                if (sp.IsOpen)
                {
                    sp.Write("7");
                }
            }
            if (number == 2)
            {
                Debug.Log("I got the left side!, audio");
                if (sp.IsOpen)
                {
                    sp.Write("5");
                }

            }
           
        }
		else if(Side == "Right"){
            if (number == 1)
            {
                Debug.Log("I got the right side!, audio");
                if (sp.IsOpen)
                {
                    sp.Write("8");
                }

            }
            if (number == 2)
            {
                Debug.Log("I got the right side!, audio");
                if (sp.IsOpen)
                {
                    sp.Write("6");
                }

            }
            
        }
	}



    public void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                //sp.Close();
                //Debug.Log("Closing port, because it was already open!");
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
                Debug.Log("Port is already open");
            }
            else
            {
                Debug.Log("Port == null");
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball01" || collision.gameObject.name == "Ball02")
        {
            if(this.gameObject.name == "Player01")
            {
                paddleCounter1++;
            }

            else if (this.gameObject.name == "Player02")
            {
                paddleCounter2++;
            }
        }
    }



}


