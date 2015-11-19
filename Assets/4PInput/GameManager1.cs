using UnityEngine;
using System.Collections;

public class GameManager1 : MonoBehaviour {
	
	public bool autoSetup = false;
	Spawner1 spawner = null;
	public int playerCount = 0;
	public GameObject[] playerCameras = new GameObject[0];
	public GameObject[] players = new GameObject[0];
	public bool[] activePlayers = new bool[0];
	
	
	// Use this for initialization
	void Start () {
	playerCameras = new GameObject[4];
	activePlayers = new bool[4];
	players = new GameObject[4];
	/*for(int i=0;i<playerCameras.Length;i++){
	playerCameras[i] = new GameObject();
	players[i] = new GameObject();
	}*/
	if(autoSetup == true){
	spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spawner1>();
	}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetPlayerCount (int nrOfPlayers) {
	playerCount = nrOfPlayers;
	//ScoreManager.singleton.SetPlayerCount(nrOfPlayers);
	}
	
	public IEnumerator SetupGame () {
	spawner.SpawnCameras(playerCount);
	yield return 0;
	spawner.SpawnPlayers(playerCount);
	yield return 0;
	SetupCameras();
	yield return 0;
	}
	
	public void AddPlayerCamera(int index,GameObject go){
	playerCameras[index] = go;
	}
	
	public void AddPlayer (int index,GameObject go){
	players[index] = go;
	}
	
	void SetupCameras () {
	for(int i=0;i<playerCount;i++){
	playerCameras[i].GetComponent<PlayerCamera1>().SetTarget(players[i]);
	//TODO CORRECT THIS
	//players[i].GetComponent<Tank_Controller>().SetCamera(playerCameras[i]);
	//players[i].GetComponent<Tank_Controller>().SetPlayerNumber(i);
		}
	}
}
