using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	float speed = 5.0f;
	float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
	{
		return (ballPos.x - racketPos.x) / racketWidth;
	}
	private Rigidbody2D rb2d;
	private Vector2 vel;
	public static int points = 0;
	public int dead = 0;
	public List<SpriteRenderer> list;


	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Invoke("AddForceToBall", 2);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void AddForceToBall()
	{
		float rand = Random.Range(0, 2);
		if (rand < 1)
		{
			rb2d.AddForce(new Vector2(20, -15));
		}
		else
		{
			rb2d.AddForce(new Vector2(-20, -15));
		}
	}

	void ResetBall()
	{
		vel = Vector2.zero;
		rb2d.velocity = vel;
		transform.position = Vector2.zero;
		if(this.name == "Ball01"){
			transform.position = new Vector3(2,0,0);
		}
		if(this.name == "Ball02")
		{
			transform.position = new Vector3(-2,0,0);
		}
	}

	void RestartGame()
	{
		ResetBall();
		Invoke("AddForceToBall", 1);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
        //Route to root of game folder
        string path = System.IO.Path.GetDirectoryName(Application.dataPath) + "/points.txt";        

        if (col.gameObject.name == "green_brick(Clone)" || col.gameObject.name == "blue_brick(Clone)"
			|| col.gameObject.name == "red_brick(Clone)" || col.gameObject.name == "yellow_brick(Clone)")
		{
            //Save points to points file
			if(Bricks.bricks == 0){
				int time2 = GameObject.Find ("Player01").GetComponent<GameEngine> ().time;
				System.IO.File.WriteAllText(path +
					System.DateTime.Now.ToString("dd-MM-yy_hh-mm-ss")+".txt", "Seconds: " + time2 +"\nPoints: "+
					points.ToString()+"\nDead: "+dead+"\nInput device: "+GameEngine.selectedInput+"\nOutput method: "+GameEngine.selectedOutput+"\nOutput effect used: "+GameEngine.outputEffectUsed);
					GameEngine[] gameEngines = GameObject.FindObjectsOfType<GameEngine>();
					BallController[] balls = GameObject.FindObjectsOfType<BallController>();
					gameEngines[0].GetComponent<SpriteRenderer>().enabled = false;
					gameEngines[1].GetComponent<SpriteRenderer>().enabled = false;
					balls[0].GetComponent<SpriteRenderer>().enabled = false;
					balls[1].GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		else if (col.gameObject.name == "BottomWall")
		{
			dead++;
			RestartGame ();
		}
		else if (col.gameObject.name == "Player01") {
			// Calculate hit Factor
			float x = hitFactor(transform.position,
				col.transform.position,
				col.collider.bounds.size.x);

			// Calculate direction, set length to 1
			Vector2 dir = new Vector2(x, 1).normalized;

			// Set Velocity with dir * speed
			rb2d.velocity = dir*speed;
		}
		else if (col.gameObject.name == "Player02") {
			// Calculate hit Factor
			float x = hitFactor(transform.position,
				col.transform.position,
				col.collider.bounds.size.x);

			// Calculate direction, set length to 1
			Vector2 dir = new Vector2(x, 1).normalized;

			// Set Velocity with dir * speed
			rb2d.velocity = dir*speed;
		}

	}
}