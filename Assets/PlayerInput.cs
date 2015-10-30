using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public PlayerInputNames playerInput = new PlayerInputNames();
	public int playerNr = -1;
	
	public void SetPlayerInputNames (PlayerInputNames pl){
	playerInput = pl;
	}
	
	public void SetPlayerNumber (int nr){
	playerNr = nr;
	}
	
	/*
	public void GetTankInputs(Tank_Controller tc){
	tc.SetControllerAxisNames(playerInput.xAxis,playerInput.yAxis,playerInput.x2Axis,playerInput.y2Axis,playerInput.fire1Axis);
	}
	*/
}
