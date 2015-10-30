using UnityEngine;
using System.Collections;

public class PlayerData {
	
	public PlayerIdInfo idInfo;
	public PlayerLoadoutInfo loadoutInfo;
	
	
	public PlayerData () {
		idInfo = new PlayerIdInfo();
		loadoutInfo = new PlayerLoadoutInfo();
	}
	
	void ClearData () {
	
	}
}
