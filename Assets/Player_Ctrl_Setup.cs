/*
	-
	-
	-
	- THIS CODE IS FOR XBOX 360 CONTROLLERS ONLY
	- IT IS NOT PROOF TESTED FOR OTHERS
	-
	-
	-
*/
using UnityEngine;
using System.Collections;

public class PlayerInputNames {
	
	// For use with an xbox 360 controller
	
	public string xAxis = "";
	public string yAxis = "";
	public string x2Axis = "";
	public string y2Axis = "";
	public string fire1Axis = "";
	public string fire2Axis = "";
	
}

// TODO: create the axes in the input manager, 

public class Player_Ctrl_Setup : MonoBehaviour {
	
	public static Player_Ctrl_Setup singleton = null;
	public bool autoSetup = false;
	public PlayerInputNames[] playerInputs;
	//public GameManager gameManager = null;
	
	int vScreenHeight,vScreenWidth = 0;
	
	int totalPlayers = 0;
	bool inJoinMenu = false;
	bool isPC = false;
	bool hasPlayer1,hasPlayer2,hasPlayer3,hasPlayer4;
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy () {
		singleton = null;
	}
	
	// Use this for initialization
	void Start () {
	vScreenHeight = Screen.height;
	vScreenWidth = Screen.width;
	if(autoSetup == true){
	//gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	playerInputs = new PlayerInputNames[4];
	//
	playerInputs[0] = new PlayerInputNames();
	playerInputs[0].xAxis = "Joy1_X";
	playerInputs[0].yAxis = "Joy1_Y";
	playerInputs[0].x2Axis = "Joy1_X2";
	playerInputs[0].y2Axis = "Joy1_Y2";
	playerInputs[0].fire1Axis = "Joy1_Fire1";
	playerInputs[0].fire2Axis = "Joy1_Fire2";
	//
	playerInputs[1] = new PlayerInputNames();
	playerInputs[1].xAxis = "Joy2_X";
	playerInputs[1].yAxis = "Joy2_Y";
	playerInputs[1].x2Axis = "Joy2_X2";
	playerInputs[1].y2Axis = "Joy2_Y2";
	playerInputs[1].fire1Axis = "Joy2_Fire1";
	playerInputs[1].fire2Axis = "Joy2_Fire2";
	//
	playerInputs[2] = new PlayerInputNames();
	playerInputs[2].xAxis = "Joy3_X";
	playerInputs[2].yAxis = "Joy3_Y";
	playerInputs[2].x2Axis = "Joy3_X2";
	playerInputs[2].y2Axis = "Joy3_Y2";
	playerInputs[2].fire1Axis = "Joy3_Fire1";
	playerInputs[2].fire2Axis = "Joy3_Fire2";
	//
	playerInputs[3] = new PlayerInputNames();
	playerInputs[3].xAxis = "Joy4_X";
	playerInputs[3].yAxis = "Joy4_Y";
	playerInputs[3].x2Axis = "Joy4_X2";
	playerInputs[3].y2Axis = "Joy4_Y2";
	playerInputs[3].fire1Axis = "Joy4_Fire1";
	playerInputs[3].fire2Axis = "Joy4_Fire2";
	}
	}
	
	public void GetPlayerInputNames (int playerNumber, Transform plObject){
	plObject.GetComponent<PlayerInput>().SetPlayerInputNames(playerInputs[playerNumber]);
	}
	
	public void GetPlayerControllers () {
		inJoinMenu = true;
	}
	
	void Update () {
	if(inJoinMenu == true){
	
	if(hasPlayer1 == false){
	if(Input.GetKey(KeyCode.Joystick1Button0)){
	totalPlayers += 1;
	hasPlayer1 = true;
		}
	}
	else{
	if(Input.GetKey(KeyCode.Joystick1Button7)){
		inJoinMenu = false;
		SetupGame();
		}
	}
	if(hasPlayer2 == false){
	if(Input.GetKey(KeyCode.Joystick2Button0)){
	totalPlayers += 1;
	hasPlayer2 = true;
		}
	}
	if(hasPlayer3 == false){
	if(Input.GetKey(KeyCode.Joystick3Button0)){
	totalPlayers += 1;
	hasPlayer3 = true;
		}
	}
	if(hasPlayer4 == false){
	if(Input.GetKey(KeyCode.Joystick4Button0)){
	totalPlayers += 1;
	hasPlayer4 = true;
		}
	}
	}
	
	
	}
	
	void OnGUI () {
	if(inJoinMenu == true){
		
		// Button to play on PC
		// this means that there is only 1 player
		if(GUI.Button(new Rect(75,25,100,25),"Play on PC")){
			hasPlayer1 = true;
			totalPlayers = 1;
			isPC = true;
			inJoinMenu = false;
			SetupGame();
		}
		
		GUI.Box(new Rect(50,50,vScreenWidth-100,vScreenHeight-100),"");
		GUI.Box(new Rect(vScreenWidth/2-150,25,300,25),"Player Connection Menu : "+totalPlayers+" Players");
			
		if(hasPlayer1 == false){
			GUI.Box(new Rect(75,75,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 1: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(75,75,vScreenWidth/2-100,vScreenHeight/2-100),"Player 1 : Waiting For Game Start");
			GUI.Label(new Rect(vScreenWidth/4-50,100,200,30),"Press (Start) To Begin");
		}
		if(hasPlayer2 == false){
			GUI.Box(new Rect(75,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 2: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(75,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Player 2 : Waiting For Game Start");
		}
		if(hasPlayer3 == false){
			GUI.Box(new Rect(vScreenWidth/2+25,75,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 3: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(vScreenWidth/2+25,75,vScreenWidth/2-100,vScreenHeight/2-100),"Player 3 : Waiting For Game Start");
		}
		if(hasPlayer4 == false){
			GUI.Box(new Rect(vScreenWidth/2+25,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 4: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(vScreenWidth/2+25,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Player 4 : Waiting For Game Start");
		}
	}
	else{
	
	}
	
	
	}
	
	void SetupGame () {
	// check if we are on PC, since we need to change the axis names
	if(isPC == true){
		playerInputs[0].xAxis = "Horizontal";
		playerInputs[0].yAxis = "Vertical";
		playerInputs[0].x2Axis = "Mouse X";
		playerInputs[0].y2Axis = "Mouse Y";
		playerInputs[0].fire1Axis = "Fire1";
		playerInputs[0].fire2Axis = "Fire2";
	}
	/*
	gameManager.activePlayers[0] = hasPlayer1;
	gameManager.activePlayers[1] = hasPlayer2;
	gameManager.activePlayers[2] = hasPlayer3;
	gameManager.activePlayers[3] = hasPlayer4;
	gameManager.SetPlayerCount(totalPlayers);
	gameManager.StartCoroutine(gameManager.SetupGame());
	*/
	}
	
}
