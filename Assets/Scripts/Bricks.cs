using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
	public static int bricks = 44;
	// Use this for initialization
	void Start()
	{
		

	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnCollisionExit2D(Collision2D col)
	{        
        bricks--;
        //Debug.Log(bricks);
		BallController.points++;
        Destroy(gameObject);
    }

}