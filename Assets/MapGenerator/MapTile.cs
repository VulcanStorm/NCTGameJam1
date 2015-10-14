using UnityEngine;
using System.Collections;

[System.Serializable]
public struct MapTile {
	// height to draw the tile at
	public byte height;
	// whether it is open or not... so we can know if fog can be here
	public bool isOpen;
	// how much fog is here
	public byte fog;
	
	public MapTile(byte h){
		height = h;
		isOpen = true;
		fog = 0;
		
	}
}
