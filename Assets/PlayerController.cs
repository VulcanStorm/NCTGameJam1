using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public uint controllerNo = 9;
	public PlayerInputNames inputAxes;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// used to set the input axes when spawning the players
	void SetInputAxes(PlayerInputNames newAxes){
		inputAxes = newAxes;
	}
}
