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
	
	public void CreateNewWorld(Vector2 size){
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
		//MapGenerator.singleton.CreateLoadedWorldMesh(new Vector2(mapData.mapSizeX,mapData.mapSizeY));
		MapGenerator.singleton.CreateLoadedWorldMesh(false);
		
	}
	
	public void CreateWorldFromMapTiles(MapTile[,] map){
		world = (MapTile[,])map.Clone();
		worldSizeX = map.GetLength(0);
		worldSizeY = map.GetLength(1);
		MapGenerator.singleton.CreateLoadedWorldMesh(true);
		
	}
	
	// Update is called once per frame
	void Update () {
		// TODO, remove this
	}
	
	public void UpdateFog () {
	/*	for(int i=0;i<worldSizeX;i++){
			world[i,0].fog = (byte)(255 * Mathf.Abs(Mathf.Sin (i*Mathf.Deg2Rad + Time.time)));
			for(int n=1;n<worldSizeY;n++){
				world[i,n].fog = world[i,0].fog;
			}
		}
	*/	
	}
	
	public void CreateTestWorld (Vector2 size) {
		worldSizeX = (int)size.x;
		worldSizeY = (int)size.y;
		world = new MapTile[(int)size.x,(int)size.y];
		
		for(int i=0;i<worldSizeX;i++){
			world[i,0].fog = 255;
			world[i,0].height = 1;
			for(int n=1;n<worldSizeY;n++){
				world[i,n] = world[i,0];
			}
		}
		world[5,5].height = 10;
		world[5,4].height = 10;
		world[5,3].height = 10;
		world[5,2].height = 10;
		
		world[5,5].fog = 0;
		world[5,4].fog = 0;
		world[5,3].fog = 0;
		world[5,2].fog = 0;
		
		MapGenerator.singleton.CreateLoadedWorldMesh(true);
	}
}
