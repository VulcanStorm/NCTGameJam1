using UnityEngine;
using System.Collections;

[System.Serializable]
public struct MapTile {
	// height to draw the tile at
	public byte height;
	// whether it is open or not... so we can know if fog can be here
	public bool isOpen;
	// used to determine how high the fog is, so higher fogs arent modified by lower ones...
	// allows for upwards height
	public byte fogHeight;
	// base fog value, to return to
	public byte baseFog;
	// how much fog is here
	public byte fog;
	
	public MapTile(byte h){
		height = h;
		isOpen = true;
		fog = 255;
		fogHeight = 1;
		baseFog = 255;
		
	}
}
