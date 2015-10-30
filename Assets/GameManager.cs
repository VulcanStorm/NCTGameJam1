using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager singleton = null;
	
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
	// Get Player Inputs
	GetPlayerInputs();
		WorldController.singleton.CreateTestWorld(new Vector2(worldSizeX,worldSizeY));
		//WorldController.singleton.CreateNewWorld(new Vector2(worldSizeX, worldSizeY));
		FogTextureGen.singleton.CreateNewFogTexture();
	}
	
	void GetPlayerInputs () {
		Player_Ctrl_Setup.singleton.GetPlayerControllers();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
