using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	
	public static PlayerManager singleton;
	
	public PlayerData[] playerData;
	
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy () {
		singleton = null;
	}
	
	
	// Use this for initialization
	// TODO change this
	void Start () {
		playerData = new PlayerData[4];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
