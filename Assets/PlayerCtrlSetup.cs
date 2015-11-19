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

public struct PlayerInputNames {
	
	// For use with an xbox 360 controller
	
	public string xAxis;
	public string yAxis;
	public string x2Axis;
	public string y2Axis;
	public string fire1Axis;
	public string fire2Axis;
	
}

// TODO: create the axes in the input manager, 

public class PlayerCtrlSetup : MonoBehaviour {
	
	public static PlayerCtrlSetup singleton = null;
	public bool autoSetup = false;
	
	
	// actual variables used in game
	bool[] activePlayers = new bool[4];
	public PlayerCtrlData[] localPlayerCtrlData; 
	
	// seutp variables
	int vScreenHeight,vScreenWidth = 0;
	public PlayerInputNames[] playerInputAxes;
	int totalPlayers = 0;
	bool inJoinMenu = false;
	bool isPC = false;
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy () {
		singleton = null;
	}
	
	// Use this for initialization
	void Start () {
	// get the screen dimensions
	vScreenHeight = Screen.height;
	vScreenWidth = Screen.width;
	// check if we auto setup everything
	if(autoSetup == true){
	playerInputAxes = new PlayerInputNames[4];
	//
	//playerInputAxes[0] = new PlayerInputNames();
	playerInputAxes[0].xAxis = "Joy1_X";
	playerInputAxes[0].yAxis = "Joy1_Y";
	playerInputAxes[0].x2Axis = "Joy1_X2";
	playerInputAxes[0].y2Axis = "Joy1_Y2";
	playerInputAxes[0].fire1Axis = "Joy1_Fire1";
	playerInputAxes[0].fire2Axis = "Joy1_Fire2";
	//
	//playerInputAxes[1] = new PlayerInputNames();
	playerInputAxes[1].xAxis = "Joy2_X";
	playerInputAxes[1].yAxis = "Joy2_Y";
	playerInputAxes[1].x2Axis = "Joy2_X2";
	playerInputAxes[1].y2Axis = "Joy2_Y2";
	playerInputAxes[1].fire1Axis = "Joy2_Fire1";
	playerInputAxes[1].fire2Axis = "Joy2_Fire2";
	//
	//playerInputAxes[2] = new PlayerInputNames();
	playerInputAxes[2].xAxis = "Joy3_X";
	playerInputAxes[2].yAxis = "Joy3_Y";
	playerInputAxes[2].x2Axis = "Joy3_X2";
	playerInputAxes[2].y2Axis = "Joy3_Y2";
	playerInputAxes[2].fire1Axis = "Joy3_Fire1";
	playerInputAxes[2].fire2Axis = "Joy3_Fire2";
	//
	//playerInputAxes[3] = new PlayerInputNames();
	playerInputAxes[3].xAxis = "Joy4_X";
	playerInputAxes[3].yAxis = "Joy4_Y";
	playerInputAxes[3].x2Axis = "Joy4_X2";
	playerInputAxes[3].y2Axis = "Joy4_Y2";
	playerInputAxes[3].fire1Axis = "Joy4_Fire1";
	playerInputAxes[3].fire2Axis = "Joy4_Fire2";
	}
	}
	
	public void GetPlayerInputNames (int playerNumber, Transform plObject){
	plObject.GetComponent<PlayerInput>().SetPlayerInputNames(playerInputAxes[playerNumber]);
	}
	
	public void GetPlayerControllers () {
		inJoinMenu = true;
	}
	
	void Update () {
	if(inJoinMenu == true){
	
		if(activePlayers[0] == false){
			if(Input.GetKey(KeyCode.Joystick1Button0)){
				totalPlayers += 1;
				activePlayers[0] = true;
			}
		}
		else{
			if(Input.GetKey(KeyCode.Joystick1Button7)){
				SetupGame();
			}
			else if(Input.GetKey(KeyCode.Joystick1Button1)){
				totalPlayers -= 1;
				activePlayers[0] = false;
			}
		}
		
		if(activePlayers[1] == false){
			if(Input.GetKey(KeyCode.Joystick2Button0)){
				totalPlayers += 1;
				activePlayers[1] = true;
			}
		}
		else if(Input.GetKey(KeyCode.Joystick2Button1)){
			totalPlayers -= 1;
			activePlayers[1] = false;
		}
		
		
		if(activePlayers[2] == false){
			if(Input.GetKey(KeyCode.Joystick3Button0)){
				totalPlayers += 1;
				activePlayers[2] = true;
			}
		}
		else if(Input.GetKey(KeyCode.Joystick3Button1)){
			totalPlayers -= 1;
			activePlayers[2] = false;
		}
		
		
		if(activePlayers[3] == false){
			if(Input.GetKey(KeyCode.Joystick4Button0)){
				totalPlayers += 1;
				activePlayers[3] = true;
			}
		}
		else if(Input.GetKey(KeyCode.Joystick4Button1)){
			totalPlayers -= 1;
			activePlayers[3] = false;
		}
	}
	
	
	}
	
	void OnGUI () {
	if(inJoinMenu == true){
		
		// Button to play on PC
		// this means that there is only 1 player
		if(GUI.Button(new Rect(75,25,100,25),"Play on PC")){
			activePlayers[0] = true;
			totalPlayers = 1;
			isPC = true;
			// setup the game with 1 player
			SetupGame();
		}
		
		GUI.Box(new Rect(50,50,vScreenWidth-100,vScreenHeight-100),"");
		GUI.Box(new Rect(vScreenWidth/2-150,25,300,25),"Player Connection Menu : "+totalPlayers+" Players");
			
		if(activePlayers[0] == false){
			GUI.Box(new Rect(75,75,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 1: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(75,75,vScreenWidth/2-100,vScreenHeight/2-100),"Player 1 :  Ready! Press (B) To Cancel");
			GUI.Label(new Rect(vScreenWidth/4-50,100,200,30),"Press (Start)  To Begin");
		}
		if(activePlayers[1] == false){
			GUI.Box(new Rect(75,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 2: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(75,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Player 2 : Ready! Press (B) To Cancel");
		}
		if(activePlayers[2] == false){
			GUI.Box(new Rect(vScreenWidth/2+25,75,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 3: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(vScreenWidth/2+25,75,vScreenWidth/2-100,vScreenHeight/2-100),"Player 3 : Ready! Press (B) To Cancel");
		}
		if(activePlayers[3] == false){
			GUI.Box(new Rect(vScreenWidth/2+25,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Connect Controller 4: Press (A) To Start");
		}
		else{
			GUI.Box(new Rect(vScreenWidth/2+25,vScreenHeight/2+25,vScreenWidth/2-100,vScreenHeight/2-100),"Player 4 : Ready! Press (B) To Cancel");
		}
	}
	}
	
	void SetupGame () {
		// we are no longer in the join menu
		inJoinMenu = false;
		// check if we are on PC, since we need to change the axis names for player 1
		if(isPC == true){
			playerInputAxes[0].xAxis = "Horizontal";
			playerInputAxes[0].yAxis = "Vertical";
			playerInputAxes[0].x2Axis = "Mouse X";
			playerInputAxes[0].y2Axis = "Mouse Y";
			playerInputAxes[0].fire1Axis = "Fire1";
			playerInputAxes[0].fire2Axis = "Fire2";
		}
		
		// set the player count
		GameManager.singleton.SetPlayerCount(totalPlayers);
		
		// setup the player array now we have all the players
		localPlayerCtrlData = new PlayerCtrlData[totalPlayers];
		
		// fill the empty array
		for(int i=0;i<totalPlayers;i++){
			localPlayerCtrlData[i] = new PlayerCtrlData();
		}
		
		// integer to hold which player we are currently processing
		int plCtrlNum = 0;
		
		// check all the possible players, and set their controllerIDs
		if(activePlayers[0] == true){
			localPlayerCtrlData[plCtrlNum].controllerScreenID = 0;
			localPlayerCtrlData[plCtrlNum].inputAxes = playerInputAxes[0];
				plCtrlNum++;
		}
		if(activePlayers[1] == true){
			localPlayerCtrlData[plCtrlNum].controllerScreenID = 1;
			localPlayerCtrlData[plCtrlNum].inputAxes = playerInputAxes[1];
			plCtrlNum++;
		}
		if(activePlayers[2] == true){
			localPlayerCtrlData[plCtrlNum].controllerScreenID = 2;
			localPlayerCtrlData[plCtrlNum].inputAxes = playerInputAxes[2];
			plCtrlNum++;
		}
		if(activePlayers[3] == true){
			localPlayerCtrlData[plCtrlNum].controllerScreenID = 3;
			localPlayerCtrlData[plCtrlNum].inputAxes = playerInputAxes[3];
			plCtrlNum++;
		}
		// finally notify the game manager that we have all the player controllers
		GameManager.singleton.GotPlayerControllers();
	}
	
	// called from spawner when spawning cameras
	public void AddPlayerCamera(int index, GameObject go){
		localPlayerCtrlData[index].cameraObject = go;
	}
	
}
