using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
	
	public static WorldController singleton;
	public MapTile[,] world;
	public MapData mapData;
	public int chunkSize;
	public int worldSizeX;
	public int worldSizeY;
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy(){
		singleton = null;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	public void CreateWorld(Vector2 size){
		worldSizeX = (int)size.x;
		worldSizeY = (int)size.y;
		world = new MapTile[(int)size.x,(int)size.y];
		MapGenerator.singleton.CreateEmptyWorld(size);
	}
	
	public void LoadMap(string filePath){
	//TODO LOAD THIS
		mapData = MapData.Load(filePath);
		// copy the map data array
		world = (MapTile[,])mapData.map.Clone();
		// create this mesh
		MapGenerator.singleton.CreateLoadedWorldMesh(new Vector2(mapData.mapSizeX,mapData.mapSizeY));
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void UpdateWater () {
		
	}
}
