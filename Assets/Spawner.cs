using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public static Spawner singleton;
	
	
	public bool autoSetup = false;
	
	public GameObject playerCamera = null;
	public GameObject playerPrefab = null;
	public Transform initialCameraSpawn = null;
	public Transform playerSpawn = null;
	
	public int spawnRadius = 5;
	Vector3 spawnPos;
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy () {
		singleton = null;
	}
	
	
	public void SpawnCameras(int noOfPlayers){
		for(int i=0;i<noOfPlayers;i++){
			GameObject newCam = Instantiate(playerCamera,initialCameraSpawn.position,initialCameraSpawn.rotation) as GameObject;
			Rect camRect = new Rect();
			// only spawn 1 camera
			if(noOfPlayers ==1){
				camRect = new Rect(0,0,1,1);
				
				
			}
			// spawn 2 cameras
			else if(noOfPlayers ==2){
				if(i==0){
					// cam 1 = bottom screen
					camRect = new Rect(0,0.5f,1,0.5f);
					
				}
				else if(i==1){
					// cam 2 = bottom screen
					camRect = new Rect(0,0f,1,0.5f);
					
				}
			}
			// spawn more than 2 cameras
			else if(noOfPlayers >2){
				if(i == 0){
					// cam 1 = top left
					camRect = new Rect(0,0.5f,0.5f,0.5f);
					
				}
				else if(i==1){
					// cam 2 = top right
					camRect = new Rect(0.5f,0.5f,0.5f,0.5f);
					
				}
				else if(i==2){
					// cam 3 = bottom left
					camRect = new Rect(0,0,0.5f,0.5f);
					
				}
				else if(i==3){
					// cam 4 = bottom right
					camRect = new Rect(0.5f,0,1f,0.5f);
					
				}	
			}
			newCam.GetComponent<CameraViewLayers>().SetViewRect(camRect,0,1);
			PlayerCtrlSetup.singleton.AddPlayerCamera(i, newCam);
		}
		GameManager.singleton.GotPlayerCameras();
	}
	
	// TODO remove this
	public void SpawnPlayers(int noOfPlayers){
		
		for(uint i=0;i<noOfPlayers;i++){
			spawnPos = playerSpawn.position;
			spawnPos.z += Random.Range(-spawnRadius,spawnRadius);
			spawnPos.x += Random.Range(-spawnRadius,spawnRadius);
			GameObject newPlayer = Instantiate(playerPrefab,spawnPos,playerSpawn.rotation) as GameObject;
			// get the player script
			PlayerController playerScript = newPlayer.GetComponent<PlayerController>();
			// set the controller number to use
			playerScript.controllerNo = i;
			// send the right input
			playerScript.SetInputAxes(PlayerCtrlSetup.singleton.playerInputAxes[i]);
			// get the camera for the player
			playerScript.GetCamera();
			Team team = (Team)((i%2));
			playerScript.SetTeam(team);
			
		}
		/*int plNr = 0;
		for(int i=0;i<playerCount;i++){
			GameObject newPlayer = Instantiate(playerPrefab,playerSpawns[i].position,playerSpawns[i].rotation) as GameObject;
			// send the Axis names to the player
			for(;plNr<4;plNr++){
				print("checking for player "+plNr);
				if(GameManager.singleton.activePlayers[plNr] == true){
					PlayerCtrlSetup.singleton.GetPlayerInputNames(plNr,newPlayer.transform);
					newPlayer.SendMessage("SetPlayerNumber",plNr,SendMessageOptions.RequireReceiver);
					print("player number sent for input msg is "+plNr);
					plNr++;
					break;
				}
			}
			GameManager.singleton.AddPlayer(i,newPlayer);
		}*/
	}
	
	
	public void RespawnPlayer (int plNr){
		//GameManager.singleton.players[plNr].transform.position = playerSpawns[plNr].position;
		//GameManager.singleton.players[plNr].transform.rotation = playerSpawns[plNr].rotation;
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
