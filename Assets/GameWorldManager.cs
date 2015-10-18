using UnityEngine;
using System.Collections;

public class GameWorldManager : MonoBehaviour {
	
	public int worldSizeX = 64;
	public int worldSizeY = 64;
	
	// Use this for initialization
	void Start () {
		WorldController.singleton.CreateTestWorld(new Vector2(worldSizeX,worldSizeY));
		//WorldController.singleton.CreateNewWorld(new Vector2(worldSizeX, worldSizeY));
		FogTextureGen.singleton.CreateNewFogTexture();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
