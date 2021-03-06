﻿using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public Texture btnLeftTexture;
	public Texture btnRightTexture;
	public Texture btnMiddleTexture;

	public Texture btnRestartTexture;
	public Texture btnBackTexture;
	
	//public GameObject greenObstacle;
	//public GameObject redObstacle;


	static public Bounds stage_bounds;

	static public int obstacleIndex;
	static public int iteration;
	static public int obstacleCount;
	static public int goCount = 0;

	private Rect gameOverWindowRect;

	private float startTimer;

	private GameObject numberObject;
	int showingNumber;


	public GUIStyle lblStyle; 
	public GUIStyle lblStyleBig; 

	public GUIStyle btnTurnStyle; 

	public GUISkin mainSkin;
	public GUISkin keySkin;

	private bool videoAdToBeShown;
	private bool interstitialToBeShown;
	// LEVEL 1
	// 10-90
	int[] obstAmountLvl1 = new int[100] 
	{89, 44, 64, 81, 75, 25, 57, 40, 41, 46, 
		36, 33, 16, 61, 18, 65, 19, 77, 79, 28, 
		63, 59, 10, 47, 20, 85, 72, 24, 53, 15, 
		52, 54, 62, 58, 80, 29, 57, 69, 31, 11, 
		87, 73, 76, 26, 43, 22, 39, 51, 84, 67, 
		86, 66, 55, 42, 74, 17, 49, 60, 48, 35, 
		81, 78, 27, 50, 13, 83, 38, 30, 88, 68, 
		14, 56, 12, 82, 23, 32, 70, 90, 45, 21,
		53, 81, 22, 44, 29, 46, 88, 89, 84, 42, 
		61, 51, 62, 40, 58, 37, 50, 65, 54, 63};

	int[] obstAmountLvl2 = new int[100] 
	{32, 49, 62, 90, 86, 21, 20, 14, 88, 53, 
		17, 33, 73, 44, 61, 39, 87, 30, 80, 18, 
		22, 25, 65, 10, 23, 29, 74, 63, 41, 34, 
		58, 54, 79, 48, 27, 66, 64, 76, 47, 82, 
		56, 36, 67, 45, 77, 69, 13, 16, 31, 11, 
		51, 83, 84, 26, 28, 42, 59, 89, 46, 15, 
		35, 40, 50, 24, 72, 68, 37, 60, 52, 19,
		9, 24, 62, 12, 44, 19, 23, 34, 74, 85,
		4, 59, 88, 22, 12, 50, 84, 52, 71, 63,
		85, 70, 55, 75, 71, 57, 43, 38, 12, 78};

	int[] obstAmountLvl3 = new int[100] 
	{29, 37, 28, 63, 76, 72, 10, 49, 15, 11, 
		71, 38, 58, 16, 70, 22, 32, 24, 20, 49, 
		50, 78, 36, 51, 83, 84, 52, 31, 74, 34, 
		90, 85, 42, 55, 80, 79, 61, 12, 64, 13, 
		75, 86, 77, 45, 67, 59, 56, 23, 68, 18, 
		17, 69, 21, 66, 33, 46, 40, 25, 81, 65, 
		48, 57, 26, 54, 73, 82, 44, 43, 89, 53,
		45, 88, 70, 77, 27, 75, 65, 30, 28, 79,
	    88, 70, 77, 27, 75, 65, 30, 28, 79, 65,
		60, 88, 39, 62, 14, 35, 47, 87, 37, 30};

	int[] obstAmountLvl4 = new int[100] 
	{32, 49, 62, 90, 86, 21, 20, 14, 88, 53, 
		17, 33, 73, 44, 61, 39, 87, 30, 80, 18, 
		22, 25, 65, 10, 23, 29, 74, 63, 41, 34, 
		58, 54, 79, 48, 27, 66, 64, 76, 47, 82, 
		56, 36, 67, 45, 77, 69, 13, 16, 31, 11, 
		51, 83, 84, 26, 28, 42, 59, 89, 46, 15, 
		35, 40, 50, 24, 72, 68, 37, 60, 52, 19,
		9, 24, 62, 12, 44, 19, 23, 34, 74, 85,
		4, 59, 88, 22, 12, 50, 84, 52, 71, 63,
		85, 70, 55, 75, 71, 57, 43, 38, 12, 78};
	
	int[] obstAmountLvl5 = new int[100] 
	{29, 37, 28, 63, 76, 72, 10, 49, 15, 11, 
		71, 38, 58, 16, 70, 22, 32, 24, 20, 49, 
		50, 78, 36, 51, 83, 84, 52, 31, 74, 34, 
		90, 85, 42, 55, 80, 79, 61, 12, 64, 13, 
		75, 86, 77, 45, 67, 59, 56, 23, 68, 18, 
		17, 69, 21, 66, 33, 46, 40, 25, 81, 65, 
		48, 57, 26, 54, 73, 82, 44, 43, 89, 53,
		45, 88, 70, 77, 27, 75, 65, 30, 28, 79,
		88, 70, 77, 27, 75, 65, 30, 28, 79, 65,
		60, 88, 39, 62, 14, 35, 47, 87, 37, 30};

	// 10-90
	int[] obstDistanceLvl1 = new int[100] 
	{13, 71, 42, 90, 41, 50, 11, 39, 67, 10, 
		45, 88, 70, 77, 27, 75, 65, 30, 28, 79, 
		56, 58, 35, 81, 22, 46, 78, 82, 33, 60, 
		21, 47, 49, 25, 15, 83, 66, 48, 73, 54, 
		43, 84, 14, 63, 69, 72, 68, 17, 31, 36, 
		89, 24, 62, 12, 44, 19, 23, 34, 74, 85, 
		80, 51, 18, 86, 53, 61, 76, 29, 59, 20, 
		38, 64, 32, 16, 57, 55, 52, 37, 40, 87,
		24, 59, 88, 22, 12, 50, 84, 52, 71, 63, 
		67, 80, 83, 82, 17, 48, 89, 64, 47, 85};

	int[] obstDistanceLvl2 = new int[100] 
	{67, 80, 83, 82, 17, 48, 89, 64, 47, 85,
		13, 71, 42, 90, 41, 50, 11, 39, 67, 10, 
		24, 59, 88, 22, 12, 50, 84, 52, 71, 63,
		45, 88, 70, 77, 27, 75, 65, 30, 28, 79, 
		56, 58, 35, 81, 22, 46, 78, 82, 33, 60, 
		43, 84, 14, 63, 69, 72, 68, 17, 31, 36, 
		89, 24, 62, 12, 44, 19, 23, 34, 74, 85, 
		80, 51, 18, 86, 53, 61, 76, 29, 59, 20,
		21, 47, 49, 25, 15, 83, 66, 48, 73, 54,
		38, 64, 32, 16, 57, 55, 52, 37, 40, 87};

	int[] obstDistanceLvl3 = new int[100] 
	{13, 71, 22, 46, 78, 82, 33, 39, 67, 10, 
		56, 58, 35, 81, 22, 46, 78, 82, 33, 60, 
		21, 47, 49, 25, 15, 83, 66, 48, 73, 54, 
		45, 88, 70, 77, 27, 75, 65, 30, 28, 79, 
		24, 59, 88, 22, 12, 50, 84, 52, 71, 63, 
		43, 84, 14, 63, 69, 72, 68, 17, 31, 36, 
		89, 24, 62, 12, 44, 19, 23, 34, 74, 85, 
		80, 51, 86, 53, 61, 52, 76, 29, 59, 20, 
		38, 51, 18, 86, 53, 61, 52, 37, 40, 87,
		67, 80, 83, 82, 17, 48, 89, 64, 47, 85};

	int[] obstDistanceLvl4 = new int[100] 
	{50, 27, 78, 64, 85, 28, 30, 34, 47, 72, 
		63, 19, 45, 62, 51, 69, 39, 48, 60, 35, 
		81, 37, 84, 26, 86, 20, 57, 29, 82, 89, 
		12, 33, 90, 53, 58, 66, 32, 67, 54, 18, 
		22, 71, 88, 75, 11, 10, 16, 77, 49, 56, 
		14, 80, 36, 46, 74, 13, 44, 70, 52, 21, 
		31, 87, 61, 59, 76, 73, 68, 83, 38, 43, 
		40, 17, 25, 24, 15, 42, 65, 79, 23, 55,
		21, 47, 49, 25, 15, 83, 66, 48, 73, 54,
		38, 64, 32, 16, 57, 55, 52, 37, 40, 87};
	
	int[] obstDistanceLvl5 = new int[100] 
	{22, 23, 27, 46, 15, 17, 49, 84, 11, 36, 
		51, 59, 20, 74, 35, 44, 47, 56, 45, 25, 
		88, 15, 78, 41, 34, 73, 37, 55, 81, 75, 
		29, 40, 24, 13, 71, 31, 38, 76, 89, 57, 
		62, 68, 77, 54, 80, 26, 33, 21, 14, 32, 
		72, 63, 22, 23, 27, 46, 61, 69, 12, 82, 
		66, 87, 67, 30, 64, 43, 79, 28, 52, 42, 
		60, 85, 65, 10, 48, 90, 18, 39, 58, 19, 
		38, 51, 18, 86, 53, 61, 52, 37, 40, 87,
		67, 80, 83, 82, 17, 48, 89, 64, 47, 85};

	int[] obstSideLvl1 = new int[100]
	{1,0,1,0,1,0,1,0,1,0,
		1,1,0,1,0,1,0,1,0, 0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,1,0,1,0,1,0,1,
		0,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,0,1,0,1,0,1,0,1,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,0,1,1,0,0,1,0};

	int[] obstSideLvl2 = new int[100]
	{1,0,1,0,1,0,1,0,1,0,
		1,1,0,1,0,1,1,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,1,1,0,1,
		0,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,0,1,1,0,1,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,0,1,1,0,0,1,0};

	int[] obstSideLvl3 = new int[100]
	{1,0,1,1,0,0,1,0,1,1,
		0,1,0,1,1,0,0,1,0, 0,
		1,0,1,0,1,1,0,0,1,0,
		1,0,0,1,1,0,1,0,1,0,
		0,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,0,1,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,0,1,1,0,0,1,0};

	int[] obstSideLvl4 = new int[100]
	{0,1,1,0,1,0,1,0,1,0,
		1,1,0,1,0,0,1,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,1,0,0,1,0,1,1,0,1,
		0,0,1,0,1,1,0,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,0,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,0,1,1,0,0,1,0};
	
	int[] obstSideLvl5 = new int[100]
	{1,0,1,1,0,1,0,0,1,1,
		0,1,0,1,1,0,1,0,0, 1,
		1,0,1,0,1,1,0,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		0,0,1,1,0,1,0,0,1,0,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,0,1,1,0,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,0,1,1,0,0,1,0};

	int[] obstTypeLvl1 = new int[100]
	{1,0,1,0,1,1,0,1,0,1,
		0,1,0,1,0,0,1,0,1,0,
		1,0,0,1,0,1,0,1,0,1,
		0,1,1,0,1,0,1,0,1,0,
		1,0,1,0,1,1,0,1,0,1,
		1,0,1,0,1,0,1,0,1,0,
		1,1,0,1,0,1,0,1,0,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,1,0,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0};

	int[] obstTypeLvl2 = new int[100]
	{1,1,0,1,0,1,0,0,1,1,
		0,1,1,0,1,0,1,0,1,0,
		1,0,0,1,0,1,0,1,0,1,
		0,1,1,0,1,0,1,0,1,0,
		1,0,1,0,1,1,0,1,0,1,
		1,0,1,0,1,0,1,0,1,0,
		1,1,0,1,0,1,0,1,0,0,
		1,0,1,0,1,0,0,1,1,0,
		1,1,0,1,0,1,0,1,0,0,
		1,0,1,0,1,0,0,1,1,0};

	int[] obstTypeLvl3 = new int[100]
	{0,0,1,0,1,1,0,1,0,1,
		0,1,1,0,1,0,1,0,1,0,
		0,1,0,1,0,0,1,0,1,0,
		1,1,0,1,0,1,0,1,0,0,
		1,0,0,1,0,1,0,1,0,1,
		1,0,1,0,1,1,0,1,0,1,
		1,0,1,0,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,1,0,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0};

	int[] obstTypeLvl4 = new int[100]
	{1,1,0,1,0,1,0,0,1,1,
		0,1,0,1,0,0,1,0,1,0,
		1,0,0,1,0,1,0,1,0,1,
		1,0,1,0,1,0,1,0,1,0,
		1,1,0,1,0,1,0,1,0,1,
		1,0,1,0,1,0,1,0,1,0,
		1,1,0,1,0,1,0,1,0,0,
		1,0,1,0,1,0,0,1,1,0,
		1,1,0,1,0,1,0,1,0,0,
		1,0,1,0,1,0,0,1,1,0};
	
	int[] obstTypeLvl5 = new int[100]
	{0,1,0,1,0,1,1,0,1,1,
		0,1,1,0,1,0,1,0,1,0,
		0,1,0,1,0,1,0,1,1,0,
		1,1,0,1,0,1,0,1,0,0,
		1,0,0,1,0,1,0,1,0,1,
		1,0,1,0,1,1,0,1,0,1,
		1,1,0,1,1,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0,
		1,0,1,1,0,0,1,0,1,0,
		1,0,1,0,1,0,0,1,1,0};

	// Use this for initialization
	void Start () {
	
		/*GameObject carObject = GameObject.Find ("Player");
		if (carObject != null) 
		{
			SpriteRenderer carSprite = carObject.GetComponent<SpriteRenderer>();
			if(carSprite != null)
			{
				if(SelectedLevelScript.selectedCar == 1)
					carSprite.sprite = Resources.Load<UnityEngine.Sprite>("car2");
				else if(SelectedLevelScript.selectedCar == 2)
					carSprite.sprite = Resources.Load<UnityEngine.Sprite>("rallycar");
			}
		}*/
		HighScoreScript hsScript = (HighScoreScript) gameObject.GetComponent(typeof(HighScoreScript));
		//hsScript.Save(); // This will reset highscores
		hsScript.Load();
		obstacleIndex = 0;
		obstacleCount = 0;
		iteration = 0;
		startTimer = 0;
		videoAdToBeShown = false;
		interstitialToBeShown = false;
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = Camera.main.orthographicSize * 2;
		stage_bounds = new Bounds(
			Camera.main.transform.position,
			new Vector3(cameraHeight * screenAspect, cameraHeight, 0));


		CreateNextObstacle (0f);
		CreateLanes();

		showingNumber = 4;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (CarScript.stopped)
			checkStartup ();

	}

	void checkStartup()
	{
		if (!CarScript.stopped)
			return;

		if (showingNumber > 3) 
		{
			Destroy(numberObject, 0.1f);
			numberObject = (GameObject) GameObject.Instantiate(Resources.Load("Number3", typeof(GameObject)));
			showingNumber = 3;
		}
		if (startTimer > 1 && showingNumber == 3) 
		{
			Destroy(numberObject, 0.1f);
			numberObject = (GameObject) GameObject.Instantiate(Resources.Load("Number2", typeof(GameObject)));
			showingNumber = 2;
		}
		if (startTimer > 2 && showingNumber == 2) 
		{
			Destroy(numberObject, 0.1f);
			numberObject = (GameObject) GameObject.Instantiate(Resources.Load("Number1", typeof(GameObject)));
			showingNumber = 1;
		}
		if (startTimer > 2.8f && showingNumber == 1) 
		{
			Destroy(numberObject, 0.1f);
			numberObject = (GameObject) GameObject.Instantiate(Resources.Load("Number0", typeof(GameObject)));
			showingNumber = 0;
			Destroy(numberObject, 0.8f);
			AdHandlerScript.hideBanner();
		}
		numberObject.transform.position = new Vector3(0,0,-3);
		startTimer += Time.deltaTime;

		float scaling = 5 * Time.deltaTime;
		numberObject.transform.localScale = new Vector3(numberObject.transform.localScale.x + scaling,numberObject.transform.localScale.y+scaling,numberObject.transform.localScale.z);
		if (startTimer > 3) 
		{
			AdHandlerScript.hideBanner();

			CarScript.stopped = false;
			goCount++;
			if (goCount == 3 || goCount == 30) 
			{
				videoAdToBeShown = true;
			}
			if (goCount == 10 || goCount == 50) 
			{
				interstitialToBeShown = true;
			}
			if(numberObject)
				Destroy(numberObject, 0.8f);
		}
	}
	void OnGUI () {

		GUI.skin = mainSkin;
		GUI.skin.window.normal.background = keySkin.window.normal.background;

		if (CarScript.finished) {
			float winYSpace = Screen.height/10;
			float winXSpace = Screen.width/10;

			Rect gameOverWindowRect = new Rect(winXSpace, 1.4f*winYSpace, Screen.width -2*winXSpace, Screen.height-2.4f*winYSpace);

			gameOverWindowRect = GUI.Window(0, gameOverWindowRect, DoGameOverWindow, "");

		} else {
			GUI.skin = keySkin;

			float btnHeight = Screen.height / 4;
			if (GUI.Button (new Rect (0, Screen.height - btnHeight, Screen.width / 3, btnHeight), btnLeftTexture)) {
				CarScript.targetLane = 1;
				CarScript.handleButtonPress = true;
			} 

			if (GUI.Button (new Rect (1*Screen.width / 3, Screen.height - btnHeight, Screen.width / 3, btnHeight), btnMiddleTexture)) {
				CarScript.targetLane = 2;
				CarScript.handleButtonPress = true;
			}

			if (GUI.Button (new Rect (2*Screen.width / 3, Screen.height - btnHeight, Screen.width / 3, btnHeight), btnRightTexture)) {
				CarScript.targetLane = 3;
				CarScript.handleButtonPress = true;
			} 
			lblStyle.fontSize = 24 * Screen.height / 800;

			GUI.Label (new Rect (3*Screen.width / 100, 3*Screen.width / 100, Screen.width / 2, btnHeight / 8), "SCORE: " + CarScript.score.ToString (),lblStyle);
		}
	}

	void DoGameOverWindow(int windowID) {

		if (!AdHandlerScript.bannerVisible)
		{
			AdHandlerScript.showBanner();
		}

		GUI.skin = mainSkin;
		GUI.skin.button.fontSize = 22 * Screen.height / 800;

		float btnHeight = Screen.height / 16;
		float btnWidth = Screen.width / 4;
		float lblStartY = btnHeight;
		float btnStartY = Screen.width / 2 + 1.6f*btnHeight;

		lblStyleBig.fontSize = 34 * Screen.height / 800;
		lblStyle.fontSize = 26 * Screen.height / 800;

		GUI.Label(new Rect(Screen.width / 2 -1.3f*btnWidth, Screen.height / 10, 2*btnWidth, btnHeight), "GAME OVER!", lblStyleBig);

		GUI.Label(new Rect(Screen.width / 2 -1.0f*btnWidth, Screen.height / 10+1.5f*btnHeight, 2*btnWidth, btnHeight), "SCORE: "+ CarScript.score.ToString(), lblStyle);

		HighScoreScript hsScript = (HighScoreScript) gameObject.GetComponent(typeof(HighScoreScript));

		int hsScore = 0;
		if (SelectedLevelScript.selectedLevel == 1) 
		{
			hsScore = hsScript.highScores.highscore1;
		}
		else if (SelectedLevelScript.selectedLevel == 2) 
		{
			hsScore = hsScript.highScores.highscore2;
		}
		else if (SelectedLevelScript.selectedLevel == 3) 
		{
			hsScore = hsScript.highScores.highscore3;
		}
		else if (SelectedLevelScript.selectedLevel == 4) 
		{
			hsScore = hsScript.highScores.highscore4;
		}
		else if (SelectedLevelScript.selectedLevel == 5) 
		{
			hsScore = hsScript.highScores.highscore5;
		}
		int carScore = CarScript.score;

		if (hsScore <= carScore) 
		{
			GUI.Label (new Rect (Screen.width / 2 - 1.5f * btnWidth, Screen.height / 10 + 3f * btnHeight, btnWidth*5, btnHeight), "THAT'S A HIGHSCORE!", lblStyle);

			if (SelectedLevelScript.selectedLevel == 1) 
			{
				hsScript.highScores.highscore1 = carScore;
			}
			else if (SelectedLevelScript.selectedLevel == 2) 
			{
				hsScript.highScores.highscore2 = carScore;
			}
			else if (SelectedLevelScript.selectedLevel == 3) 
			{
				hsScript.highScores.highscore3 = carScore;
			}
			else if (SelectedLevelScript.selectedLevel == 4) 
			{
				hsScript.highScores.highscore4 = carScore;
			}
			else if (SelectedLevelScript.selectedLevel == 5) 
			{
				hsScript.highScores.highscore5 = carScore;
			}
			hsScript.Save();
		}

		if (GUI.Button(new Rect(Screen.width / 2 -btnWidth, btnStartY, btnWidth, 1.2f*btnHeight), " RESTART"))
			Application.LoadLevel("GameScene");

		if (GUI.Button(new Rect(Screen.width / 2 -btnWidth, btnStartY+2*btnHeight, btnWidth, 1.2f*btnHeight), "  BACK"))
			Application.LoadLevel("LevelScene");

		if (videoAdToBeShown)
		{
			AdHandlerScript.showVideoAd();
			videoAdToBeShown = false;
		}
		if (interstitialToBeShown)
		{
			AdHandlerScript.showInterstitial();
			interstitialToBeShown = false;
		}
	}

	public void CreateNextObstacle(float prevY)
	{
		GameObject newObstacle;

		int obsType = 1;

		if (SelectedLevelScript.selectedLevel == 1) 
		{
			obsType = obstTypeLvl1[obstacleIndex];
		}
		else if (SelectedLevelScript.selectedLevel == 2) 
		{
			obsType = obstTypeLvl2[obstacleIndex];
		}
		else if (SelectedLevelScript.selectedLevel == 3) 
		{
			obsType = obstTypeLvl3[obstacleIndex];
		}
		else if (SelectedLevelScript.selectedLevel == 4) 
		{
			obsType = obstTypeLvl4[obstacleIndex];
		}else if (SelectedLevelScript.selectedLevel == 5) 
		{
			obsType = obstTypeLvl5[obstacleIndex];
		}
		// Create correct type of obstacle
		if(obsType == 0)
			newObstacle = (GameObject) GameObject.Instantiate(Resources.Load("Obstacle_green", typeof(GameObject)));
		else
			newObstacle = (GameObject) GameObject.Instantiate(Resources.Load("Obstacle_red", typeof(GameObject)));


		// Set id
		ObstacleScript obsScript = (ObstacleScript) newObstacle.GetComponent(typeof(ObstacleScript));
		if (obsScript) 
		{
			obsScript.id = obstacleCount;
		}

		//calculate position

		float obsDist = 0;
		float obsAmount = 0;
		int obsSide = 0;
		float freeSpace = 0;

		if (SelectedLevelScript.selectedLevel == 1) 
		{
			obsDist = obstDistanceLvl1[obstacleIndex];
			obsAmount = obstAmountLvl1[obstacleIndex];
			obsSide = obstSideLvl1[obstacleIndex];
			freeSpace = (0.1f+0.6f*(obsAmount / 100)) * BackgroundScript.stage_bounds.size.x;
		}
		else if (SelectedLevelScript.selectedLevel == 2) 
		{
			obsDist = obstDistanceLvl2[obstacleIndex];
			obsAmount = obstAmountLvl2[obstacleIndex];
			obsSide = obstSideLvl2[obstacleIndex];
			freeSpace = (0.1f + 0.5f * (obsAmount / 100)) * BackgroundScript.stage_bounds.size.x;
		}
		else if (SelectedLevelScript.selectedLevel == 3) 
		{
			obsDist = obstDistanceLvl3[obstacleIndex];
			obsAmount = obstAmountLvl3[obstacleIndex];
			obsSide = obstSideLvl3[obstacleIndex];
			freeSpace = (0.1f + 0.45f * (obsAmount / 100)) * BackgroundScript.stage_bounds.size.x;
		}
		else if (SelectedLevelScript.selectedLevel == 4) 
		{
			obsDist = obstDistanceLvl4[obstacleIndex];
			obsAmount = obstAmountLvl4[obstacleIndex];
			obsSide = obstSideLvl4[obstacleIndex];
			freeSpace = (0.1f + 0.45f * (obsAmount / 100)) * BackgroundScript.stage_bounds.size.x;
		}
		else if (SelectedLevelScript.selectedLevel == 5) 
		{
			obsDist = obstDistanceLvl5[obstacleIndex];
			obsAmount = obstAmountLvl5[obstacleIndex];
			obsSide = obstSideLvl5[obstacleIndex];
			freeSpace = (0.1f + 0.40f * (obsAmount / 100)) * BackgroundScript.stage_bounds.size.x;
		}

		if (SelectedLevelScript.selectedLevel == 1 && iteration < 1 && obstacleIndex < 40) 
		{
			if (freeSpace < 4.5f)
				freeSpace = 4.5f;

		}
		else if (iteration > 3) 
		{
			if (freeSpace < 3f)
					freeSpace = 3f;
		}
		else 
		{
			if (freeSpace < 4f)
				freeSpace = 4f;
		}

		float max_speed = CarScript.getMaxSpeed();

		if (SelectedLevelScript.selectedLevel == 1) 
		{
			max_speed = 250 + CarScript.score / 2.0f;

			if (max_speed > 400)
					max_speed = 400;
		}
		else if (SelectedLevelScript.selectedLevel == 2) 
		{
			max_speed = 300 + CarScript.score / 2.0f;
			
			if (max_speed > 450)
				max_speed = 450;
		}
		else if (SelectedLevelScript.selectedLevel == 3) 
		{
			max_speed = 360 + CarScript.score / 2.0f;
			
			if (max_speed > 500)
				max_speed = 500;
		}
		else if (SelectedLevelScript.selectedLevel == 4) 
		{
			max_speed = 420 + CarScript.score;
			
			if (max_speed > 600)
				max_speed = 600;
		}
		else if (SelectedLevelScript.selectedLevel == 5) 
		{
			max_speed = 510 + CarScript.score;
			
			if (max_speed > 700)
				max_speed = 700;
		}
		CarScript.setMaxSpeed (max_speed);

		float x_pos;

		if (obsSide == 0) {
			x_pos = BackgroundScript.stage_bounds.max.x - freeSpace - newObstacle.collider2D.bounds.size.x/2;
		} 
		else 
		{
			x_pos = BackgroundScript.stage_bounds.min.x+freeSpace+newObstacle.collider2D.bounds.size.x/2;
		}

		float y_space = obsDist / 12f;

		if (y_space < 4.5f)
			y_space = 4.5f;

		float y_pos = prevY+y_space;

		if (obstacleIndex == 0)
			y_pos += 4;
		newObstacle.transform.position = new Vector3(x_pos,y_pos,0);

		obstacleIndex++;
		obstacleCount++;

		if (obstacleIndex > 99)
		{
			obstacleIndex = 0;
			iteration++;
		}
			

	}

	public void CreateLanes()
	{
		GameObject newLines1;
		GameObject newLines2;

		// Create correct type of obstacle
		newLines1 = (GameObject) GameObject.Instantiate(Resources.Load("LaneSprite", typeof(GameObject)));
		newLines2 = (GameObject) GameObject.Instantiate(Resources.Load("LaneSprite", typeof(GameObject)));

				
		//calculate position

		
		newLines1.transform.position = new Vector3(BackgroundScript.stage_bounds.min.x+BackgroundScript.stage_bounds.size.x/3,0,1);
		newLines2.transform.position = new Vector3(BackgroundScript.stage_bounds.min.x+2*BackgroundScript.stage_bounds.size.x/3,0,1);

		
	}
}
