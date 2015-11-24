using UnityEngine;
using System.Collections;

public struct FogMapCoord {
	public sbyte x;
	public sbyte y;
	public byte fogLevel;
	public bool clearFog;
	
	public FogMapCoord(sbyte nx, sbyte ny, byte lv, bool clrFg){
		x = nx;
		y = ny;
		fogLevel = lv;
		clearFog = clrFg;
	}
}

public enum FogType {
	SmallClear,
	MediumClear,
	LargeClear,
	Custom
}

// used to specify which team this fog zone works for
public enum FogTeam {
	All,
	Vampires,
	Hunters
}

public class FogZone : MonoBehaviour {
	
	static bool hasDonePresetFogSetup = false;
	static FogMapCoord[] smallFogClear;
	static FogMapCoord[] mediumFogClear;
	static FogMapCoord[] largeFogClear;
	public FogType fogZoneType = FogType.SmallClear;
	
	public FogMapCoord[] fogTileZone;
	public FogTeam team = FogTeam.All;
	[HideInInspector]
	public Transform thisTransform;
	
	// all hard coded, for some pre-set fog clear zones
	void CreatePresetFogZones () {
		if(hasDonePresetFogSetup == false){
			// we have now done the setup
			hasDonePresetFogSetup = true;
			// create the small fog clearer		
			smallFogClear = new FogMapCoord[21];
			// centre
			smallFogClear[0] = new FogMapCoord(0,0,255, true);
			// 8 ring around centre
			smallFogClear[1] = new FogMapCoord(1,0,80, true);
			smallFogClear[2] = new FogMapCoord(1,-1,48, true);
			smallFogClear[3] = new FogMapCoord(0,-1,80, true);
			smallFogClear[4] = new FogMapCoord(-1,-1,48, true);
			smallFogClear[5] = new FogMapCoord(-1,0,80, true);
			smallFogClear[6] = new FogMapCoord(-1,1,48, true);
			smallFogClear[7] = new FogMapCoord(0,1,80, true);
			smallFogClear[8] = new FogMapCoord(1,1,48, true);
			// 12 around that ring
			smallFogClear[9] = new FogMapCoord(2,0,16, true);
			smallFogClear[10] = new FogMapCoord(2,-1,8, true);
			smallFogClear[11] = new FogMapCoord(1,-2,8, true);
			smallFogClear[12] = new FogMapCoord(0,-2,16, true);
			smallFogClear[13] = new FogMapCoord(-1,-2,8, true);
			smallFogClear[14] = new FogMapCoord(-2,-1,8, true);
			smallFogClear[15] = new FogMapCoord(-2,0,16, true);
			smallFogClear[16] = new FogMapCoord(-2,1,8, true);
			smallFogClear[17] = new FogMapCoord(-1,2,8, true);
			smallFogClear[18] = new FogMapCoord(0,2,16, true);
			smallFogClear[19] = new FogMapCoord(1,2,8, true);
			smallFogClear[20] = new FogMapCoord(2,1,8, true);
			
			// MEDIUM FOG CLEARING RING
			byte[,] medFogArea = new byte[9,9];
			mediumFogClear = new FogMapCoord[81];
			// create the ring
			for(int i=0;i<9;i++){
				for(int j=0;j<9;j++){
					
					int xPos = i-4;
					int yPos = j-4;
					float mag = 5-(Mathf.Sqrt((xPos*xPos)+(yPos*yPos)));
					if(mag < 0){
						mag = 0;
					}
					mag = (mag/5);
					byte fogDensity = (byte)(mag*255);
					medFogArea[i,j] = fogDensity;
					
				}
			}
			
			for(int i=0;i<81;i++){
				int xCoord = (int)(i%9);
				int yCoord = (int)(i/9);
				
				mediumFogClear[i] = new FogMapCoord((sbyte)(xCoord-4),(sbyte)(yCoord-4),medFogArea[xCoord,yCoord],true);
				
			}
			
			// LARGE FOG CLEARING RING
			byte[,] lrgFogArea = new byte[15,15];
			largeFogClear = new FogMapCoord[225];
			// create the ring
			for(int i=0;i<15;i++){
				for(int j=0;j<15;j++){
					
					int xPos = i-7;
					int yPos = j-7;
					float mag = 8-(Mathf.Sqrt((xPos*xPos)+(yPos*yPos)));
					if(mag < 0){
						mag = 0;
					}
					mag = (mag/8);
					byte fogDensity = (byte)(mag*255);
					lrgFogArea[i,j] = fogDensity;
					
				}
			}
			
			for(int i=0;i<225;i++){
				int xCoord = (int)(i%15);
				int yCoord = (int)(i/15);
				
				largeFogClear[i] = new FogMapCoord((sbyte)(xCoord-7),(sbyte)(yCoord-7),lrgFogArea[xCoord,yCoord],true);
				
			}
		}
	}
	
	void SetFogZone () {
		switch(fogZoneType){
			
			case FogType.LargeClear:
			fogTileZone = largeFogClear;
			break;
			
			case FogType.MediumClear:
				fogTileZone = mediumFogClear;
			break;
			
			case FogType.SmallClear:
				fogTileZone = smallFogClear;
			break;
			
			case FogType.Custom:
				// change this
				fogTileZone = mediumFogClear;
			break;
			
			// by default, all fog zones are small clearing ones
			default:
				fogTileZone = smallFogClear;
			break;
			
		}
	}
	
	// Use this for initialization
	void Start () {
		CreatePresetFogZones();
		SetFogZone ();
		
	
		thisTransform = this.transform;
		WorldController.RegisterFogZone(this);
	}
	
	void OnDestroy () {
		//WorldController.RemoveFogZone(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
