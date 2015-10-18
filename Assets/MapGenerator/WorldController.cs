using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {
	
	public static WorldController singleton;
	public MapTile[,] world;
	public MapData mapData;
	public int chunkSize;
	public int worldSizeX;
	public int worldSizeY;
	
	// fog management variables
	public List<FogZone> fogZones;
	
	
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
	
	public static void RegisterFogZone (FogZone fz) {
		singleton.fogZones.Add (fz);
	}
	
	public static void RemoveFogZone (FogZone fz){
		singleton.fogZones.Remove(fz);
	}
	
	public void UpdateFog () {
		// simple fog removal
		
		// refresh the fog map
		for(int i=0;i<worldSizeX;i++){
			for(int j=0;j<worldSizeY;j++){
				world[i,j].fog = world[i,j].baseFog;
			}
		}
		
		// iterate over the list of fog zones
		
		// add all the fog generated first
		
		// now remove any fog that needs clearing
		for(int i=0;i<fogZones.Count;i++){
			// iterate over the fog zone array
			ProcessFogZone(fogZones[i]);
		}
		
	}
	
	void ProcessFogZone (FogZone zone) {
		// get the tile position of the object
		int baseXPos = CheckBoundsX(zone.thisTransform.position.x-0.5f);
		int baseYPos = CheckBoundsY(zone.thisTransform.position.z-0.5f);
		
		int xPos;
		int yPos;
		
		// iterate over all the tiles
		for(int i=0;i<zone.fogTileZone.Length;i++){
			xPos = CheckBoundsX(zone.fogTileZone[i].x + baseXPos);
			yPos = CheckBoundsY(zone.fogTileZone[i].y + baseYPos);
			world[xPos,yPos].fog = BoundFog(world[xPos,yPos].fog - zone.fogTileZone[i].fogLevel);
		}
	}
	
	static byte b;
	
	byte BoundFog (int num){
		if(num < 0){
			b=0;
		}
		else if(num > 255){
			b=255;
		}
		else{
			b=(byte)num;
		}
		return b;
	}
	
	static int p;
	
	
	
	int CheckBoundsX (float pos) {
		if(pos < 0){
			p = 0;
		}
		else if(pos > worldSizeX-1){
			p = worldSizeX-1;
		}
		else{
			p = Mathf.RoundToInt(pos);
		}
		return p;
	}
	
	int CheckBoundsY (float pos) {
		if(pos < 0){
			p = 0;
		}
		else if(pos > worldSizeY-1){
			p = worldSizeY-1;
		}
		else{
			p = Mathf.RoundToInt(pos);
		}
		return p;
	}
	
	public void CreateTestWorld (Vector2 size) {
		worldSizeX = (int)size.x;
		worldSizeY = (int)size.y;
		world = new MapTile[(int)size.x,(int)size.y];
		
		for(int i=0;i<worldSizeX;i++){
			world[i,0].fog = 255;
			world[i,0].baseFog = 255;
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
