using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager singleton = null;
	
	public int playerCount = 0;
	
	public int worldSizeX = 64;
	public int worldSizeY = 64;
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy () {
		singleton = null;
	}
	
	// Use this for initialization
	void Start () {
		// create the world
		WorldController.singleton.CreateTestWorld(new Vector2(worldSizeX,worldSizeY));
		//WorldController.singleton.CreateNewWorld(new Vector2(worldSizeX, worldSizeY));
		FogTextureGen.singleton.CreateNewFogTexture();
		
		// start the game!
		
		// Get Player Inputs
		GetPlayerInputs();
	}
	
	void GetPlayerInputs () {
		PlayerCtrlSetup.singleton.GetPlayerControllers();
	}
	
	/*
	// called from player ctrl setup when we have all the players
	public void SetActivePlayers(bool p1, bool p2, bool p3, bool p4){
		activePlayers[0] = p1;
		activePlayers[1] = p2;
		activePlayers[2] = p3;
		activePlayers[3] = p4;
	}
	*/
	
	// called from player ctrl setup
	public void SetPlayerCount (int plCount){
		playerCount = plCount;
	}
	
	// called from player ctrl setup
	// what to do once we have the player controllers setup?
	
	
	public void GotPlayerControllers () {
		
		// spawn player cameras
		print ("got player controllers");
		Spawner.singleton.SpawnCameras(playerCount);
	}
	
	// called from spawner when we have all of the cameras created
	public void GotPlayerCameras () {
		print ("got player cameras");
		// spawn players
		Spawner.singleton.SpawnPlayers(playerCount);
		
	}
	
	
	/*
	// called from spawner when creating cameras
	// this is to set the camera to the controller
	public void AddPlayerCamera(int index, GameObject go){
		localPlayerCtrlData[index].cameraObject = go;
	}
	*/
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void GameOver () {
		PlayerCtrlSetup.singleton.GameOver();
	}
}
