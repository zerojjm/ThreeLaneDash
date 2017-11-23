using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		inputX = 0;
		targetLane = 2;
		prevX = -10000;
		isDead = false;
		carDefaultYPos = -3.4f;
		onlyCarMoves = false;
		speed = 0;
		max_speed = 200;
		score = 0;
		stopped = true;
		crashAnimationTimer = 0;
		finished = false;
		moveDir = 0;
		handleButtonPress = false;
		xSpeed = 0;
	}

	public float prevX;
	
	static public bool isDead;
	static public bool stopped;
	static public bool finished;

	static public bool onlyCarMoves;

	static public int targetLane;
	private int moveDir;
	static public bool handleButtonPress;

	static public int score;

	private float inputX;
	// 2 - Store the movement
	static public Vector2 movement;

	public List<float> rotationList; // For drifting
	float delayTimer;

	public float speed;
	static private float max_speed;

	public static float getMaxSpeed() {return max_speed;}

	private float carDefaultYPos;

	private float crashAnimationTimer;

	private float xSpeed;

	public static void setMaxSpeed(float newMax)
	{
		if (SelectedLevelScript.selectedCar == 2) 
		{
			newMax *= 0.85f;
		}
		max_speed = newMax;
	}
	// Update is called once per frame
	void Update () {

		/*if (rigidbody2D.position.x < BackgroundScript.stage_bounds.min.x || rigidbody2D.position.x > BackgroundScript.stage_bounds.max.x
						|| rigidbody2D.position.y < BackgroundScript.stage_bounds.min.y || rigidbody2D.position.y > BackgroundScript.stage_bounds.max.y)
			handleCrash ();*/

		if (isDead || stopped)
		{
			crashAnimation();
			movement = new Vector2(0,0);
			return;
		}

		float turnAmount = 0.05f;

		float targetXPos = 0;

		if (targetLane == 1)
			targetXPos = BackgroundScript.stage_bounds.min.x + BackgroundScript.stage_bounds.size.x / 6;
		else if (targetLane == 2)
			targetXPos = BackgroundScript.stage_bounds.min.x + BackgroundScript.stage_bounds.size.x / 2;
		else if (targetLane == 3)
			targetXPos = BackgroundScript.stage_bounds.min.x + 5*BackgroundScript.stage_bounds.size.x / 6;


		if (speed < max_speed)
			accelerate ();


		//handleButtonPress...
		if (handleButtonPress) 
		{
			if (transform.position.x < targetXPos) 
			{
				xSpeed = 5*speed;
			}
			else if (transform.position.x > targetXPos) 
			{
				xSpeed =-5*speed;
			}
			if (Math.Abs (transform.position.x - targetXPos) < 0.2) 
			{
				xSpeed = 0;
			}
			handleButtonPress = false;
		}

		if (xSpeed > 0 && transform.position.x > targetXPos) 
		{
			transform.position = new Vector3 (targetXPos, transform.position.y, transform.position.z);
			xSpeed = 0;
		}
		else if (xSpeed < 0 && transform.position.x < targetXPos) 
		{
			transform.position = new Vector3 (targetXPos, transform.position.y, transform.position.z);
			xSpeed = 0;
		}
		// 4 - Movement per direction
			movement = new Vector2 (
				xSpeed, speed);
	}

	void crashAnimation()
	{
		if (!isDead)
			return;
		crashAnimationTimer += Time.deltaTime;
		float scaling = 5 * Time.deltaTime;

		transform.localScale = new Vector3(transform.localScale.x - scaling,transform.localScale.y-scaling,transform.localScale.z);
		transform.Rotate(0, 0, 60*scaling);

		if (crashAnimationTimer > 0.5) 
		{
			finished = true;
			Destroy(gameObject, 0.2f);
		}
	}
	void FixedUpdate()
	{
		if (isDead)
				rigidbody2D.velocity = Vector2.zero;
		else {

			Vector2 tmpMovement = new Vector2 (movement.x * Time.deltaTime, 0);
			rigidbody2D.velocity = tmpMovement;
			//transform.Rotate(0, 0, rotation);
			checkScore();
		}

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		handleCrash();
	}

	private void accelerate()
	{
		speed += 200 * Time.deltaTime;
	}

	private void handleCrash()
	{
		isDead = true;
	}

	private void checkScore()
	{
		ObstacleScript[] obScripts = FindObjectsOfType<ObstacleScript> ();
		foreach (ObstacleScript oScript in obScripts) 
		{
			if(oScript.transform.position.y < gameObject.transform.position.y && oScript.id+1 > score)
				score = oScript.id+1;
		}
	}
}
