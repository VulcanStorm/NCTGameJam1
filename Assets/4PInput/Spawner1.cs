using UnityEngine;
using System.Collections;

public class Spawner1 : MonoBehaviour {
	
	public bool autoSetup = false;
	public GameObject playerCamera = null;
	public GameObject playerPrefab = null;
	public Transform initialCameraSpawn = null;
	public Transform[] playerSpawns = new Transform[4];
		
	GameManager1 gameManager = null;
	PlayerCtrlSetup playerCtrlSetup = null;
	
	
	// Use this for initialization
	void Start () {
		if(autoSetup == true){
			gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager1>();
			playerCtrlSetup = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerCtrlSetup>();
		}
	}
	
	public void SpawnCameras(int playerCount){
	for(int i=0;i<playerCount;i++){
	GameObject newCam = Instantiate(playerCamera,initialCameraSpawn.position,initialCameraSpawn.rotation) as GameObject;
	if(playerCount ==1){
	newCam.GetComponent<Camera>().rect = new Rect(0,0,1,1);
	newCam.GetComponent<Camera>().depth = 0;
	
	}
	else if(playerCount ==2){
	if(i==0){
	// cam 1 = bottom screen
	newCam.GetComponent<Camera>().rect = new Rect(0,0.5f,1,0.5f);
	newCam.GetComponent<Camera>().depth = 0;
		}
	else if(i==1){
	// cam 2 = bottom screen
	newCam.GetComponent<Camera>().rect = new Rect(0,0f,1,0.5f);
	newCam.GetComponent<Camera>().depth = 0;
	}
	}
	else if(playerCount >2){
	if(i == 0){
	// cam 1 = top left
	newCam.GetComponent<Camera>().rect = new Rect(0,0.5f,0.5f,0.5f);
	newCam.GetComponent<Camera>().depth = 0;
		}
	else if(i==1){
	// cam 2 = top right
	newCam.GetComponent<Camera>().rect = new Rect(0.5f,0.5f,0.5f,0.5f);
	newCam.GetComponent<Camera>().depth = 0;
		}
	else if(i==2){
	// cam 3 = bottom left
	newCam.GetComponent<Camera>().rect = new Rect(0,0,0.5f,0.5f);
	newCam.GetComponent<Camera>().depth = 0;
		}
	else if(i==3){
	// cam 4 = bottom right
	newCam.GetComponent<Camera>().rect = new Rect(0.5f,0,1f,0.5f);
	newCam.GetComponent<Camera>().depth = 0;
		}	
		}
	gameManager.AddPlayerCamera(i, newCam);
	}
	}
	
	public void SpawnPlayers(int playerCount){
	int plNr = 0;
	for(int i=0;i<playerCount;i++){
	GameObject newPlayer = Instantiate(playerPrefab,playerSpawns[i].position,playerSpawns[i].rotation) as GameObject;
	// send the Axis names to the player
	for(;plNr<4;plNr++){
	print("checking for player "+plNr);
	if(gameManager.activePlayers[plNr] == true){
	playerCtrlSetup.GetPlayerInputNames(plNr,newPlayer.transform);
	newPlayer.SendMessage("SetPlayerNumber",plNr,SendMessageOptions.RequireReceiver);
	print("player number sent for input msg is "+plNr);
	plNr++;
	break;
	}
	}
	gameManager.AddPlayer(i,newPlayer);
	}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void RespawnPlayer (int plNr){
	gameManager.players[plNr].transform.position = playerSpawns[plNr].position;
	gameManager.players[plNr].transform.rotation = playerSpawns[plNr].rotation;
	}
}
