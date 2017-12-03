int motorPin1Right = 3;
int motorPin1Left = 6;
int motorPin2Left = 10;
int motorPin2Right = 11;

//edit this variable to change the intensity
int first = 255;
int second = 140;
int still = 0;

void setup() {
  Serial.begin(9600);
  pinMode(motorPin1Right, OUTPUT);
  pinMode(motorPin1Left, OUTPUT);
  pinMode(motorPin2Right, OUTPUT);
  pinMode(motorPin2Left, OUTPUT);
}

void loop()
{
  int incomingByte = 0;
  if (Serial.available())
  {
    incomingByte = Serial.read() ;  // read the byte
  }

  /*Serial communication
   * 1 = Player 1 Left Vibration
   * 2 = Player 1 Right Vibration
   * 3 = Player 2 Left Vibration
   * 4 = Player 2 Right Vibration
   */
  switch (incomingByte)
  {
    case 49:
      //Player 1 Left Vibration
      digitalWrite(motorPin1Left, first);
      delay(100);
      digitalWrite(motorPin1Left, 0);
      delay(50);
      digitalWrite(motorPin1Left, second);
      delay(300);
      digitalWrite(motorPin1Left, 0);
      break;

    case 50:
      //Player 1 Right Vibration
      digitalWrite(motorPin1Right, first);
      delay(100);
      digitalWrite(motorPin1Right, 0);
      delay(50);
      digitalWrite(motorPin1Right, second);
      delay(300);
      digitalWrite(motorPin1Right, 0);
      break;  

    case 51:
      //Player 2 Left Vibration
      digitalWrite(motorPin2Left, first);
      delay(100);
      digitalWrite(motorPin2Left, 0);
      delay(50);
      digitalWrite(motorPin2Left, second);
      delay(300);
      digitalWrite(motorPin2Left, 0);
      break;
    
    case 52:
      //Player 2 Right Vibration
      digitalWrite(motorPin2Right, first);
      delay(100);
      digitalWrite(motorPin2Right, 0);
      delay(50);
      digitalWrite(motorPin2Right, second);
      delay(300);
      digitalWrite(motorPin2Right, 0);
      break;  
  }
}
